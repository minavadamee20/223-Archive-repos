using System;
using System.Drawing;
using System.Timers;
public class Clock_Algorithms{
  public static int set_display(int mess_count, ref System.Timers.Timer mess_clock, ref Brush painting_tool, int t1, int t2, int t3){
    switch(mess_count){
      case 0: mess_clock.Interval = t1;
              painting_tool = Brushes.Red;
              break;
      case 1: mess_clock.Interval = (int)t2;
              painting_tool = Brushes.Yellow;
              break;
      case 2: mess_clock.Interval = (int)t3;
              painting_tool = Brushes.Green;
              break;
    }//end of switch statement
    return (mess_count+1)%3;
  }//end of set_display
}//end of Clock_Algorithms
