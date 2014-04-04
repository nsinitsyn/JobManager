SC STOP "JobManager"
SC DELETE "JobManager"
C:\Windows\Microsoft.NET\Framework\v4.0.30319\installutil.exe JobManager.WinService.exe
SC START "JobManager"
pause