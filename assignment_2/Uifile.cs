using System;
using System.Drawing;
using System.Timers;
using System.Windows.Forms;
public class Uifile:Form{
    //add controls
  private Button start = new Button();

  private RadioButton slow = new RadioButton();
  private RadioButton medium = new RadioButton();
  private RadioButton fast = new RadioButton();

  private Button pause = new Button();
  private Button exit = new Button();

  private static System.Timers.Timer message_clock = new System.Timers.Timer();
  private int message_counter = 0;
  private int temp_count =0;
  private int timeRed = 4000;
  private int timeYellow = 3000;
  private int timeGreen = 1000;
  private Brush paint_brush = new SolidBrush(System.Drawing.Color.Gray);
    //default values
  int radiusLight = 50;
  Color Graylight = Color.Gray;



public Uifile(){
  BackColor = Color.Pink;

    //add text to the buttons and title
  Text = "Assignment 2: Making a clock";
  start.Text = "Start";

  slow.Text = "slow";
  medium.Text= "medium";
  fast.Text = "fast";

  pause.Text = "Pause";
  exit.Text = "Exit";

  message_clock.Enabled = false;
  message_clock.Elapsed += new ElapsedEventHandler(manage_messages);
  message_clock.Interval = 5;
  //message_clock.Enabled = true;




      //adding button locations
  Size = new Size(700,700);
  int a = 500;
  int x = 50;
  start.Location = new Point(x,a);
  pause.Location = new Point (x, a +start.Height+10);

  slow.Location = new Point(x += 10 + start.Width, a);
  medium.Location = new Point( x += 2 + slow.Width, a);
  fast.Location = new Point (x += 2 + medium.Width, a);
  exit.Location  = new Point (x += 10+ fast.Width, a);

        //add controls to Form
  Controls.Add(start);

  Controls.Add(slow);
  Controls.Add(medium);
  Controls.Add(fast);

  Controls.Add(pause);
  Controls.Add(exit);


      //make the evenhandlers
  //times.Click += new EventHandler(times_Click);
  start.Click += new EventHandler(startClick);
  pause.Click += new EventHandler(pauseClick);
  exit.Click += new EventHandler(exitClick);

// slow.CheckedChanged += new EventHandler(Check_Changed);
// medium.CheckedChanged += new EventHandler(Check_Changed);
// fast.CheckedChanged += new EventHandler(Check_Changed);

  }
  //functions go here
protected void exitClick(Object sender, EventArgs e){
  System.Console.WriteLine("This program will end execution now");
  Close();
}
protected void startClick(Object sender, EventArgs e){
  //do timer stuff
  System.Console.WriteLine("the sytem has started");
  message_clock.Enabled = true;
  Invalidate();

}
protected void pauseClick(Object sender, EventArgs e){
  //do pause timer stuff
  System.Console.WriteLine("the system has paused. the message counter is:" + message_counter);
  message_clock.Enabled = false;
}
protected void drawingstuff(PaintEventArgs e){
  Graphics d = e.Graphics;
  Pen pens = new Pen(Color.Black, 3);
  d.DrawEllipse(pens, 250,10,150,150);  //first circle
  d.DrawEllipse(pens, 250, 170, 150,150); //second circle
  d.DrawEllipse(pens, 250,330, 150,150);  //third circle
  if(message_clock.Enabled == false){
  d.FillEllipse(paint_brush, 250, 11, 150, 150);
  d.FillEllipse(paint_brush, 250, 171,150,150);
  d.FillEllipse(paint_brush, 250, 330, 149, 149);
  }
  Invalidate();
}



protected override void OnPaint(PaintEventArgs e){
Graphics g = e.Graphics;

switch(message_counter){
  case 1:
          g.FillEllipse(paint_brush, 250, 11, 150, 150);
          g.FillEllipse(paint_brush = Brushes.Gray, 250, 171,150,150);
          g.FillEllipse(paint_brush = Brushes.Gray, 250, 330, 149, 149);
          break;
  case 2: g.FillEllipse(paint_brush, 250, 171,150,150);
          g.FillEllipse(paint_brush = Brushes.Gray, 250, 11, 150, 150);
          g.FillEllipse(paint_brush = Brushes.Gray, 250, 330, 149, 149);
          break;
  case 0: g.FillEllipse(paint_brush, 250, 330, 149, 149);
          g.FillEllipse(paint_brush = Brushes.Gray, 250, 11, 150, 150);
          g.FillEllipse(paint_brush = Brushes.Gray, 250, 171,150,150);
          break;
}

base.OnPaint(e);

}

protected void manage_messages(System.Object sender, ElapsedEventArgs ea ){
  System.Console.WriteLine("message_counter before calling the function:[ " + message_counter);
  if(slow.Checked){

    timeRed = 4000;
    timeYellow = 1000;
    timeGreen = 3000;
  }
  else if(medium.Checked){
    timeRed = 2000;
    timeYellow = 500;
    timeGreen = 1500;
  }
  else if(fast.Checked){
    timeRed = 1000;
    timeYellow = 250;
    timeGreen = 750;
  }



  message_counter = Clock_Algorithms.set_display(message_counter, ref message_clock, ref paint_brush, timeRed, timeYellow, timeGreen);
  System.Console.WriteLine("message_counter after calling the function: [ " + message_counter);
  Invalidate();

}


}//end of Ui Form




/* NOTE
  when we have the following function:
      protected override void OnPaint(PaintEventArgs e){
        Graphics g = e.Graphics;
        Pen pens = new Pen(Color.Black, 3);
        //Ellipse circle1 = new Ellipse(pens, 250,10,150,150);
        g.DrawEllipse(pens, 250,10,150,150);
        g.FillEllipse(paint_brush, 250, 11, 150, 150);

        g.DrawEllipse(pens, 250, 170, 150,150);
        g.FillEllipse(paint_brush, 250, 171,150,150);

        g.DrawEllipse(pens, 250,330, 150,150);
        g.FillEllipse(paint_brush, 250, 330, 149, 149);
        base.OnPaint(e);

      }
the colors for each circle light up instead of just one circle lighting up at it's called time.
we need to make switch statements that come from message counter to handle the switch statements
*/
