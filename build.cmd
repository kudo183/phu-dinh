MsBuild.exe %~dp0\PhuDinh.sln /m /t:Build /p:Configuration=Release /p:TargetFramework=v4.0

set d=%date:/=-%
set t=%time::=_%
set t=%t:,=_%
set Rar="C:\Program Files\WinRAR\rar.exe"
set OutputName="%d% %t%.rar"

%Rar% a -hpnobita C:\Users\Administrator\OneDrive\server\db\release\%OutputName%.rar %~dp0\PhuDinh\bin\Release
