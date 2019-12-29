@echo off
".\CAcertInstall\bin\x64\Debug\CAcertInstall.exe" --uninstall
echo Cleanup started, waiting...
PING -n 10 -w 1000 127.1 > NUL
echo Done.