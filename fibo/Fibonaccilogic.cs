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

//This file's name: Fibonaccilogic.cs
//This file purpose: This is a third level module; it is called from fibuserinterface.cs.
//Date last modified: Feb 1, 2018
//
//
//To compile fibonaccilogic.cs:   
//          mcs -target:library -out:Fibonaccilogic.dll Fibonaccilogic.cs
//
////Ref: for data types uint and ulong see Gittleman, p. 149
//
public class Fibonaccilogic
{
 public static ulong computefibonaccinumber(uint sequencenun)
   {ulong past = 0;
    ulong present = 1;
    ulong saved;
    while(sequencenun > 0)
      {saved = past+present;
       past = present;
       present = saved;
       sequencenun--;
      }
    return present;
   }//End of computefibonaccinumber

}//End of Fibonaccilogic
