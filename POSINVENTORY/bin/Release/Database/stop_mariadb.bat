@echo off
echo === Stopping MariaDB Server ===
set BASE_DIR=%~dp0
set MYSQL_DIR=%BASE_DIR%\mysql

echo Shutting down MariaDB server...
"%MYSQL_DIR%\bin\mysqladmin" -u root -P 3307 shutdown

if %ERRORLEVEL% NEQ 0 (
  echo Could not shut down MariaDB gracefully. Forcing termination...
  taskkill /F /IM mysqld.exe /T
) else (
  echo MariaDB server shut down successfully.
)