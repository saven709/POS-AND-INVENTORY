@echo off
echo === MariaDB Clean Initialization ===
set BASE_DIR=%~dp0
set DATA_DIR=%BASE_DIR%\data
set MYSQL_DIR=%BASE_DIR%\mysql

echo Cleaning any existing data directory...
if exist "%DATA_DIR%" (
    rmdir /S /Q "%DATA_DIR%"
)
mkdir "%DATA_DIR%"

echo Installing MariaDB database...
"%MYSQL_DIR%\bin\mysql_install_db" --datadir="%DATA_DIR%"

echo Starting MariaDB server for system table setup...
start /b "MariaDB Server" "%MYSQL_DIR%\bin\mysqld" --datadir="%DATA_DIR%" --console --port=3307

echo Waiting for MariaDB to start...
timeout /t 10 /nobreak

echo Testing connection...
"%MYSQL_DIR%\bin\mysql" -u root -P 3307 -e "SELECT 'Connection test';"
if %ERRORLEVEL% NEQ 0 (
    echo Failed to connect to MariaDB after initialization!
    echo Check for errors above.
    exit /b 1
)

echo MariaDB initialized successfully.
echo Stopping MariaDB...
"%MYSQL_DIR%\bin\mysqladmin" -u root -P 3307 shutdown

pause