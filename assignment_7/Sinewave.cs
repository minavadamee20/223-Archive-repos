using System;
using System.Windows.Forms;            //Needed for "Application" near the end of Main function.
public class Sinewave
{  public static void Main()
   {  System.Console.WriteLine("The Sine Wave program has begun.");
      Sineframe sineapp = new Sineframe();
      Application.Run(sineapp);
      System.Console.WriteLine("This Sine Wave program has ended.  Bye.");
   }//End of Main function
}//End of Sinewave class
