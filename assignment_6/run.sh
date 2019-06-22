echo First remove old binary files
rm* .dll
rm *.exe

echo view the list of source files
ls -l

echo compile the uifile.cs to create the file uifile.dll
mcs -target:library -r:System.Drawing.dll -r:System.Windows.Forms -out:uifile.dll uifile.cs

#add more stuff here if needed

echo compiles the run.sh and link the previous created dll files to create an executeable file
mcs -r:System.Windows.Forms -r:System.Drawing.dll -r:uifile.dll -out:application6.exe main.cs

echo view the list of the files in the folder
ls -l

echo Run the Assignment6 program
./application6.exe

echo [THE SCRIPT HAS NOW ENDED... PROGRAM SHUTTING DOWN]
