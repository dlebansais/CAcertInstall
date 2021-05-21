@echo off
echo Application: %1
echo Parameter: %3
if not exist ".\%1\obj\x64\%2\Coverage-%2_coverage.xml" goto nomerge
set MERGE=-mergeoutput
:nomerge
start "%1" /B ".\packages\OpenCover.4.7.1189\tools\OpenCover.Console.exe" -register:Path64 -target:".\%1\bin\x64\%2\%1.exe" -targetargs:%3 -output:".\%1\obj\x64\%2\Coverage-%2_coverage.xml" %MERGE%
set MERGE=
