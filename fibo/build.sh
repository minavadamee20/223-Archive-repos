echo First remove old binary files
rm *.dll
rm *.exe

echo View the list of source files
ls -l

echo Compile Fibonaccilogic.cs to create the file: Fibonaccilogic.dll
mcs -target:library -out:Fibonaccilogic.dll Fibonaccilogic.cs

echo Compile Fibuserinterface.cs to create the file: Fibuserinterface.dll
mcs -target:library -r:System.Drawing.dll -r:System.Windows.Forms.dll -r:Fibonaccilogic.dll -out:Fibuserinterface.dll Fibuserinterface.cs

echo Compile Fibonaccimain.cs and link the two previously created dll files to create an executable file.
mcs -r:System -r:System.Windows.Forms -r:Fibuserinterface.dll -out:Fibo.exe Fibonaccimain.cs

echo View the list of files in the current folder
ls -l

echo Run the Assignment 1 program.
./Fibo.exe

echo The script has terminated.
