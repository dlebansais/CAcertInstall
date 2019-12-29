rem @echo off

if not exist ".\packages\OpenCover.4.7.922\tools\OpenCover.Console.exe" goto error_console1
if "%WINAPPDRIVER_DIR%" == "" goto error_console2
if not exist "%WINAPPDRIVER_DIR%/WinAppDriver.exe" goto error_console2
if "%VSTESTPLATFORM_DIR%" == "" goto error_console3
if not exist "%VSTESTPLATFORM_DIR%/VSTest.Console.exe" goto error_console3
if not exist ".\CAcertInstall\bin\x64\Debug\CAcertInstall.exe" goto error_not_built
if not exist ".\CAcertInstall\bin\x64\Release\CAcertInstall.exe" goto error_not_built

if exist .\CAcertInstall\obj\x64\Debug\Coverage-CAcertInstall-Debug_coverage.xml del .\CAcertInstall\obj\x64\Debug\Coverage-CAcertInstall-Debug_coverage.xml
if exist .\CAcertInstall\obj\x64\Release\Coverage-CAcertInstall-Release_coverage.xml del .\CAcertInstall\obj\x64\Release\Coverage-CAcertInstall-Release_coverage.xml

start cmd /k .\coverage\start_winappdriver.bat

".\packages\OpenCover.4.7.922\tools\OpenCover.Console.exe" -register:Path64 -target:"%VSTESTPLATFORM_DIR%\VSTest.Console.exe" -targetargs:".\Test-CAcertInstall\bin\x64\Debug\Test-CAcertInstall.dll" -output:".\CAcertInstall\obj\x64\Debug\Coverage-CAcertInstall-Debug_coverage.xml" -showunvisited

if exist .\CAcertInstall\obj\x64\Debug\Coverage-CAcertInstall-Debug_coverage.xml .\packages\Codecov.1.9.0\tools\codecov -f ".\CAcertInstall\obj\x64\Debug\Coverage-CAcertInstall-Debug_coverage.xml" -t "a5fa6e76-5ff8-4ef6-87f4-6d681ef5b1e9"
if exist .\CAcertInstall\obj\x64\Release\Coverage-CAcertInstall-Release_coverage.xml .\packages\Codecov.1.9.0\tools\codecov -f ".\CAcertInstall\obj\x64\Release\Coverage-CAcertInstall-Release_coverage.xml" -t "a5fa6e76-5ff8-4ef6-87f4-6d681ef5b1e9"

start cmd /c .\coverage\stop_winappdriver.bat
goto end

:error_console1
echo ERROR: OpenCover.Console not found.
goto end

:error_console2
echo ERROR: WinAppDriver not found.
goto end

:error_console3
echo ERROR: WinAppDriver not found.
goto end

:error_not_built
echo ERROR: CAcertInstall.dll not built (both Debug and Release are required).
goto end

:end
