MsBuild.exe %~dp0\PhuDinh.sln /m /t:ReBuild /p:Configuration=Release /p:TargetFramework=v4.0

set d=%date:/=-%
set t=%time::=_%
set t=%t:,=_%
set Rar="C:\Program Files\WinRAR\rar.exe"

set SubWCRev=C:\Program Files\TortoiseSVN\bin\SubWCRev.exe
set WorkingCopyPath=%~dp0
for /f "tokens=5" %%i in ('""%SubWCRev%" "%WorkingCopyPath%.""') do set v=%%i

set OutputName="%d% %t% r%v%"

rd /s /q %~dp0\PhuDinhConsole\bin\release\log
rd /s /q %~dp0\PhuDinh\bin\release\log
cd /d %~dp0\PhuDinh\bin\Release\

set OutputPath=%UserProfile%\OneDrive\server\db\release\%OutputName%.rar
rem set OutputPath=D:\%OutputName%.rar

%Rar% a -hpnobita %OutputPath% PhuDinh.exe PhuDinh.exe.config

cd %~dp0