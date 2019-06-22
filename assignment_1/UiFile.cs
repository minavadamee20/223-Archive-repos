/*========================================================================================================================================================================

          NOTE:
              In order to keep the circle sizes true to their original values of 200px,400px,600px I have extended the graphical graphareawidth and graphareaheight to
              fit the the circles to 1400px wide and 1400px long. I can send in a different UiFile.cs that has smaller dimensions for the circle so they may appear for
              smaller screens

==========================================================================================================================================================================*/
using System;
using System.Drawing;
using System.Windows.Forms;
public class UiFile:Form{
//add controls
      //color add controls
  private Button rfirst = new Button();
  private Button gsecond = new Button();
  private Button bthird = new Button();
      //size add controls
  private Button smallsize = new Button();
  private Button mediumsize = new Button();
  private Button bigsize = new Button();
      //functionality add controls
  private Button enterbutton = new Button();
  private Button clearbutton = new Button();
  private Button exitbutton = new Button();
      //default colors
  Color color200 = Color.Pink;
  Color color400 = Color.Pink;
  Color color600 = Color.Pink;
  Color c        = Color.Pink;
  int radius    = 200;

//`~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~``
public UiFile(){
  BackColor = Color.Pink;

//set the captions for the buttons
  Text = "Assignment 1: Drawing Circles";

  rfirst.Text = "Red";
  gsecond.Text = "Green";
  bthird.Text = "Blue";

  smallsize.Text = "200 px";
  mediumsize.Text = "400 px";
  bigsize.Text = "600 px";

  enterbutton.Text = "Enter";
  clearbutton.Text = "Clear";
  exitbutton.Text = "Exit";

  //select the size of the Window
  Size = new Size(1500,1500);

  //set location of buttons
  int b =1275;
  rfirst.Location = new Point (50, b);
  gsecond.Location = new Point (50, b += 5 + rfirst.Height);
  bthird.Location = new Point (50, b += 5 + gsecond.Height); //385 + rfirst.Height + 5);

  int q = 1275;
  smallsize.Location = new Point(60+ rfirst.Width +10, q);
  mediumsize.Location = new Point (60+ gsecond.Width+10, q += 5 + mediumsize.Height);
  bigsize.Location = new Point (60+bthird.Width+10, q+= 5 + mediumsize.Height);

  int z = 1275;
  enterbutton.Location = new Point (150 + smallsize.Width + 20, z);
  clearbutton.Location = new Point(150 + mediumsize.Width + 20, z += 5 + enterbutton.Height);
  exitbutton.Location = new Point (150 + bigsize.Width+ 20, z += 5 + clearbutton.Height);



  //add controls to the Form
  Controls.Add(rfirst);
  Controls.Add(gsecond);
  Controls.Add(bthird);
  Controls.Add(smallsize);
  Controls.Add(mediumsize);
  Controls.Add(bigsize);
  Controls.Add(enterbutton);
  Controls.Add(clearbutton);
  Controls.Add(exitbutton);
  //end of add controls to the form

  //EventHandler
  gsecond.Click += new EventHandler(Greenclick);
  rfirst.Click += new EventHandler(RedClick);
  bthird.Click += new EventHandler(BlueClick);

  smallsize.Click += new EventHandler(smallClick);
  mediumsize.Click += new EventHandler(mediumClick);
  bigsize.Click += new EventHandler(largeClick);

  exitbutton.Click += new EventHandler(exitClick);
  //need to do
  enterbutton.Click += new EventHandler(DrawFunction);
  clearbutton.Click += new EventHandler(clearClick);

}//end of UiFile public

//set buttons for radius size
protected void smallClick(Object sender, EventArgs e){radius = 200;}
protected void mediumClick(Object sender, EventArgs e){radius = 400;}
protected void largeClick(Object sender, EventArgs e){radius = 600;}

//set buttons for color
protected void Greenclick(Object sender, EventArgs e){c=Color.Green;}
protected void BlueClick(Object sender, EventArgs e){c=Color.Blue;}
protected void RedClick(Object sender, EventArgs e){c= Color.Red;}



//switch case that handles which radius is clicked on and assigns the color
protected void DrawFunction(Object sender, EventArgs e){   //dummy parameters
    switch(radius)
    {
      case 200:
        color200=c;
        break;
      case 600:
        color600=c;
        break;
      case 400:
        color400=c;
        break;
    }
    Invalidate(); //this calls the OnPaint function.
  }

  //draws onto the screen
  protected override void OnPaint(PaintEventArgs anything){
    Graphics area = anything.Graphics;
    System.Drawing.Rectangle r = new System.Drawing.Rectangle();

      //makes a copy of the class Graphics
    Pen pen = new Pen(Brushes.Pink,3);
    pen.Color = color200;   //this gives us something to write with
    r = CircleAlgorithms.getcircleinfo(1500,1260,200);
    area.DrawEllipse(pen, r); //we're doubling to make the diameter

    pen.Color = color400;
    r = CircleAlgorithms.getcircleinfo(1500,1260,400);
    area.DrawEllipse(pen, r);

    pen.Color = color600;
    r = CircleAlgorithms.getcircleinfo(1500,1260,600);
    area.DrawEllipse(pen, r);




    // Create solid brush.
    SolidBrush blueBrush = new SolidBrush(Color.SkyBlue);

    // Create rectangle.
    Rectangle rect = new Rectangle(0, 1260, 1500, 1500);

    // Fill rectangle to screen.
    anything.Graphics.FillRectangle(blueBrush, rect);
    //area.FillRectangle(pen, 0,360,1000,900);
    base.OnPaint(anything);
  }

  protected void exitClick(Object sender, EventArgs e){
    System.Console.WriteLine("This program will end execution.");
        Close();
  }


//handles switching the colors back to "clear" or in this case back to pink
  protected void clearClick(Object sender, EventArgs e){
    switch(radius){
      case 200:
        color200=Color.Pink;
        break;
      case 400:
        color400=Color.Pink;
        break;
      case 600:
        color600 = Color.Pink;
        break;
    }
    Invalidate();
  }


}//end of UiFile form
