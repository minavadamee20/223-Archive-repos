echo First remove old binary files
rm *.dll
rm* exe

echo view the list of source files
ls -l

echo compile the Uifile.cs to create the file Uifile.dll
mcs -target:library -r:System.Drawing.dll -r:System.Windows.Forms.dll -out:Uifile.dll Uifile.cs

echo comile the run.sh and link the pervious created dll files to create an executable file
mcs -r:System.Windows.Forms -r:Uifile.dll -out:Application3.exe main.cs

echo View the list of the files in the current folder
ls -l

echo Run the Assignment3 program
./Application3.exe

echo [THE SCRIPT HAS NOW ENDED]
