CREATE TABLE [dbo].[tMatHang] (
    [Ma]              INT            IDENTITY (1, 1) NOT NULL,
    [MaLoai]          INT            NOT NULL,
    [TenMatHang]      NVARCHAR (200) NOT NULL,
    [SoKy]            INT            CONSTRAINT [DF_tMatHang_SoKy] DEFAULT ((0)) NOT NULL,
    [SoMet]           INT            CONSTRAINT [DF_tMatHang_SoMet] DEFAULT ((0)) NOT NULL,
    [TenMatHangDayDu] NVARCHAR (200) CONSTRAINT [DF_tMatHang_TenMatHangDayDu] DEFAULT ('') NOT NULL,
    [TenMatHangIn]    NVARCHAR (50)  CONSTRAINT [DF_tMatHang_TenMatHangIn] DEFAULT ('') NOT NULL,
    CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED ([Ma] ASC),
    CONSTRAINT [FK_tMatHang_rLoaiHang] FOREIGN KEY ([MaLoai]) REFERENCES [dbo].[rLoaiHang] ([Ma])
);







