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


private Pen pen_tool = new Pen(Color.Red, 1);//color, size = 1 pixels wide

private SolidBrush brushes = new SolidBrush(Color.MistyRose);
private Rectangle bottompanel = new Rectangle(0,form_height- bottomPanel_height, form_width, bottomPanel_height);
private Font font_style = new System.Drawing.Font("Times New Roman", 11, FontStyle.Regular);

//mathematical constants and others that are needed to run the function
private const double scale_factor = 100; //this is 100 pixels, will be needed to most likely seperate the tic marks on the x and y axis
private const double origin = 40.0; //the origin is located 40 pixels away from the left side of the user interace
private const double refresh_rate = 88.8; //frequency in Hertz, how many times per second is the display area repainted. so 888  times per second
private const double dot_update_rate = 55.5; //frequence measured in Herz, how many times per second is the coordinate's of the dot being updated
private const double time_converter = 1000.0; //number of miliseconds per second
//private const double delta = 0.015; //this number is free to be changed. smaller delta, smoother anmiation but slower animation, big delta, faster motion but jerky animation.

//private Point origin_center = new Point(form_width/2, form_height/2); //we assign the point to be at th center of the UI screen
private const int origin_center_x = form_width/2;
private const int origin_center_y = form_height/2;

private Point origin_center_x_point_left = new Point(0, form_height/2);
private Point origin_center_x_point_right = new Point(form_width, form_height/2);
private Point origin_center_y_point_top = new Point(form_width/2, 0);
private Point origin_center_y_point_bottom = new Point(form_width/2, form_height);


private String x_coordinate_string = "";
private String y_coordinate_string = "";

private static System.Timers.Timer ball_refresh_clock = new System.Timers.Timer();
private const double ball_refresh_clock_rate = 43.5;  //the ball will be updated 43.5 per second, meaning it will be repained 43.5 times per second

private static System.Timers.Timer monitor_refresh_clock = new System.Timers.Timer();
private const double monitor_refresh_clock_rate = 23.3;   //measured in Hz. will refresh 23.3 times per second



private const double radius_tic = 2.0; //radius of the presumed tic marks that reprsent where certain numbers are on the axis.

  //declare these const values later..like maybe at the origin and then move them according to the function that we end up choosing?
private const double ball_center_initial_coordinate_x = form_width/2;
private const double ball_center_initial_coordinate_y = form_height/2;


private double ball_center_coordinate_x;
private double ball_center_coordinate_y;

private double ball_center_coordinate_x_original;
private double ball_center_coordinate_y_original;
private double x_coordinate_rounded;
private double y_coordinate_rounded;
private int first_iteration;

private const double linear_speed = 44.5; //moving 44.5 pixels per second

//ball direction variables

private double delta = .15;
private System.Drawing.Graphics pointer_to_surface;
private System.Drawing.Bitmap bitmap_pointer;



public uifile(){
  BackColor = Color.PowderBlue;
  Size = new Size(form_width, form_height);
  bitmap_pointer = new Bitmap(form_width, form_height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
  pointer_to_surface = Graphics.FromImage(bitmap_pointer);
  initialize_bitmap();
//   ball_center_coordinate_x = ball_center_initial_coordinate_x;
//   ball_center_coordinate_y = ball_center_initial_coordinate_y;

  monitor_refresh_clock.Enabled = false;
  monitor_refresh_clock.Elapsed += new ElapsedEventHandler(update_display);

  ball_refresh_clock.Enabled = false;
  ball_refresh_clock.Elapsed += new ElapsedEventHandler(update_ball_position); //everytime this tics, we are taken to the function update+update_ball_position


  bottomPanel_Location = form_height - bottomPanel_height;
  // bottomPanel_Location_LeftPoint = new Point(0,bottomPanel_Location);
  // bottomPanel_Location_RightPoint = new Point(form_width, bottomPanel_Location);

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

protected void initialize_bitmap(){
  pointer_to_surface.Clear(System.Drawing.Color.White);
  brushes.Color = Color.MistyRose;
  pointer_to_surface.FillRectangle(brushes, bottompanel);
  System.Console.WriteLine("this has been called on");
  //drawing the x and y axis, without tic marks
  pen_tool.Color = Color.Black;
  pointer_to_surface.DrawLine(pen_tool, origin_center_x_point_left, origin_center_x_point_right);
  pointer_to_surface.DrawLine(pen_tool, origin_center_y_point_top, origin_center_y_point_bottom);
      /*as for making the tic marks, let's just make a for loop, where a certain variable holds an invisible dot that moves along the x axis and makes a tic mark at ever [n] amounts of pixels
      and after it reaches the half way point, the labels become positive or negative, whichever way the dot is going originally*/
      brushes.Color = Color.Red;
      int tic_mark_number = -15;
    for(int x = 0; x < form_width; x += 40){  //for every 40 pixels, we will add a dot and a label for the x-axis
      x_coordinate_string = tic_mark_number.ToString();
      pointer_to_surface.FillEllipse(brushes, x, origin_center_y-2, 4, 6);
      pointer_to_surface.DrawString(x_coordinate_string, font_style,brushes, x , origin_center_y + 3);
      tic_mark_number += 1;
    }
    tic_mark_number = -11;
    for(int y =0; y < form_height; y+= 40){ //for every 40 pixels we add a dot and a label for the y-axis
      y_coordinate_string = tic_mark_number.ToString();
      pointer_to_surface.FillEllipse(brushes, origin_center_x-2, y, 4, 6);
      pointer_to_surface.DrawString(y_coordinate_string, font_style, brushes, origin_center_x+3, y);
      if(tic_mark_number == 9){break;}
      tic_mark_number += 1;
    }

}

protected void go_click(Object sender, EventArgs e){ //every time this functoin (ball_refresh_clock) tics, we will be directed to the function update_ball_position;
  start_graphics(monitor_refresh_clock_rate);
  start_ball_motion(ball_refresh_clock_rate);

}

protected void pause_resume_click(Object sender, EventArgs e){
  pause_button.Text = "Resume";
}

protected void exit_click(Object sender, EventArgs e){
  System.Console.WriteLine("Now exiting program.....");
  Close();
}

protected void update_ball_position(System.Object sender, ElapsedEventArgs e){

    first_iteration +=1;
    ball_center_coordinate_x = (1+ System.Math.Cos(2*delta))*System.Math.Cos(delta);
    ball_center_coordinate_y = (1+System.Math.Cos(2*delta))*System.Math.Sin(delta);



    ball_center_coordinate_x *= 90;
    ball_center_coordinate_x += form_width/2;

    ball_center_coordinate_y *= 90;
    ball_center_coordinate_y += form_height/2;
    x_coordinate_rounded = System.Math.Round(ball_center_coordinate_x);
    y_coordinate_rounded = System.Math.Round(ball_center_coordinate_y);
    if(x_coordinate_rounded == ball_center_coordinate_x_original){
      System.Console.WriteLine("The curve is repeating itself, nothing new will be outputted");
    }

    if(first_iteration == 1){
      ball_center_coordinate_x_original = System.Math.Round(ball_center_coordinate_x);
      ball_center_coordinate_y_original = System.Math.Round(ball_center_coordinate_y);
    }
  //  System.Console.WriteLine("x-coordinate = " + ball_center_coordinate_x);

    delta += .015;
    brushes.Color = Color.HotPink;
    pointer_to_surface.FillEllipse(brushes, (float)ball_center_coordinate_x, (float)ball_center_coordinate_y, 4,4);

  }


protected override void OnPaint(PaintEventArgs ee){
  Graphics g = ee.Graphics;
    g.DrawImage(bitmap_pointer, 0,0,form_width,form_height);
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
