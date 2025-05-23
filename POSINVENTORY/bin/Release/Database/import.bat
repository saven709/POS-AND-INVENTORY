@echo off
echo === IMPORTING DATABASE ===
set BASE_DIR=%~dp0
set MYSQL_DIR=%BASE_DIR%\mysql
set SQL_FILE=%BASE_DIR%\brewtopia_db.sql

REM CHECK IF SQL FILE EXISTS
if not exist "%SQL_FILE%" (
  echo SQL FILE NOT FOUND: %SQL_FILE%
  exit /b 1
)

REM CHECK CONNECTION TO MARIADB
echo CHECKING MARIADB CONNECTION...
"%MYSQL_DIR%\bin\mysql" -u root -P 3307 -e "SELECT 'CONNECTION SUCCESSFUL';" >nul 2>&1
if %ERRORLEVEL% NEQ 0 (
  echo CANNOT CONNECT TO MARIADB. MAKE SURE MARIADB IS RUNNING!
  exit /b 1
)

REM DROP DATABASE IF EXISTS
echo DROPPING EXISTING DATABASE...
"%MYSQL_DIR%\bin\mysql" -u root -P 3307 -e "DROP DATABASE IF EXISTS brewtopia_db;"
if %ERRORLEVEL% NEQ 0 (
  echo FAILED TO DROP DATABASE.
  exit /b 1
)

REM CREATE DATABASE AGAIN
echo CREATING DATABASE...
"%MYSQL_DIR%\bin\mysql" -u root -P 3307 -e "CREATE DATABASE brewtopia_db;"
if %ERRORLEVEL% NEQ 0 (
  echo FAILED TO CREATE DATABASE.
  exit /b 1
)

REM IMPORT DATABASE FROM SQL FILE
echo IMPORTING DATABASE FROM %SQL_FILE%...
"%MYSQL_DIR%\bin\mysql" -u root -P 3307 brewtopia_db < "%SQL_FILE%"
if %ERRORLEVEL% NEQ 0 (
  echo FAILED TO IMPORT DATABASE.
  exit /b 1
) else (
  echo DATABASE IMPORTED SUCCESSFULLY.
)
