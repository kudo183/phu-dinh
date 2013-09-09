CREATE TABLE [dbo].[tChuyenHang] (
    [Ma]                 INT      IDENTITY (1, 1) NOT NULL,
    [MaNhanVienGiaoHang] INT      NOT NULL,
    [Ngay]               DATE     NOT NULL,
    [Gio]                TIME (0) NULL,
    CONSTRAINT [PK_ChuyenHang] PRIMARY KEY CLUSTERED ([Ma] ASC),
    CONSTRAINT [FK_tChuyenHang_rNhanVienGiaoHang] FOREIGN KEY ([MaNhanVienGiaoHang]) REFERENCES [dbo].[rNhanVien] ([Ma])
);



