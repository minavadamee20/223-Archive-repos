using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Timers;

public class Sineframe : Form               //Sineframe inherits from Form class
{  //It is desirable to maintain a 16:9 aspect ratio, but it is not a hard requirement.
   private const int form_width = 1920;     //A graphical area of size 1920x1080 has standard aspect ratio 16:9.
   private const int form_height = 1080;    //Valid x-coordinates: 0-1919; valid y-coordinates: 0-1079.
   Size maxframesize = new Size(1920,1080); //Ultra HD TV size = 3829x2160
   Size minframesize = new Size(1920,1080); //Regular HD TV size =1920x1080

   //Declare constants
   private const double scale_factor = 100.0;    //One mathematical unit equals 100 pixels
   private const double offset = 40.0;           //The origin is located this many pixels to the right of the left boundary.
   private const double refresh_rate = 88.8;     //Frequency in Hertz: How many times per second the display area is repainted.
   private const double dot_update_rate = 55.5;  //Frequency in Hertz: How many times per second the coordinates of the dot are updated.
   private const double time_converter = 1000.0; //Number of milliseconds per second.
   private const double delta = 0.015;           //Find this number by experimentation.  A smaller number will result in smoother
                                                 //but slower animation.  A larger delta will produce faster motion, but will appear jerky.
   private const double radius_of_mark = 2.0;    //Radius of the small unit mark on the either of the two axes.

   //Declare constants for the sine function: y = amplitude * sin (coefficient * t).
   private const double amplitude = 4.00;       //Mathematical units
   private const double period = 6.0;//6.283185307;   //6.283185307=2π Mathematical units
   private const double coefficient = 5.0*System.Math.PI/period;  //coefficient is measured in Mathematical units
   private double slope;  //Slope of the tangent to the sine curve.  Fortunately, sine has no directly vertical tangents.
   private string slope_string;  //Contains slope after conversion to string type.

   //Declare tools for outputting the equation as a string,
   private string equation;  //Holds the equation: y = amplitude * sin (coefficient * t)
   private double equation_height = 12;
   private Font equation_font; //To be initialize inside constructor Sineframe()
   private Brush equation_brush = new SolidBrush(System.Drawing.Color.Blue);
   private double equation_location_x = offset+16.0;
   private double equation_location_y = 16.0;

   //Variables for the parameterize 	d sine curve function: γ(t) = (x(t),y(t)) where x(t) = t, y(t) = A*sin(B*t), A = amplitude,
   //                                                                          // B = coefficient = 2π/period.
   private const double t_initial_value = 0.0;  //Start particle motion at γ(0.0) = (0.0,0.0) = Origin
   private double t = t_initial_value;
   private double x;  //Uses mathematical units
   private double y;  //Uses mathematical units
   private const double dot_radius = 6.0;  //The dot traveling in the sine path has a fixed radius.

   //Variables for the scaled description of the sine curve
   private double x_scaled_double;//x-coordinate of the upper left corner of the dot
   private double y_scaled_double;

   //Variables scaled for drawing on the graphic area
   private int x_scaled_int;//x-coordinate of the center of the dot
   private int y_scaled_int;//y-coordinate of the center of the dot
   private string x_string;
   private string y_string;

   //Declare clocks
   private static System.Timers.Timer graphic_area_refresh_clock = new System.Timers.Timer();
   private static System.Timers.Timer dot_motion_clock = new System.Timers.Timer();
   private bool motion_clock_active;

   //Declare variable related to numbers placed on axes.
   double label_location_x;
   double label_location_y;
   double label_value;
   const double label_height = 10.0;  //Pixels
   Font label_font = new System.Drawing.Font("Arial",(float)label_height,FontStyle.Regular);
   Brush label_brush = new SolidBrush(System.Drawing.Color.Black);

   //Declare buttons
   Button start_button = new Button();
   private bool this_is_first_click_on_start_button;
   Button exit_button = new Button();

   //Declare a tool kit of algorithms
   Sinecurve curve_algorithms;

   //Define the constructor of this class.
   public Sineframe()
   {   //Set the size of the frame (window) holding the graphic area.
       Size = new Size(form_width,form_height);
       MaximumSize = maxframesize;
       MinimumSize = minframesize;
       //Set the title of this user interface.
       Text = "Particle moving in a Sine Wave Pattern";
       //Give feedback to the programmer.
       System.Console.WriteLine("Form_width = {0}, Form_height = {1}.", form_width, form_height);
       //Set the initial background color of this form.
       BackColor = Color.Beige;

       //Instantiate the collection of supporting algorithms
       curve_algorithms = new Sinecurve();

       //Set initial values for computing the position of the dot which will travel in a Sine curve pattern.
       //The point (x,y) is the center of the dot in mathematical coordinates.
       t = 0.0;
       x = t;
       y = amplitude * System.Math.Sin(coefficient*t);

       //x_scaled_double and y_scaled_double are the coordinates of the upper left corner of the dot.
       //The point (x_scaled_double,y_scaled_double) is the upper left corner of the dot in C# coordinate system.
       x_scaled_double = scale_factor * x + offset - dot_radius;
       y_scaled_double = (double)form_height/2.0 - scale_factor * y - dot_radius;

System.Console.WriteLine("x_scaled_double = "+x_scaled_double+" y_scaled_double = "+y_scaled_double);///ok here.

       //x_scaled_int and y_scaled_int are the integer coordinates of the upper left corner of the dot.
       x_scaled_int = (int)System.Math.Round(x_scaled_double);
       y_scaled_int = (int)System.Math.Round(y_scaled_double);

       //Prepare a string holding the sine function: y = amplitude * sin (coefficient * t).
       equation_font = new System.Drawing.Font("Arial",(float)equation_height,FontStyle.Regular);
       equation = "y = "+String.Format("{0:0.00}",amplitude)+"*sin("+String.Format("{0:0.00}",coefficient)+"*x)";
       System.Console.WriteLine("The function is "+equation);

       //Prepare the refresh clock
       graphic_area_refresh_clock.Enabled = false;
       graphic_area_refresh_clock.Elapsed += new ElapsedEventHandler(Update_the_graphic_area);

       //Prepare the dot clock
       dot_motion_clock.Enabled = false;
       dot_motion_clock.Elapsed += new ElapsedEventHandler(Update_the_position_of_the_dot);
       motion_clock_active = false;

       //Prepare both clocks for start up.
       Initialize_graphic_clock(refresh_rate);
       Initialize_dot_clock(dot_update_rate);

       //Set up the start/pause button
       start_button.Text = "Start";
       start_button.Size = new Size(60,20);
       start_button.BackColor = Color.LimeGreen;
       start_button.Location = new Point((int)System.Math.Round(offset+15.0),form_height-160);
       start_button.Click += new EventHandler(All_systems_go);
       this_is_first_click_on_start_button = true;
       Controls.Add(start_button);

       //Set up the exit button
       exit_button.Text = "Quit";
       exit_button.Size = new Size(60,20);
       exit_button.BackColor = Color.Orange;
       exit_button.Location = new Point(form_width-80,form_height-160);
       exit_button.Click += new EventHandler(Shutdown);
       Controls.Add(exit_button);

       //Use extra memory to make a smooth animation.
       DoubleBuffered = true;

   }//End of constructor of Sineframe class.

   protected override void OnPaint(PaintEventArgs a)
   {   Graphics displayarea = a.Graphics;
       Pen schaffer = new Pen(Color.Purple,1);
       //Next draw x-axis:
       displayarea.DrawLine(schaffer,0,form_height/2,form_width-1,form_height/2);
       //Next draw y-axis:
       displayarea.DrawLine(schaffer,(int)System.Math.Round(offset),0,(int)System.Math.Round(offset),form_height-1);

       //Write the sine function in the upper left corner.
       displayarea.DrawString(equation,equation_font,equation_brush,(int)System.Math.Round(equation_location_x),(int)System.Math.Round(equation_location_y));

       //Write the slope of the instantaneous tangent in real time.
       slope = amplitude*coefficient*System.Math.Cos(coefficient*t);
       slope_string = "Slope = " + String.Format("{0:0.0}",slope);
       displayarea.DrawString(slope_string,equation_font,equation_brush,(int)System.Math.Round(equation_location_x)+200,(int)System.Math.Round(equation_location_y));

       //Display x and y coordinate values
       displayarea.DrawString("Center of the dot:",equation_font,equation_brush,(int)System.Math.Round(offset)+250,form_height-100);
       x_string = "x = " + String.Format("{0:0.0}",x);
	System.Console.WriteLine("drawing x coordinate tics");
       displayarea.DrawString(x_string,equation_font,equation_brush,(int)System.Math.Round(offset)+250,form_height-80);
       y_string = "y = " + String.Format("{0:0.0}",y);
       displayarea.DrawString(y_string,equation_font,equation_brush,(int)System.Math.Round(offset)+250,form_height-60);

       //Write labels on the y-axis.
       label_value = -5.0;
       label_location_x = offset-22.0;
       label_location_y = (double)form_height/2.0-9.0+5.0*scale_factor;
       for(int k=-5;k<6;k++)

             {displayarea.DrawString(String.Format("{0:0.0}",label_value),label_font,label_brush,(int)System.Math.Round(label_location_x),(int)System.Math.Round(label_location_y));
              label_value = label_value+1.0;
              label_location_y = label_location_y-scale_factor;

             }//End of for loop

       //Write labels on the x-axis.
       label_value = 1.0;
       label_location_x = offset+scale_factor;;
       label_location_y = (double)form_height/2.0;
       for(int k=1;k<19;k++)
             {displayarea.FillEllipse(Brushes.Black,(int)System.Math.Round(label_location_x-radius_of_mark),(int)System.Math.Round(label_location_y-radius_of_mark),
              (int)System.Math.Round(2.0*radius_of_mark),(int)System.Math.Round(2.0*radius_of_mark));
              displayarea.DrawString(String.Format("{0:0.0}",label_value),label_font,label_brush,(int)System.Math.Round(label_location_x)-10,(int)System.Math.Round(label_location_y));
              label_value = label_value+1.0;
              label_location_x = label_location_x+scale_factor;
             }//End of for loop

       //If the size of the dot is a single pixel it is almost invisible to human eyes.
       //Therefore, we make this dot be a solid disc of diameter > 1 pixel.
       displayarea.FillEllipse(Brushes.Red,x_scaled_int,y_scaled_int,(int)System.Math.Round(dot_radius*2.0),(int)System.Math.Round(dot_radius*2.0));
       base.OnPaint(a);
   }

   protected void Initialize_graphic_clock(double refreshrate)//Called by constructor
   {double elapsedtimebetweentics;
    if(refreshrate < 1.0) refreshrate = 1.0;  //Do not allow updates slower than 1 hertz.
    elapsedtimebetweentics = time_converter/refreshrate;  //elapsed time between tics has units milliseconds
    graphic_area_refresh_clock.Interval = (int)System.Math.Round(elapsedtimebetweentics);
    System.Console.WriteLine("Method Initialize_graphic_clock has terminated.");
   }//End of method Initialize_graphic_clock

   protected void Initialize_dot_clock(double dot_parameter_update_rate)//Called by constructor
   {double elapsedtimebetweenchangestodotcoordinates;
    //This program does not allow a clock speed slower than one hertz.
    if(dot_parameter_update_rate < 1.0) dot_parameter_update_rate = 1.0;
    //Compute the interval in millisec between each tick of the clock.
    elapsedtimebetweenchangestodotcoordinates = time_converter/dot_parameter_update_rate;
    dot_motion_clock.Interval = (int)System.Math.Round(elapsedtimebetweenchangestodotcoordinates);
    System.Console.WriteLine("Method Initialize_dot_clock has now terminated.");
   }//End of method Initialize_dot_clock

   protected void All_systems_go(Object sender,EventArgs events)//Event handler of the start button.
   {//The motion clock will either pause or resume
    if(this_is_first_click_on_start_button)
        {//This section updates the position of the ball one time when the start button is first clicked.  This is necessary when-
         //refresh_rate > dot_update_rate.  The location of the ball will be displayed incorrectly in the interval between the
         //first click on the start button and the first tic of the dot_motion_clock.  Therefore, the following section performs
         //a one-time update of the position of the ball.
         curve_algorithms.get_next_coordinates(amplitude,coefficient,delta,ref t,out x,out y);
         //Convert the Cartesian coordinates to scaled coordinates for viewing on a monitor
         x_scaled_double = scale_factor * x + offset;
         y_scaled_double = (double)form_height/2.0 - scale_factor*y;
         this_is_first_click_on_start_button = false;
         if(x_scaled_double > (double)(form_width-1))
            {dot_motion_clock.Enabled = false;
             graphic_area_refresh_clock.Enabled = false;
             System.Console.WriteLine("Both clocks have stopped.  You may close the window.");
            }//End of if
        }//End of section updating the position of the ball one time

    if(motion_clock_active)
        {dot_motion_clock.Enabled = false;
         graphic_area_refresh_clock.Enabled = false;
         start_button.Text = "Resume";
        }
    else
        {dot_motion_clock.Enabled = true;
         graphic_area_refresh_clock.Enabled = true;
         start_button.Text = "Pause";
        }
    motion_clock_active = !motion_clock_active;
   }//End of method All_system_go

   //The next method updates the x and y coordinates of the dot that is tracing the sine curve.
   protected void Update_the_position_of_the_dot(System.Object sender,ElapsedEventArgs an_event)//dot_motion_clock handler
   {   //Call a method to compute the next pair of Cartesian coordinates for the moving particle.
       curve_algorithms.get_next_coordinates(amplitude,coefficient,delta,ref t,out x,out y);
       //Convert the Cartesian coordinates to scaled coordinates for viewing on a monitor
       x_scaled_double = scale_factor * x + offset;
       y_scaled_double = (double)form_height/2.0 - scale_factor*y;
       if(x_scaled_double > (double)(form_width-1))
          {dot_motion_clock.Enabled = false;
           graphic_area_refresh_clock.Enabled = false;
           System.Console.WriteLine("Both clocks have stopped.  You may close the window.");
          }//End of if
   }//End of method Update_the_position_of_the_dot

   //The next method place converts the dot's floating-point double coordinates to coordinates of type int, and then calls the
   //the OnPaint method to paint the dot on the visible graphical area.  Notice that the mathematical calculations of computer
   //graphics are performed consistently in type double.  Only at the last instant immediately before integer coordinates are
   //need does the casting from double to int occur.
   protected void Update_the_graphic_area(System.Object sender, ElapsedEventArgs even)//Refresh clock handler
   {//The amount dot_radius is subtracted in order that the center of the dot be drawn exactly where the sine wave is.
       x_scaled_int = (int)System.Math.Round(x_scaled_double-dot_radius);//x_scaled_int is x-coordinate of the center of the dot   //Why is this here?
       y_scaled_int = (int)System.Math.Round(y_scaled_double-dot_radius);//y_scaled_int is y-coordinate of the center of the dot   //Why is this here?
       Invalidate();  //This function actually calls OnPaint.  Yes, that is true.
       if(x_scaled_int >= form_width)  //dot has reach the right edge of the frame
          {graphic_area_refresh_clock.Enabled = false;  //Stop refreshing the graphic area
           System.Console.WriteLine("The graphical area is no longer refreshing.  You may close the window.");
          }
   }//End of Update_the_graphic_area

   protected void Shutdown(System.Object sender, EventArgs even)
   {System.Console.WriteLine("This program will close its window and end execution.");
    Close();
   }

}//End of Sineframe class
