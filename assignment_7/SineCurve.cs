public class Sinecurve
{   private double absolute_value_of_derivative_squared;
    private double absolute_value_of_derivative;

    public void get_next_coordinates(double amp, double coef, double delta, ref double dot, out double x, out double y)
       {absolute_value_of_derivative_squared = 1+ amp*amp * coef*coef * System.Math.Cos(coef*dot)*System.Math.Cos(coef*dot);
        absolute_value_of_derivative = System.Math.Sqrt(absolute_value_of_derivative_squared);
        dot = dot + delta/absolute_value_of_derivative;
        x = dot;
        y = amp*System.Math.Sin(coef*dot);
       }//End of get_next_coordinates

}//End  of class Sinecurve
