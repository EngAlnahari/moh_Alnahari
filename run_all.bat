@echo off
echo ===================================================
echo   Starting Tanjez Projects (Backend API & Frontend)
echo ===================================================

echo Starting Backend API (TestProject)...
start "Backend API" cmd /k "cd /d %~dp0TestProject\Test && dotnet run"

echo Starting Frontend Web App (Mntry_Awqaf)...
start "Frontend Web" cmd /k "cd /d %~dp0Mntry_Awqaf\Mntry_Awqaf && dotnet run"

echo.
echo Both projects have been launched in separate windows!
echo API URL: http://localhost:5046
echo Web URL: http://localhost:5064
echo.
pause
