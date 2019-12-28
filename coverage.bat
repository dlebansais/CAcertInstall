rem @echo off

if not exist ".\packages\OpenCover.4.7.922\tools\OpenCover.Console.exe" goto error_console1
if "%WINAPPDRIVER_DIR%" == "" goto error_console2
if not exist "%WINAPPDRIVER_DIR%/WinAppDriver.exe" goto error_console2
if not exist ".\CAcertInstall\bin\x64\Debug\CAcertInstall.exe" goto error_console3
if not exist ".\CAcertInstall\bin\x64\Release\CAcertInstall.exe" goto error_console3
if exist .\CAcertInstall\obj\x64\Debug\Coverage-CAcertInstall-Debug_coverage.xml del .\CAcertInstall\obj\x64\Debug\Coverage-CAcertInstall-Debug_coverage.xml
if exist .\CAcertInstall\obj\x64\Release\Coverage-CAcertInstall-Release_coverage.xml del .\CAcertInstall\obj\x64\Release\Coverage-CAcertInstall-Release_coverage.xml
".\packages\OpenCover.4.7.922\tools\OpenCover.Console.exe" -register:Path64 -target:".\CAcertInstall\bin\x64\Debug\CAcertInstall.exe" -targetargs:"-uninstall" -output:".\CAcertInstall\obj\x64\Debug\Coverage-CAcertInstall-Debug_coverage.xml" -showunvisited
".\packages\OpenCover.4.7.922\tools\OpenCover.Console.exe" -register:Path64 -target:".\CAcertInstall\bin\x64\Debug\CAcertInstall.exe" -output:".\CAcertInstall\obj\x64\Debug\Coverage-CAcertInstall-Debug_coverage.xml" -showunvisited
rem ".\packages\OpenCover.4.7.922\tools\OpenCover.Console.exe" -register:user -target:".\CAcertInstall\bin\x64\Debug\CAcertInstall.exe" -targetargs:"" -filter:"+[CAcertInstall*]* -[CAcertInstall*]*" -output:".\CAcertInstall\obj\x64\Debug\Coverage-CAcertInstall-Debug_coverage.xml" -showunvisited
if exist .\CAcertInstall\obj\x64\Debug\Coverage-CAcertInstall-Debug_coverage.xml .\packages\Codecov.1.9.0\tools\codecov -f ".\CAcertInstall\obj\x64\Debug\Coverage-CAcertInstall-Debug_coverage.xml" -t "a5fa6e76-5ff8-4ef6-87f4-6d681ef5b1e9"
if exist .\CAcertInstall\obj\x64\Release\Coverage-CAcertInstall-Release_coverage.xml .\packages\Codecov.1.9.0\tools\codecov -f ".\CAcertInstall\obj\x64\Release\Coverage-CAcertInstall-Release_coverage.xml" -t "a5fa6e76-5ff8-4ef6-87f4-6d681ef5b1e9"
goto end

:error_console1
echo ERROR: OpenCover.Console not found.
goto end

:error_console2
echo ERROR: WinAppDriver not found.
goto end

:error_not_built
echo ERROR: CAcertInstall.dll not built (both Debug and Release are required).
goto end

:end
