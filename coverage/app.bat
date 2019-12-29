@echo off
echo Parameter: %2
start "CAcertInstall" /B ".\packages\OpenCover.4.7.922\tools\OpenCover.Console.exe" -register:Path64 -target:".\CAcertInstall\bin\x64\%1\CAcertInstall.exe" -targetargs:%2 -output:".\CAcertInstall\obj\x64\%1\Coverage-CAcertInstall-%1_coverage.xml"
