
::��װ�����ļ�
@SET TNetServiceDir=%~dp0bin\Debug\TNetService.exe



@SET FrameworkDir=%WINDIR%\Microsoft.NET\Framework

@SET FrameworkVersion=v4.0.30319

@SET PATH=%FrameworkDir%\%FrameworkVersion%;%WINDIR%\System32;%PATH%;

@SET/p setup_type=����1����װ�� 2:ж��.���겢����������

@echo off
if "%setup_type%" == "1" (
	@echo -----��װ����-----

	InstallUtil.exe %TNetServiceDir%
	
	::sc start TNetService

	pause

)   

if "%setup_type%" == "2" (
	@echo -----ж�ط���-----

	InstallUtil.exe /u  %TNetServiceDir% 

	pause

)   
 

 

