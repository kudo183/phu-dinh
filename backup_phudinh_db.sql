BACKUP
	DATABASE [PhuDinh] 
TO
	DISK = N'D:\phu_dinh\phudinh.bak' 
WITH
	COPY_ONLY,
	NOFORMAT,
	NOINIT,
	NAME = N'PhuDinh-Full Database Backup',
	SKIP, NOREWIND, NOUNLOAD,
	STATS = 10
GO
