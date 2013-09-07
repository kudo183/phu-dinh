set d=%date:/=-%
set t=%time::=_%
set t=%t:,=_%
set Rar="C:\Program Files\WinRAR\rar.exe"
set OutputName="%d% %t%.bak"
sqlcmd -s (local) -i backup_phudinh_db.sql
rename "phudinh.bak" %OutputName%
%Rar% a %OutputName%.rar %OutputName%
del %OutputName%