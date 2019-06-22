echo First remove old binary files
rm *.dll
rm *.exe

echo View the list of source files
ls -l

echo Compile Selectui.cs to create the file: Selectui.dll
mcs -target:library -r:System.Drawing.dll -r:System.Windows.Forms.dll  -out:Selectui.dll Selectui.cs

echo Compile Driver.cs and link the previous created dll files to create an executable file.
mcs -r:System -r:System.Windows.Forms -r:Selectui.dll -out:App0.exe main.cs

echo View the list of files in the current folder
ls -l

echo Run the Assignment 1 program.
./App0.exe

echo The script has terminated.
