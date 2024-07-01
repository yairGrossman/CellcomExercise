@echo off
REM Build the project (optional if the project is already built)
msbuild C:\Users\YairVM\Documents\Projects\CellcomExercise\CellcomExercise.sln

REM Navigate to the output directory
cd C:\Users\YairVM\Documents\Projects\CellcomExercise\CellcomClient\bin\Debug

REM Start each client in a new console window
start "Client COM2" cmd /k "CellcomClient.exe COM2"
start "Client COM4" cmd /k "CellcomClient.exe COM4"
start "Client COM4" cmd /k "CellcomClient.exe COM6"
start "Client COM4" cmd /k "CellcomClient.exe COM8"
start "Client COM4" cmd /k "CellcomClient.exe COM10"
pause
