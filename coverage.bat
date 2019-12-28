rem @echo off

if not exist ".\packages\OpenCover.4.7.922\tools\OpenCover.Console.exe" goto error_console1
if "%WINAPPDRIVER_DIR%" == "" goto error_console2
if not exist "%WINAPPDRIVER_DIR%/WinAppDriver.exe" goto error_console2
if not exist ".\CACertInstall\bin\x64\Debug\CACertInstall.exe" goto error_console3
if not exist ".\CACertInstall\bin\x64\Release\CACertInstall.exe" goto error_console3
if exist .\CACertInstall\obj\x64\Debug\Coverage-CACertInstall-Debug_coverage.xml del .\CACertInstall\obj\x64\Debug\Coverage-CACertInstall-Debug_coverage.xml
if exist .\CACertInstall\obj\x64\Release\Coverage-CACertInstall-Release_coverage.xml del .\CACertInstall\obj\x64\Release\Coverage-CACertInstall-Release_coverage.xml
start cmd.exe @cmd /k "%WINAPPDRIVER_DIR%/WinAppDriver.exe"
pause
".\packages\OpenCover.4.7.922\tools\OpenCover.Console.exe" -register:user -target:".\CACertInstall\bin\x64\Debug\CACertInstall.exe" -targetargs:"" -filter:"+[CACertInstall*]* -[CACertInstall*]*" -output:".\CACertInstall\obj\x64\Debug\Coverage-CACertInstall-Debug_coverage.xml" -showunvisited
if exist .\CACertInstall\obj\x64\Debug\Coverage-CACertInstall-Debug_coverage.xml .\packages\Codecov.1.9.0\tools\codecov -f ".\CACertInstall\obj\x64\Debug\Coverage-CACertInstall-Debug_coverage.xml" -t "a5fa6e76-5ff8-4ef6-87f4-6d681ef5b1e9"
if exist .\CACertInstall\obj\x64\Release\Coverage-CACertInstall-Release_coverage.xml .\packages\Codecov.1.9.0\tools\codecov -f ".\CACertInstall\obj\x64\Release\Coverage-CACertInstall-Release_coverage.xml" -t "a5fa6e76-5ff8-4ef6-87f4-6d681ef5b1e9"
goto end

:error_console1
echo ERROR: OpenCover.Console not found.
goto end

:error_console2
echo ERROR: WinAppDriver not found.
goto end

:error_not_built
echo ERROR: CACertInstall.dll not built (both Debug and Release are required).
goto end

:end
