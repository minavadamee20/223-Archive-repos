using System;
using System.Drawing;
using System.Timers;
using System.Windows.Forms;
public class uifile:Form{
private Button go_button = new Button();
private Button pause_button = new Button();
private Button exit_button = new Button();

private const int form_height = 900;
private const int form_width = 1200;
private const int bottomPanel_height = 75;
private int bottomPanel_Location;


//private Pen pen_tool = new Pen(Color.Red, 1);//color, size = 1 pixels wide
private Pen tail_pen = new Pen(Color.White, 1);

private SolidBrush brushes = new SolidBrush(Color.MistyRose);
private SolidBrush star_brushes = new SolidBrush(Color.White);
private SolidBrush moon_brush = new SolidBrush(Color.Silver);
private SolidBrush grass_brush = new SolidBrush(Color.SeaGreen);
private Rectangle bottompanel = new Rectangle(0,form_height- bottomPanel_height, form_width, bottomPanel_height);
private Rectangle grassRectangle = new Rectangle(0, form_height-bottomPanel_height-grass_height, form_width, grass_height);

//mathematical constants and others that are needed to run the function
private const double scale_factor = 100; //this is 100 pixels, will be needed to most likely seperate the tic marks on the x and y axis
private const double origin = 40.0; //the origin is located 40 pixels away from the left side of the user interace
private const double refresh_rate = 88.8; //frequency in Hertz, how many times per second is the display area repainted. so 888  times per second
private const double dot_update_rate = 55.5; //frequence measured in Herz, how many times per second is the coordinate's of the dot being updated
private const double time_converter = 1000.0; //number of miliseconds per second

private const int origin_center_y = form_height/2;


private static System.Timers.Timer ball_refresh_clock = new System.Timers.Timer();
private const double ball_refresh_clock_rate = 43.5;  //the ball will be updated 43.5 per second, meaning it will be repained 43.5 times per second

private static System.Timers.Timer monitor_refresh_clock = new System.Timers.Timer();
private const double monitor_refresh_clock_rate = 23.3;   //measured in Hz. will refresh 23.3 times per second



private const double radius_tic = 2.0; //radius of the presumed tic marks that reprsent where certain numbers are on the axis.

  //declare these const values later..like maybe at the origin and then move them according to the function that we end up choosing?
private const double ball_center_initial_coordinate_x = form_width;
private const double ball_center_initial_coordinate_y = 0;


private double ball_center_coordinate_x;
private double ball_center_coordinate_y;

// private double ball_center_coordinate_x_original;
// private double ball_center_coordinate_y_original;
// private double x_coordinate_rounded;
// private double y_coordinate_rounded;
private int first_iteration;

private const double linear_speed = 44.5; //moving 44.5 pixels per second

private double delta = .005;

// private Point grass_left;
// private Point grass_right;
private const int grass_height= 175; //the grass height is 75 pixels
private int grass_location;

private int state=0;
private bool tail_state = true;
private Point tail_begin;
private Point tail_end;



public uifile(){
  BackColor = Color.DarkSlateGray;
  Size = new Size(form_width, form_height);

   ball_center_coordinate_x = ball_center_initial_coordinate_x;
   ball_center_coordinate_y = ball_center_initial_coordinate_y;
   tail_end = new Point((int)ball_center_initial_coordinate_x, 0);

  monitor_refresh_clock.Enabled = false;
  monitor_refresh_clock.Elapsed += new ElapsedEventHandler(update_display);

  ball_refresh_clock.Enabled = false;
  ball_refresh_clock.Elapsed += new ElapsedEventHandler(update_ball_position); //everytime this tics, we are taken to the function update+update_ball_position


  bottomPanel_Location = form_height - bottomPanel_height;
  grass_location = bottomPanel_Location - grass_height;

  int width_measuring_tool = 0;
  int height_measuring_tool = bottomPanel_Location + 2;

  go_button.Location = new Point(width_measuring_tool += 5, height_measuring_tool);
  go_button.BackColor = Color.Pink;
  go_button.Text = "Go";

  pause_button.Location = new Point(width_measuring_tool += 5 + go_button.Width, height_measuring_tool);
  pause_button.BackColor = Color.Pink;
  pause_button.Text = "Pause";

  exit_button.Location = new Point( form_width - 15 - exit_button.Width, height_measuring_tool);
  exit_button.BackColor = Color.Pink;
  exit_button.Text = "Exit";

  go_button.Click += new EventHandler(go_click);
  pause_button.Click += new EventHandler(pause_resume_click);
  exit_button.Click += new EventHandler(exit_click);

  Controls.Add(go_button);
  Controls.Add(pause_button);
  Controls.Add(exit_button);
}//end of public uifile function

protected void go_click(Object sender, EventArgs e){ //every time this functoin (ball_refresh_clock) tics, we will be directed to the function update_ball_position;
  start_graphics(monitor_refresh_clock_rate);
  start_ball_motion(ball_refresh_clock_rate);
  state =1;

}

protected void pause_resume_click(Object sender, EventArgs e){
  switch(state){
    case 1:
      pause_button.Text = "Resume";
      ball_refresh_clock.Enabled = false;
      monitor_refresh_clock.Enabled = false;
      state =2;
      break;
    case 2:
    pause_button.Text = "Pause";
    ball_refresh_clock.Enabled = true;
    monitor_refresh_clock.Enabled = true;
    state =1;
    break;

  }//end of switch statement


}

protected void exit_click(Object sender, EventArgs e){
  System.Console.WriteLine("Now exiting program.....");
  Close();
}

protected void update_ball_position(System.Object sender, ElapsedEventArgs e){
    //first_iteration +=1;
    ball_center_coordinate_x -= delta;
    ball_center_coordinate_y += delta;

    tail_begin = new Point((int)ball_center_coordinate_x+6, (int)ball_center_coordinate_y);

    if(ball_center_coordinate_y+7 > grass_location){
      tail_state = false;
    }
    if(!tail_state){
      delta = .005;
      ball_center_coordinate_x = ball_center_initial_coordinate_x;
      ball_center_coordinate_y = ball_center_initial_coordinate_y;
      tail_state = true;

    }
    delta += .004;
  }


protected override void OnPaint(PaintEventArgs ee){
  Graphics g = ee.Graphics;
  g.FillRectangle(brushes, bottompanel);
  g.FillRectangle(grass_brush, grassRectangle);
  g.FillEllipse(moon_brush, (float)form_width/15, (float)form_height/12, 120,120);

    if(tail_state){
        g.FillEllipse(star_brushes, (float)ball_center_coordinate_x, (float)ball_center_coordinate_y, 7,7);
        g.DrawLine(tail_pen, tail_begin, tail_end);
    }

    base.OnPaint(ee);
}

protected void start_graphics(double monitor_refresh_clock_rate) {
  //stuff: don't forget to add arg
  double actual_refresh_rate = 1.0;
  double elapsed_time_between_tics;
  if(monitor_refresh_clock_rate > actual_refresh_rate){
    actual_refresh_rate = monitor_refresh_clock_rate;
  }
  elapsed_time_between_tics = 1000.0/actual_refresh_rate;
  monitor_refresh_clock.Interval = (int)System.Math.Round(elapsed_time_between_tics);
  monitor_refresh_clock.Enabled = true;
}

protected void start_ball_motion(double update_rate){
  //stuff: don't forget to add arg
  double elapsed_time_between_ball_moves;
  if(update_rate < 1.0) {update_rate = 1.0;}
  elapsed_time_between_ball_moves = 1000.0/update_rate;
  ball_refresh_clock.Interval = (int)System.Math.Round(elapsed_time_between_ball_moves);
  ball_refresh_clock.Enabled = true;
}

protected void update_display(System.Object sender, ElapsedEventArgs e){
  Invalidate();
  if(!ball_refresh_clock.Enabled){
    monitor_refresh_clock.Enabled = false;
    System.Console.WriteLine("The graphical interface has stopped refreshing. you may not exit the program");

  }
}



}//end of public class uifile:Form
