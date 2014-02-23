CREATE TABLE [dbo].[tChuyenKho] (
    [Ma]            INT  IDENTITY (1, 1) NOT NULL,
    [MaNhanVien]    INT  NOT NULL,
    [MaKhoHangXuat] INT  NOT NULL,
    [MaKhoHangNhap] INT  NOT NULL,
    [Ngay]          DATE NOT NULL,
    CONSTRAINT [PK_tChuyenKho] PRIMARY KEY CLUSTERED ([Ma] ASC),
    CONSTRAINT [FK_tChuyenKho_rKhoHangNhap] FOREIGN KEY ([MaKhoHangNhap]) REFERENCES [dbo].[rKhoHang] ([Ma]),
    CONSTRAINT [FK_tChuyenKho_rKhoHangXuat] FOREIGN KEY ([MaKhoHangXuat]) REFERENCES [dbo].[rKhoHang] ([Ma]),
    CONSTRAINT [FK_tChuyenKho_rNhanVien] FOREIGN KEY ([MaNhanVien]) REFERENCES [dbo].[rNhanVien] ([Ma])
);

