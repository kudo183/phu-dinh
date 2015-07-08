CREATE TABLE [dbo].[tChuyenHang] (
    [Ma]                     INT      IDENTITY (1, 1) NOT NULL,
    [MaNhanVienGiaoHang]     INT      NOT NULL,
    [Ngay]                   DATE     NOT NULL,
    [Gio]                    TIME (0) CONSTRAINT [DF_tChuyenHang_Gio] DEFAULT (getdate()) NULL,
    [TongDonHang]            INT      CONSTRAINT [DF_tChuyenHang_TongDonHang] DEFAULT ((0)) NOT NULL,
    [TongSoLuongTheoDonHang] INT      CONSTRAINT [DF_tChuyenHang_TongSoLuongTheoDonHang] DEFAULT ((0)) NOT NULL,
    [TongSoLuongThucTe]      INT      CONSTRAINT [DF_tChuyenHang_TongSoLuongThucTe] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_ChuyenHang] PRIMARY KEY CLUSTERED ([Ma] ASC),
    CONSTRAINT [FK_tChuyenHang_rNhanVienGiaoHang] FOREIGN KEY ([MaNhanVienGiaoHang]) REFERENCES [dbo].[rNhanVien] ([Ma])
);







