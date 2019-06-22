using System;
using System.Drawing;
using System.Timers;
using System.Windows.Forms;
public class uifile:Form{
  /*
  ((((((((((((((((((((((((((((((((((((((((((((((what needs to be done))))))))))))))))))))))))))))))))))))))))))))))
  [x] = done
  []  = not done

[]  need to resize the bottom panel width to specifications
[]  need to make TextBox for the appropiate labels and input values asked for






  (((((((((((((((((((((((((((((((((((((((((((((((((((((((()(end)))))))))))))))))))))))))))))))))))))))))))))))))))))))*/
  private Button newbutton = new Button();
  private Button start = new Button();
  private Button quit = new Button();
  private Label pixelSpeed = new Label();
  private Label directionDeg = new Label();
  private Label xCoordinate = new Label();
  private Label yCoordinate = new Label();
  private Label CoordinateTitle = new Label();

  private Label RefreshTitle = new Label();
  private TextBox RefreshTitleInput = new TextBox();    //done

  private Label xCoordinatebox = new Label();
  //private TextBox xCoordinateInput = new TextBox();

  private Label yCoordinatebox = new Label();
  private TextBox yCoordinateInput = new TextBox();

  private Label directionDegbox = new Label();
  private TextBox directionDegInput = new TextBox();

  private Brush brushes = new SolidBrush(System.Drawing.Color.Pink);
  private Pen pens = new Pen (Color.SlateGray, 2);

  private const int formheight = 900;
  private const int formwidth = 850;
  private const int bottomPanelHeight = 180;
  private int bottomPanel_Location=0;

  private const double ball_center_initial_coordinate_x = (double)formwidth*0.65;
  private const double ball_center_initial_coordinate_y = (double)formheight/2.0+ 80;
  private double ball_center_coordinate_x;
  private double ball_center_coordinate_y;
  private double ball_upper_left_x;
  private double ball_upper_left_y;
  private const double radius = 8.5;
  private double ball_delta_x;
  private double ball_delta_y;
  private double ball_direction_x;
  private double ball_direction_y;

  private double ball_linear_speed_pix_per_sec;
  private double ball_linear_speed_pix_per_tic;



  private Point panelHeight;
  private Point panelWidth;
  //stuff
  //data for motion ball
  private static System.Timers.Timer ball_refresh_clock = new System.Timers.Timer();
  private const double ball_refresh_clock_rate = 43.5; //measured in Hz

  private static System.Timers.Timer monitor_refresh = new System.Timers.Timer();
  private const double monitor_refresh_rate =  23.3;  //will refresh 23.3 times per second


  //next we need to make the corners of the square/rectangle which encapsulates the ball as it moves



public uifile(double speed, double v, double w){
  //declarations


  ball_linear_speed_pix_per_sec = speed;
  ball_direction_x = v;
  ball_direction_y = w;

  ball_linear_speed_pix_per_tic = ball_linear_speed_pix_per_sec/ball_refresh_clock_rate;
  double hyp_squared = ball_direction_x*ball_direction_x + ball_direction_y*ball_direction_y;
  double hyp = System.Math.Sqrt(hyp_squared);
  ball_delta_x = ball_linear_speed_pix_per_tic*ball_direction_x / hyp;
  ball_delta_y = ball_linear_speed_pix_per_tic*ball_direction_y / hyp;

  ball_center_coordinate_x = ball_center_initial_coordinate_x;
  ball_center_coordinate_y = ball_center_initial_coordinate_y;

  ball_refresh_clock.Enabled = false;
  ball_refresh_clock.Elapsed += new ElapsedEventHandler(Update_ball_position);

  monitor_refresh.Enabled = false;
  monitor_refresh.Elapsed += new ElapsedEventHandler(Update_display);




  Size = new Size(formwidth, formheight);
  BackColor = Color.MistyRose;
  bottomPanel_Location = formheight - bottomPanelHeight;
  int a=0;  //maybe need?

  newbutton.Location = new Point(a += 10, bottomPanel_Location+20);
  newbutton.BackColor = Color.LightPink;
  newbutton.Text = "New";

  start.Location = new Point ( a += newbutton.Width + 45, bottomPanel_Location+20);
  start.BackColor = Color.LightPink;
  start.Text = "Start";

  quit.Location = new Point (a += start.Width + 45, bottomPanel_Location+20);
  quit.BackColor = Color.LightPink;
  quit.Text = "Quit";

  int b = 0;
  RefreshTitle.Location = new Point (b += 10, b += bottomPanel_Location + newbutton.Height + 30);
  RefreshTitle.BackColor = Color.SeaShell;
  RefreshTitle.Text = "Refresh rate (Hz)";
  RefreshTitleInput.Location = new Point(10,b+ RefreshTitle.Height + 10);
  RefreshTitleInput.BackColor = Color.SeaShell;
  RefreshTitleInput.Text = "XX.XX";

  int c=0;
  pixelSpeed.Location = new Point(c += RefreshTitle.Width + 45, b);
  pixelSpeed.BackColor = Color.SeaShell;
  pixelSpeed.Text = "Speed (pixel/second)";
  pixelSpeed.Size = new Size(pixelSpeed.Width + 20, pixelSpeed.Height);



  directionDeg.Location = new Point(c+= pixelSpeed.Width + 45 , b);
  directionDeg.BackColor = Color.SeaShell;
  directionDeg.Text = "Direction (degrees)";
  directionDeg.Size = new Size(directionDeg.Width + 10, directionDeg.Height);

  int xcoordsize =0;
  xCoordinate.Location = new Point (c+= directionDeg.Width + 50, b);
  xCoordinate.BackColor = Color.SeaShell;
  xCoordinate.Text = "  X = ";
  xCoordinate.Size = new Size(xcoordsize += xCoordinate.Width-60, xCoordinate.Height);




  yCoordinate.Location = new Point (c, b+ xCoordinate.Height + 10);
  yCoordinate.BackColor = Color.SeaShell;
  yCoordinate.Text = "  Y = ";
  yCoordinate.Size = new Size(yCoordinate.Width -60, yCoordinate.Height);

  xCoordinatebox.Location = new Point(c += xCoordinate.Width + 10, b);
  xCoordinatebox.BackColor = Color.SeaShell;
  xCoordinatebox.Text = "" + ball_center_coordinate_x;
  xCoordinate.Size = new Size(xcoordsize, xCoordinatebox.Height);

  yCoordinatebox.Location = new Point(c, b+yCoordinate.Height+ 10);
  yCoordinatebox.BackColor = Color.SeaShell;
  yCoordinatebox.Text = ""+ ball_center_coordinate_y;
  yCoordinatebox.Size = new Size(xcoordsize, yCoordinate.Height);





  panelHeight = new Point(0, bottomPanel_Location);
  panelWidth = new Point (formwidth, bottomPanel_Location);




//adding the stuff to the controls
  Controls.Add(newbutton);
  Controls.Add(start);
  Controls.Add(quit);
  Controls.Add(RefreshTitle);
  Controls.Add(RefreshTitleInput);  //to do
  Controls.Add(pixelSpeed);
  Controls.Add(directionDeg);
  Controls.Add(xCoordinate);
  Controls.Add(yCoordinate);
  Controls.Add(xCoordinatebox);
  Controls.Add(yCoordinatebox);



  quit.Click += new EventHandler(quit_click);
  start.Click += new EventHandler(start_click);


  //newbutton.Click += new EventHandler(newbutton_click);

//text box inputs will be done and reassigned in the newbutton button


  //other stuff
}//end of class uifile

protected void quit_click(Object sender, EventArgs e){
  Close();
}

protected override void OnPaint(PaintEventArgs ee){
  Graphics g = ee.Graphics;
//  g.DrawRectangle(pens, formwidth, bottomPanel_Location, formwidth, bottomPanelHeight);
  g.DrawLine(pens, panelWidth, panelHeight);
  g.DrawRectangle(pens, 0, bottomPanel_Location, formwidth, formheight);
  g.FillRectangle(brushes, 0, bottomPanel_Location, formwidth, formheight);

  ball_upper_left_x = ball_center_coordinate_x - radius;
  ball_upper_left_y = ball_center_coordinate_y - radius;
  g.FillEllipse(Brushes.Red, (int)ball_upper_left_x, (int)ball_upper_left_y, (float)(2.0*radius), (float)(2.0*radius));
  base.OnPaint(ee);

}

protected void start_click(Object sender, EventArgs e){
  start_graphics(monitor_refresh_rate);
  start_ball_motion(ball_refresh_clock_rate);
  start.Text = "pause";
}

protected void start_graphics(double monitor_rate){
  double actual_refresh_rate = 1.0;
  double elapsed_time_between_tics;
  if(monitor_rate > actual_refresh_rate){
    actual_refresh_rate = monitor_rate;
  }
  elapsed_time_between_tics = 1000.0/actual_refresh_rate;
  monitor_refresh.Interval = (int)System.Math.Round(elapsed_time_between_tics);
  monitor_refresh.Enabled = true;
}//end of start_graphics

protected void start_ball_motion(double update_rate){
  double elapsed_time_between_ball_moves;
  if(update_rate < 1.0) {update_rate = 1.0;}
  elapsed_time_between_ball_moves = 1000.0/update_rate;
  ball_refresh_clock.Interval = (int)System.Math.Round(elapsed_time_between_ball_moves);
  ball_refresh_clock.Enabled = true;
}//end of start_ball_motion

protected void Update_display(System.Object sender, ElapsedEventArgs evt){  //this is called after update ball position
  Invalidate();
if(!ball_refresh_clock.Enabled){
  monitor_refresh.Enabled = false;
  System.Console.WriteLine("The graphical interface is no longer refreshing. You may now exit the program");
  start.Text = "Start";
}//end of if statment
}//end of Update_display

protected void Update_ball_position(System.Object sender, ElapsedEventArgs evt){  //this is called first
  ball_center_coordinate_x += ball_delta_x;
  ball_center_coordinate_y -= ball_delta_y;

if((int)System.Math.Round(ball_center_coordinate_x + radius) >= formwidth)
    {ball_delta_x = -ball_delta_x;}
if((int)System.Math.Round(ball_center_coordinate_x - radius) <= 0)
    ball_delta_x = -ball_delta_x;
if((int)System.Math.Round(ball_center_coordinate_y + radius) <= 0)
    ball_delta_y = -ball_delta_y;
if((int)System.Math.Round(ball_center_coordinate_y - radius) >= bottomPanel_Location)
    ball_delta_y = -ball_delta_y;

xCoordinatebox.Text = "" + (double)System.Math.Round(ball_center_coordinate_x,2);
yCoordinatebox.Text = "" + (double)System.Math.Round(ball_center_coordinate_y,2);

}






}//end of public Form
