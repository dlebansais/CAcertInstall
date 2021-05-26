@echo off
"%1\CAcertInstall.exe" --uninstall
echo Cleanup started, waiting...
PING -n 10 -w 1000 127.1 > NUL
echo Done.