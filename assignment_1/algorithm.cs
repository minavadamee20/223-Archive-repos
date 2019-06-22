using System;
using System.Drawing;

public class CircleAlgorithms{
  public static System.Drawing.Rectangle
  getcircleinfo(int graphareawidth, int graphareaheight, int radius){
   Point corner = new Point(graphareawidth/2 -radius, graphareaheight/2 - radius);  //the concept is we find the center of the graphical area of our field.
   Size lenwide = new Size(2*radius, 2* radius);
   Rectangle rect = new Rectangle(corner, lenwide);
   return rect;
 }//end of class
}
