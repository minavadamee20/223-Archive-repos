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

//Files in project: Fibonaccimain.cs, Fibonaccilogic.cs, Fibuserinterface.cs

//This file's name: Fibuserinterface.cs
//This file purpose: This file describe the structure of the user interface window.
//Date last modified: 2018-May-20

//To compile Fibuserinterface.cs: [The next line uses a continuation to the following line]
//     mcs -target:library -r:System.Drawing.dll -r:System.Windows.Forms.dll -r:Fibonaccilogic.dll -out:Fibuserinterface.dll Fibuserinterface.cs

//Function: The Fibonacii numerical calculator.  Enter a non-negative sequence integer in the input field, then
//click on the compute button, and the result will appear as a string.
//Sample results for small input values:
//
//Input  Fib number
//  0     1
//  1     1
//  2     2
//  3     3
//  4     5
//  5     8
//  6    13
//  7    21
//  8    34
//  9    55
// 10    89

using System;
using System.Drawing;
using System.Windows.Forms;

public class Fibuserinterface: Form
{private Label title = new Label();
 private Label sequencemessage = new Label();
 private TextBox sequenceinputarea = new TextBox();
 private Label outputinfo = new Label();
 private Button computebutton = new Button();
 private Button clearbutton = new Button();
 private Button exitbutton = new Button();
 private Size maximumfibonacciinterfacesize = new Size(400,240);
 private Size minimumfibonacciinterfacesize = new Size(400,240);

 public Fibuserinterface()
   {//Set the size of the user interface box.
	System.Console.WriteLine("Welcome to the Main method of the Fibonacci program.");
    MaximumSize = maximumfibonacciinterfacesize;
    MinimumSize = minimumfibonacciinterfacesize;
    //Initialize text strings
    Text = "Fibonacci Assignment";
    title.Text = "Fibonacci Numbers";
    sequencemessage.Text = "Enter sequence number: ";
    sequenceinputarea.Text = ""; //Empty string
    outputinfo.Text = "Result will display here.";
    computebutton.Text = "Compute";
    clearbutton.Text = "Clear";
    exitbutton.Text = "Exit";

    //Set sizes
    Size = new Size(400,240);
    title.Size = new Size(120,30);
    sequencemessage.Size = new Size(150,30);
    sequenceinputarea.Size = new Size(150,30);
    outputinfo.Size = new Size(370,30);
    computebutton.Size = new Size(85,30);
    clearbutton.Size = new Size(85,30);
    exitbutton.Size = new Size(85,30);

    //Set locations
    title.Location = new Point(140,20);
    sequencemessage.Location = new Point(20,60);
    sequenceinputarea.Location = new Point(200,60);
    outputinfo.Location = new Point(20,100);
    computebutton.Location = new Point(20,150);
    clearbutton.Location = new Point(150,150);
    exitbutton.Location = new Point(270,150);

    //Associate the Compute button with the Enter key of the keyboard
    AcceptButton = computebutton;

    //Add controls to the form
    Controls.Add(title);
    Controls.Add(sequencemessage);
    Controls.Add(sequenceinputarea);
    Controls.Add(outputinfo);
    Controls.Add(computebutton);
    Controls.Add(clearbutton);
    Controls.Add(exitbutton);

    //Register the event handler.  In this case each button has an event handler, but no other
    //controls have event handlers.
    computebutton.Click += new EventHandler(computefibnumber);
    clearbutton.Click += new EventHandler(cleartext);
    exitbutton.Click += new EventHandler(stoprun);  //The '+' is required.

   }//End of constructor Fibuserinterface

 //Method to execute when the compute button receives an event, namely: receives a mouse click
 protected void computefibnumber(Object sender, EventArgs events)
   {uint sequencenun = uint.Parse(sequenceinputarea.Text);
    ulong fibnum = Fibonaccilogic.computefibonaccinumber(sequencenun);
    string output = "The corresponding Fib number is " + fibnum;
    outputinfo.Text = output;
   }
 //The following function has been relocated to the file Fibonaccilogic.cs
 //protected ulong computefibonaccinumber(uint sequencenun);
   //{ulong past = 0;
   // ulong present = 1;
   // ulong saved;
   // while(sequencenun > 0)
   //   {saved = past+present;
   //    past = present;
   //    present = saved;
   //    sequencenun--;
   //   }
   // return present;
 //}//End of computefibonaccinumber

 //Method to execute when the clear button receives an event, namely: receives a mouse click
 protected void cleartext(Object sender, EventArgs events)
   {sequenceinputarea.Text = ""; //Empty string
    outputinfo.Text = "Result will display here.";
   }//End of cleartext

 //Method to execute when the exit button receives an event, namely: receives a mouse click
 protected void stoprun(Object sender, EventArgs events)
   {Close();
   }//End of stoprun

}//End of clas Fibuserinterface
