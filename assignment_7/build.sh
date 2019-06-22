#!/bin/bash

#Author: Floyd Holliday
#Program: Dot Traveling in path of sine curve




echo First remove old binary files
rm *.dll
rm *.exe

echo View the list of source files
ls -l

echo Compile SineCurve.cs to create the file: SineCurve.dll
mcs -target:library -out:SineCurve.dll SineCurve.cs

echo Compile Sineframe.cs to create the file: Sineframe.dll
mcs -target:library -r:System.Drawing.dll -r:System.Windows.Forms.dll -r:SineCurve.dll -out:Sineframe.dll Sineframe.cs

echo Compile Sinewave.cs and link the two previously created dll files to create an executable file.
mcs -r:System -r:System.Windows.Forms -r:Sineframe.dll -out:Sine.exe Sinewave.cs

echo View the list of files in the current folder
ls -l

echo Run the Assignment 1 program.
./Sine.exe

echo The script has terminated.
