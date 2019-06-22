using System;
using System.Drawing;
using System.Timers;
using System.Windows.Forms;
public class uifile:Form{

private const int form_width = 1270;
private const int form_height = 930;
private const int bottomPanel_Location_height = 800;
private double earthLocation = System.Math.Round(form_height/2.5);


private Button start = new Button();
private Button pause = new Button();
private Button clear = new Button();
private Button exit = new Button();

private Pen pens = new Pen (Color.Black, 2);
private Brush writing_tool = new SolidBrush(System.Drawing.Color.Black);
private Font style_of_message = new System.Drawing.Font("Arial",16,FontStyle.Regular);

private Point pointWidth;
private Point pointHeight;
private Point bottomPanel_Location_point1;
private Point bottomPanel_Location_point2;

private Label apples_caught_label = new Label();
private Label success_ratio_label = new Label();
private Label apples_missed_label = new Label();

private Point apples_caught_text_location;
private Point success_ratio_text_location;
private Point apples_missed_text_location;

private double apples_intial_amount;
private double apples_so_far;
private double apples_caught=0;
private double apples_missed=0;
private double success_ratio =0.0;

private String apples_caught_string = "";
private String apples_missed_string = "";
private String success_ratio_string = "";

private static System.Timers.Timer ball_refresh_clock = new System.Timers.Timer();
private const double ball_refresh_clock_rate = 43.5;

private static System.Timers.Timer monitor_refresh_clock = new System.Timers.Timer();
private const double monitor_refresh_clock_rate = 23.3;

private double ball_linear_speed_pix_per_sec;
private double ball_linear_speed_pix_per_tic;
//private double speed;

private double ball_center_coordinate_x;
private double ball_center_coordinate_y;
//private double ball_direction_x;
private double ball_direction_y;


//private double ball_delta_x;
private double ball_delta_y;

private const double radius = 12.0;
private double ball_bottom_left_x;
private double ball_bottom_left_y;

private const double ball_center_initial_coordinate_x = (double)form_width*.5;
private const double ball_center_initial_coordinate_y = 0.0;

private bool ballvisible = true;
private bool ballcaught = false;

private double cursor_x =0.0;
private double cursor_y = 0.0;
private Random rng = new Random();


public uifile(){

  apples_intial_amount = rng.Next(5,10);
  System.Console.WriteLine("initial amount of apples: " + apples_intial_amount);
  ball_linear_speed_pix_per_sec = 5;
  //ball_direction_x = 0;
  ball_direction_y = 13;

  ball_linear_speed_pix_per_tic = ball_linear_speed_pix_per_sec/ball_refresh_clock_rate;
  ball_delta_y = ball_linear_speed_pix_per_tic*ball_direction_y;

  ball_center_coordinate_x = ball_center_initial_coordinate_x;
  ball_center_coordinate_y = ball_center_initial_coordinate_y;

  ball_refresh_clock.Enabled = false;   //set to false to stop the clock from running as soon as the uifile function is called
  ball_refresh_clock.Elapsed += new ElapsedEventHandler(update_ball_position);    //every time the clock happens to tick, it calls Update_ball_position

  monitor_refresh_clock.Enabled = false;  //set to false to stop the clock from running as soon as the uifile function is called
  monitor_refresh_clock.Elapsed += new ElapsedEventHandler(update_display); //every time the clock happens to tick it calls update_monitor

  Size = new Size(form_width, form_height);
  BackColor = Color.LightPink;

  pointWidth = new Point(form_width, 0);
  pointHeight = new Point(0, form_height);
  bottomPanel_Location_point1 = new Point(0, bottomPanel_Location_height-55);
  bottomPanel_Location_point2 = new Point(form_width,bottomPanel_Location_height-55);

  int a=0;
  int b=0;
  start.Location = new Point(a+= 10, b+= bottomPanel_Location_height -10);
  start.BackColor = Color.Salmon;
  start.Text = "start";

  clear.Location = new Point (a, b + start.Height + 10);
  clear.BackColor = Color.Salmon;
  clear.Text = "clear";

  pause.Location = new Point(a += start.Width + 10, b);
  pause.BackColor = Color.Salmon;
  pause.Text = "pause";

  a = form_width - 190;
  exit.Location = new Point(a, b + pause.Height +10);
  exit.BackColor = Color.Salmon;
  exit.Text = "exit";

  a = form_width - 190 - exit.Width - 10;
  success_ratio_label.Location = new Point(a-= 45, b + pause.Height);
  success_ratio_text_location = new Point(a + (apples_caught_label.Width/2), b+ pause.Height+success_ratio_label.Height);
  success_ratio_label.Text = "Success ratio";

  apples_caught_label.Location = new Point(a, b-=25);
  apples_caught_text_location = new Point(a + (apples_caught_label.Width/2), b+apples_caught_label.Height);
  apples_caught_label.Text = "Apples caught";


  apples_missed_label.Location = new Point(a+= apples_caught_label.Width+ 10, b);
  apples_missed_text_location = new Point(a + (apples_missed_label.Width/2), b+ apples_missed_label.Height);
  apples_missed_label.Text = "Apples missed";


  exit.Click += new EventHandler(quit_click);
  start.Click += new EventHandler(start_click);

  //add the buttons to the control
  Controls.Add(start);
  Controls.Add(pause);
  Controls.Add(clear);
  Controls.Add(exit);

  Controls.Add(apples_caught_label);
  Controls.Add(success_ratio_label);
  Controls.Add(apples_missed_label);


}//end of uifile function

protected void start_click(Object sender, EventArgs e){
  start_graphics(monitor_refresh_clock_rate);
  start_ball_motion(ball_refresh_clock_rate);
  start.Text = "pause";
}

protected void quit_click(Object sender, EventArgs e){
  System.Console.WriteLine("now exiting program.....");
  Close();
}



protected override void OnPaint(PaintEventArgs ee){
  Graphics g = ee.Graphics;

  ball_bottom_left_x = ball_center_coordinate_x - radius;
  ball_bottom_left_y = ball_center_coordinate_y - radius;

  g.DrawLine(pens, bottomPanel_Location_point1, bottomPanel_Location_point2);


  apples_missed_string = apples_missed.ToString();
  g.DrawString(apples_missed_string, style_of_message, writing_tool, apples_missed_text_location);

  apples_caught_string = apples_caught.ToString();
  g.DrawString(apples_caught_string,style_of_message,writing_tool,apples_caught_text_location);

  success_ratio = apples_caught/apples_so_far;
  System.Math.Round(success_ratio, 2);
  success_ratio= success_ratio *100;
  System.Math.Round(success_ratio, 2);

  success_ratio_string = success_ratio.ToString();
  g.DrawString(success_ratio_string, style_of_message, writing_tool, success_ratio_text_location);

  if(ballvisible){
    g.FillEllipse(Brushes.Red, (int)ball_bottom_left_x, (int)ball_bottom_left_y, (float)(2.0*radius), (float)(2.0*radius));
  }//end of if statement

  base.OnPaint(ee);
}//end of OnPaint
protected override void OnMouseDown(MouseEventArgs me){
cursor_y = me.Y;
cursor_x = me.X;
if(cursor_x < ball_center_coordinate_x + radius && cursor_y < ball_center_coordinate_y + radius){
  ballvisible = false;
  ballcaught = true;
  apples_caught += 1;
  apples_so_far += 1;
  Invalidate();
}//end of if statement

}//end of OnMouseDown

protected void start_graphics(double monitor_rate){
  double actual_refresh_rate = 1.0;
  double elapsed_time_between_tics;
  if(monitor_rate > actual_refresh_rate){
    actual_refresh_rate = monitor_rate;
  }
  elapsed_time_between_tics = 1000.0/actual_refresh_rate;
  monitor_refresh_clock.Interval = (int)System.Math.Round(elapsed_time_between_tics);
  monitor_refresh_clock.Enabled = true;
}//end of start_graphics

protected void start_ball_motion(double update_rate){
  double elapsed_time_between_ball_moves;
  if(update_rate < 1.0) {update_rate = 1.0;}
  elapsed_time_between_ball_moves = 1000.0/update_rate;
  ball_refresh_clock.Interval = (int)System.Math.Round(elapsed_time_between_ball_moves);
  ball_refresh_clock.Enabled = true;
}//end of start_ball_motion

protected void update_display(System.Object sender, ElapsedEventArgs evt){  //this is called after update ball position
  Invalidate();
  if(!ball_refresh_clock.Enabled){
  monitor_refresh_clock.Enabled = false;
  System.Console.WriteLine("The graphical interface is no longer refreshing. You may now exit the program");
  start.Text = "Start";
}//end of if statment
}//end of Update_display

protected void update_ball_position(System.Object sender, ElapsedEventArgs evt){  //this is called first
  ball_center_coordinate_y += ball_delta_y; //this makes the balls move down the screen

  if((int)System.Math.Round(ball_center_coordinate_y + radius) >= bottomPanel_Location_height){   //this statement is supposed to see when the ball reaches the bottomPanel_Location_height and when the ball reaches it, add it to apples_missed
  //these statements may not be valid because we have to also make another condition that says if the ball has been clicked on before this condition is reached, it should not be considered as apples_missed
    ballvisible = false;
    ballcaught = false;
    apples_missed += 1;
    apples_so_far += 1;
    Invalidate();
  }

/*test case scenarios for now, delete later when we have multiple balls based off of the rng*/
if(apples_caught == 1){
  monitor_refresh_clock.Enabled = false;
  ball_refresh_clock.Enabled = false;
  System.Console.WriteLine("the clock has stopped. you may exit the program now");
}
if(apples_missed == 1){
  monitor_refresh_clock.Enabled = false;
  ball_refresh_clock.Enabled = false;
  System.Console.WriteLine("the clock has stopped. you may exit the program now");
}
//end of temp test case secnarios

}//end of update_ball_position



}//end of form class
