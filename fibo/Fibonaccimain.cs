//****************************************************************************************************************************
//Program name: "Fibonacci Number Computing System".  This programs accepts a non-negative integer from the user and then    *
//outputs the Fibonacci number corresponding to that integer.                                                                *
//Copyright (C) 2017  Floyd Holliday                                                                                         *
//This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License  *
//version 3 as published by the Free Software Foundation.                                                                    *
//This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied         *
//warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.     *
//A copy of the GNU General Public License v3 is available here:  <https://www.gnu.org/licenses/>.                           *
//****************************************************************************************************************************







//Ruler:=1=========2=========3=========4=========5=========6=========7=========8=========9=========0=========1=========2=========3**

//Author: Floyd Holliday
//Mail: holliday@fullerton.edu

//Program name: Fibonacci Number Computing System
//Programming language: C Sharp
//Date development of program began: 2014-Aug-20
//Date of last update: 2018-Aug-01

//Purpose:  This program will compute a Fibonacci number given its numeric position in the sequence of all Fibonacci numbers.

//Files in project: fibonaccimain.cs, fibonaccilogic.cs, fibuserinterface.cs

//This file's name: Fibonaccimain.cs
//This file purpose: This is the top level module; it launches the user interface window.
//Date last modified: 2018-August-20

//Known issues: This program does not validate incoming data.  Entering a nonsense string will crash the program.

//To compile fibonaccilogic.cs:   
//     mcs -target:library -out:Fibonaccilogic.dll Fibonaccilogic.cs
//To compile fibuserinterface.cs: [The next line uses a continuation to the following line]
//     mcs -target:library -r:System.Drawing.dll -r:System.Windows.Forms.dll -r:Fibonaccilogic.dll -out:Fibuserinterface.dll Fibuserinterface.cs
//To compile  and link Fibonaccimain.cs:    
//     mcs -r:System -r:System.Windows.Forms -r:Fibuserinterface.dll -out:Fibo.exe Fibonaccimain.cs
//To execute this program:
//     ./Fibo.exe

//Hardcopy of source files: The sources files of this program are best printed using 7-point monospaced font in protrait orientation.
//
using System;
//using System.Drawing;
using System.Windows.Forms;  //Needed for "Application" on next to last line of Main
public class Fibonaccimain
{  static void Main(string[] args)
   {System.Console.WriteLine("Welcome to the Main method of the Fibonacci program.");
    Fibuserinterface fibapp = new Fibuserinterface();
    Application.Run(fibapp);
    System.Console.WriteLine("Main method will now shutdown.");
   }//End of Main
}//End of Fibonaccimain
