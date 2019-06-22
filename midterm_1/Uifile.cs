

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Timers;

public class Uifile: Form
{  private const int maximum_form_width  = 1920;         //A graphical area of size 1920x1080 has standard aspect ratio 16:9.
   private const int maximum_form_height = 1080;        //Valid x-coordinates: 0-1919; valid y-coordinates: 0-1079.
   private const int minimum_form_width  = 640;
   private const int minimum_form_height = 360;
   Size maxframesize = new Size(maximum_form_width,maximum_form_height);
   Size minframesize = new Size(minimum_form_width,minimum_form_height);

   //Declare more constants
   private const int    top_panel_height       = 50;     //Measured in pixels
   private const int    bottom_panel_height    = 110;    //Measured in pixels
   private const double delta                  = 8.972;  //Animation speed: distance traveled during each tic of the animation clock.
   private const double animation_clock_speed  = 90.7;   //Hz; how many times per second the coordinates of the ball are updated
   private double refresh_clock_speed    = 30.0;   //Hz; how many times per second the UI is re-painted
   private const double line_segment_width     = 2.0;    //Width measured in pixels
   private const double radius                 = 6.8;    //Radius measured in pixels
   private const double millisec_per_sec       = 1000.0; //Number of milliseconds per second.
   private const double animation_interval     = millisec_per_sec/animation_clock_speed;  //Units are milliseconds
   //private double refresh_clock_interval = millisec_per_sec/refresh_clock_speed;    //Units are milliseconds

   //Declare variables related to points on the rectangular path.
   //P0 = (p0x,p0y) is the starting point.
   //P1 = (p1x,p1y) is where the first 90 degree turn occurs.
   //P2 = (p2x,p2y) is where the second 90 degree turn occurs.
   //P3 = (p3x,p3y) is the stopping point.
   private int p0x;
   private int p0y;
   private int p1x;
   private int p1y;
   private int p2x;
   private int p2y;
   private int p3x;
   private int p3y;
   private int p4x;
   private int p4y;
   private Point start_point;                //This point will be constructed inside the interface constructor
   private Point upper_left_corner_point;    //Ditto
   private Point lower_left_corner_point;    //Ditto
   private Point end_point;                  //Ditto
   private Point lower_right_corner_point;

   //Declare variables related to user interface layout.
   private Pen schaffer = new Pen(Color.Purple,1);
   private Pen bic      = new Pen(Color.Red,(int)System.Math.Round(line_segment_width));
   private const String welcome_message = "Radio Control by Mina Trevizo";
   //The next statement shows how to change font size.
   private System.Drawing.Font welcome_style = new System.Drawing.Font("TimesNewRoman",24,FontStyle.Regular);
   private Brush welcome_paint_brush = new SolidBrush(System.Drawing.Color.Black);
   private Brush brushx1 = new SolidBrush(System.Drawing.Color.Green); //default
   private Point welcome_location;   //Will be initialized in the constructor.

   //Declare values that support constructing the interface
   private int form_width;
   private int form_height;

   //The next two variables are coordinates of the upper left corner of the ball.
   private double x;  //This is x-coordinate of the upper left corner of the ball.
   private double y;  //This is y-coordinate of the upper left corner of the ball.

   private Button  go_and_pause_button = new Button();
   private Button  quit_button = new Button();

   private bool clocks_are_stopped = true;

   //Declare clocks
   private static System.Timers.Timer user_interface_refresh_clock = new System.Timers.Timer();
   private static System.Timers.Timer ball_update_clock = new System.Timers.Timer();


   private RadioButton crawl = new RadioButton();
   private RadioButton walk = new RadioButton();
   private RadioButton run = new RadioButton();
   private RadioButton sprint = new RadioButton();
   private Label speedLabel = new Label();



   //Define the constructor of this class.
   public Uifile()
   {   //Set the size of the form (window) holding the graphic area: begin with a moderate size half-way between
       //maximum and minimum.
       form_width = (maximum_form_width+minimum_form_width)/2;
       form_height = (maximum_form_height+minimum_form_width)/2;
       Size = new Size(form_width,form_height);
       //Set the limits regarding how much the user may re-size the window.
       MaximumSize = maxframesize;
       MinimumSize = minframesize;
       //Set the title of this user interface.
       Text = "Radio Control by Mina Trevizo";
       //Give feedback to the programmer.
       System.Console.WriteLine("Form_width = {0}, Form_height = {1}.", Width, Height);
       //Set the initial background color of this form.
       BackColor = Color.MistyRose;

       //Set the four key points in the path of the moving ball.
       p0x = Width-90; p0y = top_panel_height+70;
       start_point = new Point(p0x,p0y);                //Starting point

       p1x = 80; p1y = p0y;
       upper_left_corner_point = new Point(p1x,p1y);    //First corner to turn 90 degrees

       p2x = p1x; p2y = top_panel_height+Height/2;
       lower_left_corner_point = new Point(p2x,p2y);    //Second corner to turn 90 degrees

       p3x = p0x; p3y = p2y;
       lower_right_corner_point = new Point(p3x, p2y);

       p4x = p0x; p4y = p1y;
       end_point = new Point(p4x,p4y);                  //Ending point

       //Initialize the ball at the starting point: subtract ball's radius so that (x,y) is the upper corner of the ball.
       x = (double)p0x-radius;
       y = (double)p0y-radius;


       crawl.Text = "Crawl";
       walk.Text = "walk";
       run.Text = "run";
       sprint.Text = "sprint";


       //The size (Width and Height) of the form may change during run-time because a user may use the mouse to re-size the form.
       //"Width" and "Height" are attributes of this UI called Ninety_degree_turn_interface.  They may be used in a read mode as
       //ordinary variables.  If the user re-sizes the form by say use of the mouse then "Width" and "Height" will be updated
       //internally with new values.

//       start_Y_coordinate.BackColor = Color.Cyan;

//       end_Y_coordinate.BackColor = Color.SeaGreen;

       //Configure the go_and_pause button
       go_and_pause_button.Size = new Size(60,30);
       go_and_pause_button.Text = "Go";
       go_and_pause_button.Location = new Point(100,form_height-80);
       go_and_pause_button.BackColor = Color.Orange;
       go_and_pause_button.Click += new EventHandler(Go_stop);

      int xLoc;
      int yLoc = 0;
       crawl.Location = new Point(yLoc += Width/3, Height - 80);
       crawl.BackColor = Color.PapayaWhip;
       walk.Location = new Point (yLoc += crawl.Width + 1, Height -80);
       walk.BackColor = Color.PapayaWhip;
       run.Location = new Point (yLoc += walk.Width + 1, Height -80);
       run.BackColor = Color.PapayaWhip;
       sprint.Location = new Point(yLoc += run.Width + 1, Height -80);
       sprint.BackColor = Color.PapayaWhip;
       speedLabel.Size = new Size(Width/3, yLoc-15);
       speedLabel.Location = new Point(Width/3, form_height-bottom_panel_height - 3);
       speedLabel.BackColor = Color.PapayaWhip;
       speedLabel.Text = "                                   Rate of Change";

       //Configure the quit button
       quit_button.Size = new Size(60,30);
       quit_button.Text = "Quit";
       quit_button.Location = new Point(Width-100,Height-80);  //Width and Height are attributes of this UI.
       quit_button.BackColor = Color.Salmon;
       quit_button.Click += new EventHandler(Close_window);

       //Prepare the refresh clock.  A button will start this clock ticking.
       user_interface_refresh_clock.Enabled = false;  //Initially this clock is stopped.
       user_interface_refresh_clock.Elapsed += new ElapsedEventHandler(Refresh_user_interface);

       //Prepare the ball clock.  A button will start this clock ticking.
       ball_update_clock.Enabled = false;
       ball_update_clock.Elapsed += new ElapsedEventHandler(Update_ball_coordinates);

       //Add controls (labels, buttons, textboxes, etc) to the form so that the user can see them.
       Controls.Add(go_and_pause_button);
       Controls.Add(quit_button);
       Controls.Add(crawl);
       Controls.Add(walk);
       Controls.Add(run);
       Controls.Add(sprint);
       Controls.Add(speedLabel);

       //Prepare for the welcome message.
       welcome_location = new Point(Width/2-340,8);

       //Use extra memory to make a smooth animation.
       DoubleBuffered = true;

   }//End of constructor of class Ninety_degree_turn_interface

//===============================================================

   protected override void OnPaint(PaintEventArgs a)
   {   Graphics displayarea = a.Graphics;

       //Next paint a green horizontal strip across the top of the ui.
       displayarea.FillRectangle(Brushes.LightGreen,0,0,Width,top_panel_height);     //Gittleman book, p. 302

       //Next draw a black horizontal line separating the green title strip from the white graphic area.
       displayarea.DrawLine(schaffer,0,top_panel_height,Width,top_panel_height);

       displayarea.FillRectangle(Brushes.LightPink,0,form_height-bottom_panel_height,Width,bottom_panel_height);
       displayarea.DrawLine(schaffer,0,form_height-bottom_panel_height,Width,form_height-bottom_panel_height);

       //The next statement draws a line segment with end points start_point and upper_left_corner_point.
       displayarea.DrawLine(bic,start_point,upper_left_corner_point);

       //The next statement draws a line segment with end points upper_left_corner_point and lower_left_corner_point.
       displayarea.DrawLine(bic,upper_left_corner_point,lower_left_corner_point);

       //The next statement draws a line segment with end points lower_left_corner_point and ending_point.
       displayarea.DrawLine(bic,lower_left_corner_point,lower_right_corner_point);

       displayarea.DrawLine(bic, lower_right_corner_point, end_point);
      // displayarea.DrawLine(bic, p0x, start_point);

       //Display the title in larger than normal font.
       displayarea.DrawString(welcome_message,welcome_style,welcome_paint_brush,welcome_location);

       //The next statement outputs the ball using the ball's current coordinates.
       displayarea.FillEllipse (brushx1,
                               (int)System.Math.Round(x),
                               (int)System.Math.Round(y),
                               (int)System.Math.Round(2.0*radius),
                               (int)System.Math.Round(2.0*radius));
       base.OnPaint(a);
   }

   protected void Refresh_user_interface(System.Object sender, ElapsedEventArgs even)  //See Footnote #2
   {Invalidate();
   }//End of event handler Refresh_user_interface

//====================================================================================================

   //This next function computes and updates coordinates.  How did the author know how to update the x and y coordinates of the ball.
   //Answer: Draw lots of diagrams of lines with a ball on top of the line.  It required a lot of diagrams to get the right
   //to update the coordinates especially on the corners.
   protected void Update_ball_coordinates(System.Object sender, ElapsedEventArgs even)
   {//This function is called each time the ball_update_clock makes one tic.  That clock is often called the animation clock.
    if(System.Math.Abs(y+radius-p0y)<0.5)    //Test if the ball is on the top horizontal line segment.
        {if(System.Math.Abs(x+radius-(double)p1x)>delta)  //Test if there is room to move forward
              {//If condition is true then move the ball by amount delta to the left.
               x -= delta;
              }
         else
              {//If condition is false make the ball move around the corner and start traveling down.
               y = (double)p1y+(delta-(x+radius-(double)p1x));
               x = (double)p1x-radius;
              }
        }//End of if
    else if(System.Math.Abs(x+radius-(double)p1x)<0.5)  //If this is true then the ball is on the line segment from upper_left_corner_point to lower_left_corner_point
        {if(System.Math.Abs((double)p2y-(y+radius))>delta)
              {//If condition is true then move the ball by amount delta downward.
               y = y+delta;
              }
         else
              {//If condition is false then move the ball around the corner and begin traveling right.
               x = (double)p2x+(delta-((double)p2y-(y+radius)));
               y = (double)p2y-radius;
              }//End of most recent else
        }//end of else if
      else if(System.Math.Abs(y+radius-p3y)<0.5)
            {if(System.Math.Abs(x+radius-(double)p3x)>delta)
                  {
                   x += delta;
                  }
             else
                  {
                   y = (double)p4y+(delta-(x+radius-(double)p4x));
                   x = (double)p4x-radius;
                  }
            }
      else if(System.Math.Abs(x+radius-(double)p0x)<0.5)  //If this is true then the ball is on the line segment from lower_right_corner_point to start
                {if(System.Math.Abs((double)p4y-(y+radius))>delta)
                      {//If condition is true then move the ball by amount delta downward.
                       y = y-delta;
                      }
                 else
                      {//If condition is false then move the ball around the corner and begin traveling right.
                       x = (double)p0x+(delta-((double)p0y-(y+radius)));
                       y = (double)p0y-radius;
                      }//End of most recent else
                }



         else
              {//If the condition is false then distance between the ball and the destination point (p3x,p3y) is less than delta.  Make one last move and stop.
               x = (double)p4x;
               y = (double)p4y-radius;
               user_interface_refresh_clock.Enabled = false;
               ball_update_clock.Enabled = false;
               go_and_pause_button.Text = "Done";
               go_and_pause_button.Enabled = false;
               System.Console.WriteLine("The program has finished.  You may close the window.");
              }//End of else part

        }

    //End of nested ifs
   //End of method Update_ball_coordinates

//============================================================

   protected void Go_stop(System.Object sender, EventArgs even)
   {if(clocks_are_stopped)
         {//Start the refresh clock running.
          user_interface_refresh_clock.Enabled = true;

          //Start the animation clock running.
          ball_update_clock.Enabled = true;

          //Change the message on the button
          go_and_pause_button.Text = "Pause";
         }
    else
         {//Stop the refresh clock.
          user_interface_refresh_clock.Enabled = false;

          //Stop the animation clock running.
          ball_update_clock.Enabled = false;

          //Change the message on the button
          go_and_pause_button.Text = "Go";
         }
    //Toggle the variable clocks_are_stopped to be its negative
    if(crawl.Checked){
      brushx1 = Brushes.Green;
      refresh_clock_speed = 20;
    }
    else if(walk.Checked){
      brushx1 = Brushes.Orange;
      refresh_clock_speed = 50;
    }
    else if(run.Checked){
      brushx1 = Brushes.Purple;
      refresh_clock_speed = 90;
    }
    else if(sprint.Checked){
      brushx1 = Brushes.Red;
      refresh_clock_speed = 160;
    }
    clocks_are_stopped = !clocks_are_stopped;


   }//End of event handler Go_stop

//==============================================================

   protected void Close_window(System.Object sender, EventArgs even)
   {System.Console.WriteLine("This program will close its window and end execution.");
    Close();
   }//End of event handler Go_stop

}//End of class Straight_line_form
