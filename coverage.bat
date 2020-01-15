@echo off

if not exist ".\packages\OpenCover.4.7.922\tools\OpenCover.Console.exe" goto error1
if not exist ".\packages\Codecov.1.9.0\tools\codecov.exe" goto error2
if "%WINAPPDRIVER_DIR%" == "" goto error3
if not exist "%WINAPPDRIVER_DIR%/WinAppDriver.exe" goto error3
if "%VSTESTPLATFORM_DIR%" == "" goto error4
if not exist "%VSTESTPLATFORM_DIR%/VSTest.Console.exe" goto error4
if not exist ".\CAcertInstall\bin\x64\Debug\CAcertInstall.exe" goto error5

echo Administor permissions required. Detecting permissions...

net session >nul 2>&1
if %errorLevel% == 0 (
    echo Success: Administrator permissions confirmed.
) else (
    goto error6
)

if exist .\CAcertInstall\obj\x64\Debug\Coverage-Debug_coverage.xml del .\CAcertInstall\obj\x64\Debug\Coverage-Debug_coverage.xml

call .\coverage\cleanup.bat

call .\coverage\app.bat CAcertInstall Debug "--uninstall"
call .\coverage\wait.bat 20
call .\coverage\app.bat CAcertInstall Debug "--language=bad"
call .\coverage\wait.bat 20
call .\coverage\app.bat CAcertInstall Debug "bad"
call .\coverage\wait.bat 20

start cmd /k .\coverage\start_winappdriver.bat

call .\coverage\app.bat CAcertInstall Debug "--language=040C"
"%VSTESTPLATFORM_DIR%\VSTest.Console.exe" ".\Test-CAcertInstall\bin\x64\Debug\Test-CAcertInstall.dll" /Tests:TestInstall1

call .\coverage\app.bat CAcertInstall Debug "--language=0409"
"%VSTESTPLATFORM_DIR%\VSTest.Console.exe" ".\Test-CAcertInstall\bin\x64\Debug\Test-CAcertInstall.dll" /Tests:TestInstall2

call .\coverage\app.bat CAcertInstall Debug "--language=0409"
"%VSTESTPLATFORM_DIR%\VSTest.Console.exe" ".\Test-CAcertInstall\bin\x64\Debug\Test-CAcertInstall.dll" /Tests:TestInstall3

call .\coverage\app.bat CAcertInstall Debug "--language=0409"
"%VSTESTPLATFORM_DIR%\VSTest.Console.exe" ".\Test-CAcertInstall\bin\x64\Debug\Test-CAcertInstall.dll" /Tests:TestInstall4

call .\coverage\app.bat CAcertInstall Debug
call .\coverage\wait.bat 20
call .\coverage\app.bat CAcertInstall Debug "--uninstall --fail=Uninstall"
call .\coverage\wait.bat 20
call .\coverage\app.bat CAcertInstall Debug "--uninstall"
call .\coverage\wait.bat 20
call .\coverage\app.bat CAcertInstall Debug "--uninstall --fail=InstalledCheck"
call .\coverage\wait.bat 20

call .\coverage\app.bat CAcertInstall Debug "--language=0409 --fail=Install"
"%VSTESTPLATFORM_DIR%\VSTest.Console.exe" ".\Test-CAcertInstall\bin\x64\Debug\Test-CAcertInstall.dll" /Tests:TestInstall5

start cmd /c .\coverage\stop_winappdriver.bat

call .\coverage\restore.bat

call ..\Certification\set_tokens.bat
if exist .\CAcertInstall\obj\x64\Debug\Coverage-Debug_coverage.xml .\packages\Codecov.1.9.0\tools\codecov -f ".\CAcertInstall\obj\x64\Debug\Coverage-Debug_coverage.xml" -t "%CACERTINSTALL_CODECOV_TOKEN%"
goto end

:error1
echo ERROR: OpenCover.Console not found. Restore it with Nuget.
goto end

:error2
echo ERROR: Codecov not found. Restore it with Nuget.
goto end

:error3
echo ERROR: WinAppDriver not found. Example: set WINAPPDRIVER_DIR=C:\Program Files\Windows Application Driver
goto end

:error4
echo ERROR: Visual Studio 2019 not found. Example: set VSTESTPLATFORM_DIR=C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\Common7\IDE\Extensions\TestPlatform
goto end

:error5
echo ERROR: CAcertInstall.dll not built.
goto end

:error6
echo ERROR: This command line console must be run as administrator.
goto end

:end
