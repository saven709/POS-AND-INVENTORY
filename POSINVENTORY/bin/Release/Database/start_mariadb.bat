@echo off
echo === Starting MariaDB Server ===
set BASE_DIR=%~dp0
set DATA_DIR=%BASE_DIR%\data
set MYSQL_DIR=%BASE_DIR%\mysql

REM Check if data directory exists
if not exist "%DATA_DIR%\ibdata1" (
  echo MariaDB data directory not initialized! Please run initialize_mariadb.bat first.
  exit /b 1
)

REM Kill any existing MariaDB processes
taskkill /F /IM mysqld.exe /T >nul 2>&1

REM Start MariaDB server
echo Starting MariaDB server...
start "MariaDB Server" /B "%MYSQL_DIR%\bin\mysqld" --datadir="%DATA_DIR%" --console --port=3307

REM Wait and verify server is running
echo Waiting for MariaDB to start...
timeout /t 5 /nobreak

REM Check if MariaDB is running
"%MYSQL_DIR%\bin\mysql" -u root -P 3307 -e "SELECT 'MariaDB is running';" >nul 2>&1
if %ERRORLEVEL% NEQ 0 (
  echo Failed to start MariaDB server! Check logs for details.
  exit /b 1
) else (
  echo MariaDB server started successfully on port 3307.
)