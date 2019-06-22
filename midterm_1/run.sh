

echo First remove old binary files
rm *.dll
rm *.exe

echo View the list of source files
ls -l

echo "Compile the file ninety-degree-ui.cs:"
mcs -target:library -r:System.Drawing.dll -r:System.Windows.Forms.dll -out:Uifile.dll Uifile.cs

echo "Compile and link ninety-degree-main.cs:"
mcs -r:System -r:System.Windows.Forms -r:Uifile.dll -out:starting.exe main.cs

echo "Run the program Ninety Degree Turn"
./starting.exe

echo "The bash script has terminated."
