MsBuild.exe %~dp0\PhuDinh.sln /m /t:Build /p:Configuration=Release /p:TargetFramework=v4.0

set d=%date:/=-%
set t=%time::=_%
set t=%t:,=_%
set Rar="C:\Program Files\WinRAR\rar.exe"
set OutputName="%d% %t%"

rd /s /q %~dp0\\PhuDinhConsole\bin\release\log
rd /s /q %~dp0\PhuDinh\bin\release\log
cd %~dp0\PhuDinh\bin\
%Rar% a -hpnobita C:\Users\Administrator\OneDrive\server\db\release\%OutputName%.rar Release
cd %~dp0