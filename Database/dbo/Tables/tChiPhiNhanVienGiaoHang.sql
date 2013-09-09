CREATE TABLE [dbo].[tChiPhiNhanVienGiaoHang] (
    [Ma]                 INT            IDENTITY (1, 1) NOT NULL,
    [MaNhanVienGiaoHang] INT            NOT NULL,
    [MaLoaiChiPhi]       INT            NOT NULL,
    [SoTien]             INT            NOT NULL,
    [Ngay]               DATE           NOT NULL,
    [GhiChu]             NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_ChiPhiNhanVienGiaoHang] PRIMARY KEY CLUSTERED ([Ma] ASC),
    CONSTRAINT [FK_tChiPhiNhanVienGiaoHang_rLoaiChiPhi] FOREIGN KEY ([MaLoaiChiPhi]) REFERENCES [dbo].[rLoaiChiPhi] ([Ma]),
    CONSTRAINT [FK_tChiPhiNhanVienGiaoHang_rNhanVienGiaoHang] FOREIGN KEY ([MaNhanVienGiaoHang]) REFERENCES [dbo].[rNhanVien] ([Ma])
);



