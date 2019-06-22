using System;
using System.Drawing;
using System.Windows.Forms;
public class selectmain{
  static void Main(){
    double speed = 81.9;
    double v = 37.2;
    double w = 21.8;
    uifile userface = new uifile(speed,v,w);
    Application.Run(userface);
  }
}
