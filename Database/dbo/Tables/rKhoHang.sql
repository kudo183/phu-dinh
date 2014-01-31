CREATE TABLE [dbo].[rKhoHang] (
    [Ma]        INT            IDENTITY (1, 1) NOT NULL,
    [TenKho]    NVARCHAR (200) NOT NULL,
    [TrangThai] BIT            CONSTRAINT [DF_rKhoHang_TrangThai] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_rKhoHang] PRIMARY KEY CLUSTERED ([Ma] ASC)
);

