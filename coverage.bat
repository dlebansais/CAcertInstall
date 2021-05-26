@echo off
setlocal

set PROJECTNAME=LargeList
set TESTPROJECTNAME=Test-%PROJECTNAME%

set OPENCOVER_VERSION=4.7.1189
set OPENCOVER=OpenCover.%OPENCOVER_VERSION%
set CODECOV_VERSION=1.13.0
set CODECOV=Codecov.%CODECOV_VERSION%
set NUINT_CONSOLE_VERSION=3.12.0
set NUINT_CONSOLE=NUnit.ConsoleRunner.%NUINT_CONSOLE_VERSION%

set RESULTFILENAME=Coverage-%PROJECTNAME%.xml
set FRAMEWORK=net48

nuget install OpenCover -Version %OPENCOVER_VERSION% -OutputDirectory packages
if not exist ".\packages\%OPENCOVER%\tools\OpenCover.Console.exe" goto error_console1
nuget install CodeCov -Version %CODECOV_VERSION% -OutputDirectory packages
if not exist ".\packages\%CODECOV%\tools\codecov.exe" goto error_console2
nuget install NUnit.ConsoleRunner -Version %NUINT_CONSOLE_VERSION% -OutputDirectory packages
if not exist ".\packages\%NUINT_CONSOLE%\tools\nunit3-console.exe" goto error_console3

if "%WINAPPDRIVER_DIR%" == "" goto error3
if not exist "%WINAPPDRIVER_DIR%/WinAppDriver.exe" goto error3
if "%VSTESTPLATFORM_DIR%" == "" goto error4
if not exist "%VSTESTPLATFORM_DIR%/VSTest.Console.exe" goto error4

set TESTPATH=.\Test\Test-CAcertInstall\bin\x64\Debug\net48
set LOGSPATH=.\CAcertInstall\obj\x64

if not exist "%TESTPATH%\CAcertInstall.exe" goto error5
if not exist "%TESTPATH%\Test-CAcertInstall.dll" goto error5

echo Administor permissions required. Detecting permissions...

net session >nul 2>&1
if %errorLevel% == 0 (
    echo Success: Administrator permissions confirmed.
) else (
    goto error6
)

if exist "%LOGSPATH%\Debug\Coverage-Debug_coverage.xml" del "%LOGSPATH%\Debug\Coverage-Debug_coverage.xml"

call .\coverage\cleanup.bat "%TESTPATH%"

call .\coverage\app.bat CAcertInstall "%LOGSPATH%" "%OPENCOVER%" Debug "--uninstall"
call .\coverage\wait.bat 20
echo 1
call .\coverage\app.bat CAcertInstall "%LOGSPATH%" "%OPENCOVER%" Debug "--language=bad"
call .\coverage\wait.bat 20
echo 2
call .\coverage\app.bat CAcertInstall "%LOGSPATH%" "%OPENCOVER%" Debug "bad"
call .\coverage\wait.bat 20

start cmd /k .\coverage\start_winappdriver.bat

call .\coverage\app.bat CAcertInstall "%LOGSPATH%" "%OPENCOVER%" Debug "--language=040C"
"%VSTESTPLATFORM_DIR%\VSTest.Console.exe" "%TESTPATH%\Test-CAcertInstall.dll" /Tests:TestInstall1

call .\coverage\app.bat CAcertInstall "%LOGSPATH%" "%OPENCOVER%" Debug "--language=0409"
"%VSTESTPLATFORM_DIR%\VSTest.Console.exe" "%TESTPATH%\Test-CAcertInstall.dll" /Tests:TestInstall2

call .\coverage\app.bat CAcertInstall "%LOGSPATH%" "%OPENCOVER%" Debug "--language=0409"
"%VSTESTPLATFORM_DIR%\VSTest.Console.exe" "%TESTPATH%\Test-CAcertInstall.dll" /Tests:TestInstall3

call .\coverage\app.bat CAcertInstall "%LOGSPATH%" "%OPENCOVER%" Debug "--language=0409"
"%VSTESTPLATFORM_DIR%\VSTest.Console.exe" "%TESTPATH%\Test-CAcertInstall.dll" /Tests:TestInstall4

call .\coverage\app.bat CAcertInstall "%LOGSPATH%" "%OPENCOVER%" Debug
call .\coverage\wait.bat 20
call .\coverage\app.bat CAcertInstall "%LOGSPATH%" "%OPENCOVER%" Debug "--uninstall --fail=Uninstall"
call .\coverage\wait.bat 20
call .\coverage\app.bat CAcertInstall "%LOGSPATH%" "%OPENCOVER%" Debug "--uninstall"
call .\coverage\wait.bat 20
call .\coverage\app.bat CAcertInstall "%LOGSPATH%" "%OPENCOVER%" Debug "--uninstall --fail=InstalledCheck"
call .\coverage\wait.bat 20

call .\coverage\app.bat CAcertInstall "%LOGSPATH%" "%OPENCOVER%" Debug "--language=0409 --fail=Install"
echo .
echo . Decide if you want to keep certificates, and this will complete the test.
echo .
"%VSTESTPLATFORM_DIR%\VSTest.Console.exe" "%TESTPATH%\Test-CAcertInstall.dll" /Tests:TestInstall5

start cmd /c .\coverage\stop_winappdriver.bat

call .\coverage\restore.bat "%TESTPATH%"

call ..\Certification\set_tokens.bat
if exist %LOGSPATH%\Debug\Coverage-Debug_coverage.xml .\packages\%CODECOV%\tools\win7-x86\codecov.exe -f "%LOGSPATH%\Debug\Coverage-Debug_coverage.xml" -t "%CACERTINSTALL_CODECOV_TOKEN%"
goto end

:error_console1
echo ERROR: OpenCover.Console not found. Restore it with Nuget.
goto end

:error_console2
echo ERROR: Codecov not found. Restore it with Nuget.
goto end

:error_console3
echo ERROR: NUnit not found. Restore it with Nuget.
goto end

:error3
echo ERROR: WinAppDriver not found. Example: set WINAPPDRIVER_DIR=C:\Program Files\Windows Application Driver
goto end

:error4
echo ERROR: Visual Studio 2019 not found. Example: set VSTESTPLATFORM_DIR=C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\Common7\IDE\Extensions\TestPlatform
goto end

:error5
echo ERROR: "%TESTPATH%\CAcertInstall.exe not built.
goto end

:error6
echo ERROR: This command line console must be run as administrator.
goto end

:end
