CREATE TABLE [dbo].[rPhuongTien] (
    [Ma]            INT            IDENTITY (1, 1) NOT NULL,
    [TenPhuongTien] NVARCHAR (200) NOT NULL,
    CONSTRAINT [PK_LoaiPhuongTien] PRIMARY KEY CLUSTERED ([Ma] ASC)
);

