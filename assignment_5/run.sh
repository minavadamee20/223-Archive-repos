echo First remove old binary files
rm *.dll
rm *exe

echo view the list of source files
 ls -l

 echo compile the uifile.cs to create the file uifile.dll
 mcs -target:library -r:System.Drawing.dll -r:System.Windows.Forms -out:uifile.dll uifile.cs


 #more stuff here

 echo compile the run.sh and link the pervious created dll files to create an executable file
 mcs -r:System.Windows.Forms -r:System.Drawing.dll -r:uifile.dll -out:application5.exe main.cs

 echo view the list of the files in the folder
 ls -l

 echo Run the Assingment4 program
 ./application5.exe

 echo [THE SCRIPT HAS ENDED NOW]
