@echo off
echo Application: %1
echo Parameter: %5
echo Logs path: %~2\%4\Coverage-%4_coverage.xml
if not exist "%~2\%4\Coverage-%4_coverage.xml" goto nomerge
set MERGE=-mergeoutput
:nomerge
start "%1" /B ".\packages\%3\tools\OpenCover.Console.exe" -register:Path64 -target:".\%1\bin\x64\%4\net48\win-x64\%1.exe" -targetargs:%5 -output:"%~2\%4\Coverage-%4_coverage.xml" %MERGE%
set MERGE=
