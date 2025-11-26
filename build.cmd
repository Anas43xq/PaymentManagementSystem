@echo off
setlocal

:: Find MSBuild
set "MSBUILD="

:: Try Visual Studio 2022/2019/2017
for %%v in (2022 2019 2017) do (
    if exist "C:\Program Files\Microsoft Visual Studio\%%v\Community\MSBuild\Current\Bin\MSBuild.exe" (
        set "MSBUILD=C:\Program Files\Microsoft Visual Studio\%%v\Community\MSBuild\Current\Bin\MSBuild.exe"
        goto :found
    )
    if exist "C:\Program Files (x86)\Microsoft Visual Studio\%%v\Community\MSBuild\Current\Bin\MSBuild.exe" (
        set "MSBUILD=C:\Program Files (x86)\Microsoft Visual Studio\%%v\Community\MSBuild\Current\Bin\MSBuild.exe"
        goto :found
    )
)

:: Try older Visual Studio versions
for %%v in (18 17 16 15) do (
    if exist "C:\Program Files\Microsoft Visual Studio\%%v\Community\MSBuild\Current\Bin\MSBuild.exe" (
        set "MSBUILD=C:\Program Files\Microsoft Visual Studio\%%v\Community\MSBuild\Current\Bin\MSBuild.exe"
        goto :found
    )
)

:found
if "%MSBUILD%"=="" (
    echo ERROR: MSBuild not found!
    echo Please install Visual Studio or Visual Studio Build Tools.
    exit /b 1
)

echo Using MSBuild: %MSBUILD%
echo.

:: Build the solution
"%MSBUILD%" PaymentManagementSystem.sln /t:Build /p:Configuration=Debug /p:Platform="Any CPU" /m /v:minimal

if %ERRORLEVEL% EQU 0 (
    echo.
    echo ========================================
    echo Build completed successfully!
    echo ========================================
) else (
    echo.
    echo ========================================
    echo Build failed with errors!
    echo ========================================
    exit /b %ERRORLEVEL%
)

endlocal
