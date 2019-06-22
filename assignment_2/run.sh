echo First remove old binary files
rm *.dll
rm* exe

echo view the list of source files
ls -l


echo compile the algorithm.cs to create the file algorithm.dll
mcs -target:library -r:System.Drawing.dll -out:algorithm.dll algorithm.cs

echo compile Uifile and then link algorithm.cs to UiFile.cs
mcs -target:library -r:System.Drawing.dll -r:System.Windows.Forms.dll -r:algorithm.dll -out:Uifile.dll Uifile.cs


#more stuff will go here as we make more cs files ^^^^

echo Compile the Run.sh and link the pervious created dll files to create an executable file
mcs -r:System.Windows.Forms -r:Uifile.dll -out:Application2.exe main.cs

echo View the list of the files in the current folder
ls -l

echo Run the Assignment2 program
./Application2.exe

echo [THE SCRIPT HAS ENDED]
