echo First remove old binary files
rm *.dll
rm* exe

echo view the list of source files
ls -l


echo compile the algorithm.cs to create the file algorithm.dll
mcs -target:library -r:System.Drawing.dll -out:algorithm.dll algorithm.cs

echo compile UiFile.cs and then link algorithm.cs to UiFile.cs
mcs -target:library -r:algorithm.dll -r:System.Drawing.dll -r:System.Windows.Forms.dll -out:UiFile.dll UiFile.cs


#echo complile uifile.cs to create the file: uifile.dll
#mcs -target:library -r:System.Drawing.dll -r:algorithm.dll -r:System.Windows.Forms.dll  -out:UiFile.dll UiFile.cs

echo Compile Driver.sh and link the previous created dll files to create an executable file
mcs -r:System -r:System.Windows.Forms -r:UiFile.dll  -out:App1.exe main.cs

echo View the list of the files in the current folder
ls -l

echo Run the Assignment1 program
./App1.exe

echo The script has terminated
