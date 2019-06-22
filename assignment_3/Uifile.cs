using System;
using System.Drawing;
using System.Timers;
using System.Windows.Forms;
public class Uifile:Form{

  private bool clocks_are_stopped = true;

    //Declare clocks
    private static System.Timers.Timer user_interface_refresh_clock = new System.Timers.Timer();
    private static System.Timers.Timer ball_update_clock = new System.Timers.Timer();



private const int max_width = 900;
private const int max_height = 900;

private Button Go = new Button();
private Button Reset = new Button();
private Button Stop = new Button();

private Label speedBoxLabel = new Label();
private TextBox SpeedBox = new TextBox();
private double Hzspeed;

private Pen otherpen = new Pen(Color.Green, 2);
private Pen pens = new Pen(Color.HotPink,(int)System.Math.Round(line_width));

private const double radius = 7.5;
private const double line_width = 2.0;
private double x;
private double y;
private const double delta = 8.972;


private int p0x;
private int p0y;
private int p1x;
private int p1y;
private int p2x;
private int p2y;
private int p3x;
private int p3y;

private Point starting_point;
private Point upper_left;
private Point lower_left;
private Point end_point;
//end will = starting_point





  public Uifile(){

    starting_point = new Point(p1x += max_width-20, p1y += max_height -880);


    BackColor = Color.MistyRose;
    Text = "Assignment 3";

    Go.Text = "Go";
    Reset.Text = "Reset";
    Stop. Text = "Stop";

    Size = new Size (max_width, max_height);  //size of the form or window

    //Configure speedbox input box
        //NOTE: need to make a rectangle that supplies more information about speedbox in ui
    speedBoxLabel.Text = "Enter speed in Hz: ";
    speedBoxLabel.Size = new Size(speedBoxLabel.Width + 10, speedBoxLabel.Height + 3);
    speedBoxLabel.Location = new Point(110, max_height - 150);
    speedBoxLabel.BackColor = Color.LightPink;

    int a = 110 + speedBoxLabel.Width;
    SpeedBox.Location = new Point (a += 5, max_height-150);
    SpeedBox.BackColor = Color.Snow;
    SpeedBox.Text = "enter here";

    Go.Location = new Point(a += SpeedBox.Width + 20, max_height - 150);
    Go.BackColor = Color.LightPink;

    Reset.Location = new Point(a += Go.Width + 10, max_height - 150);
    Reset.BackColor = Color.LightPink;

    Stop.Location = new Point(a += Reset.Width + 10, max_height - 150);
    Stop.BackColor = Color.LightPink;

    AcceptButton = Go;

    Controls.Add(SpeedBox);
    Controls.Add(speedBoxLabel);
    Controls.Add(Go);
    Controls.Add(Reset);
    Controls.Add(Stop);

    Stop.Click += new EventHandler(stopclick);
    Go.Click += new EventHandler(goclick);

    p0x = max_width - 110;
    p0y = max_height - 10;
    starting_point = new Point (p0x, p0y);





  }//end of public UI function
protected void stopclick(Object sender, EventArgs e){
System.Console.WriteLine("This program will now end execution");
Close();
}

protected void goclick(Object sender, EventArgs e){
  Hzspeed = Double.Parse(SpeedBox.Text);
  {if(clocks_are_stopped)
        {//Start the refresh clock running.
         user_interface_refresh_clock.Enabled = true;

         //Start the animation clock running.
         ball_update_clock.Enabled = true;

         //Change the message on the button
        Go.Text = "Pause";
        }
   else
        {//Stop the refresh clock.
         user_interface_refresh_clock.Enabled = false;

         //Stop the animation clock running.
         ball_update_clock.Enabled = false;

         //Change the message on the button
         Go.Text = "Go";
        }
   //Toggle the variable clocks_are_stopped to be its negative
   clocks_are_stopped = !clocks_are_stopped;
  }//End of event handler Go_stop
}

protected override void OnPaint(PaintEventArgs a){
  Graphics displaystuff = a.Graphics;
  //displaystuff.DrawLine(otherpen,0,top_panel_height,Width,top_panel_height);

       displaystuff.FillRectangle(Brushes.Yellow,0,max_height-80,Width,80);
       displaystuff.DrawLine(otherpen,0, max_height-180,max_width-10,max_height-180);

       //The next statement draws a line segment with end points starting_point and upper_left.
       displaystuff.DrawLine(pens,starting_point,upper_left);

       //The next statement draws a line segment with end points upper_left and lower_left.
       displaystuff.DrawLine(pens,upper_left,lower_left);

       //The next statement draws a line segment with end points lower_left and ending_point.
       displaystuff.DrawLine(pens,lower_left,end_point);

       //Display the title in larger than normal font.


       //The next statement outputs the ball using the ball's current coordinates.
       displaystuff.FillEllipse (Brushes.Blue,
                               (int)System.Math.Round(x),
                               (int)System.Math.Round(y),
                               (int)System.Math.Round(2.0*radius),
                               (int)System.Math.Round(2.0*radius));
       base.OnPaint(a);
}
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
    else if(System.Math.Abs(x+radius-(double)p1x)<0.5)  //If this is true then the ball is on the line segment from upper_left to lower_left
        {if(System.Math.Abs((double)p2y-(y+radius))>delta)
              {//If condition is true then move the ball by amount delta downward.
               y = y+delta;
              }
         else
              {//If condition is false then move the ball around the corner and begin traveling right.
               x = (double)p2x+(delta-((double)p2y-(y+radius)));
               y = (double)p2y-radius;
              }//End of most recent else
        }
    else if(System.Math.Abs(y+radius-(double)p2y)<0.5)  //If this is true then the ball is on the lower line segment traveling to the right.
        {if(System.Math.Abs((double)p3x-(x+radius))>delta)
              {//If the condition is true then move the ball right by the amount delta
               x = x + delta;
              }
         else
              {//If the condition is false then distance between the ball and the destination point (p3x,p3y) is less than delta.  Make one last move and stop.
               x = (double)p3x;
               y = (double)p3y-radius;
               user_interface_refresh_clock.Enabled = false;
               ball_update_clock.Enabled = false;
               Go.Text = "Done";
               Go.Enabled = false;
               System.Console.WriteLine("The program has finished.  You may close the window.");
              }//End of else part
        }//End of nested ifs
   }//End of method Update_ball_coordinates

//============================================================






}//end of form
