using System;
using System.Drawing;
using System.Windows.Forms;
public class Selectui:Form{

  //create controls
  private Button draw= new Button();
  private RadioButton square = new RadioButton();
  private RadioButton circle = new RadioButton();
  private ComboBox color = new ComboBox();

  private Color c = Color.Yellow;

//set options
  public Selectui(){
    Text = "Select";
    draw.Text="Draw";
    color.Text= "Choose a color";
    square.Text="Square";
    circle.Text="Circle";

    //set Size
    Size = new Size(500,250);
    //set Location

    int v = 20;
    draw.Location = new Point(20,30);
    square.Location = new Point(v += 10 + draw.Width,30);
    circle.Location = new Point (v += 10 + square.Width,30);
    color.Location = new Point (v += 10 + circle.Width, 30);

    //add items to combo ComboBox
    color.Items.Add("Red");
    color.Items.Add("Green");
    color.Items.Add("Blue");

    //add controls to Form
    Controls.Add(draw);
    Controls.Add(square);
    Controls.Add(circle);
    Controls.Add(color);

    //Register event handler
    draw.Click += new EventHandler(Draw_Click);
  }

  //Display chosen shape and selected Color
  protected override void OnPaint(PaintEventArgs e){
      Graphics g = e.Graphics;
      Brush brush = new SolidBrush(c);
      if(square.Checked){g.FillRectangle(brush, 100,100,100,100);}

      else{g.FillEllipse(brush, 100,100,100,100);}
        g.FillEllipse(brush, 100,100,100,100);
      base.OnPaint(e);
    }

  //Handle button Click
  protected void Draw_Click(object sender, EventArgs e){
      if(color.SelectedItem.ToString() == "Red")
        c = Color.Red;
      else if(color.SelectedItem.ToString() == "Green")
        c = Color.Green;
      else
        c = Color.Blue;
      Invalidate();
  }
}
