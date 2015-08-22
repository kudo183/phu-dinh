CREATE TABLE [dbo].[tGiamTruKhachHang] (
    [Ma]          INT            IDENTITY (1, 1) NOT NULL,
    [MaKhachHang] INT            NOT NULL,
    [Ngay]        DATE           NOT NULL,
    [SoTien]      INT            NOT NULL,
    [GhiChu]      NVARCHAR (300) NOT NULL,
    CONSTRAINT [PK_tGiamTruKhachHang] PRIMARY KEY CLUSTERED ([Ma] ASC),
    CONSTRAINT [FK_tGiamTruKhachHang_rKhachHang] FOREIGN KEY ([MaKhachHang]) REFERENCES [dbo].[rKhachHang] ([Ma])
);


GO

create TRIGGER [dbo].[tGiamTruKhachHang_trUpdateCongNo]
	ON [dbo].[tGiamTruKhachHang]
	after DELETE, INSERT, UPDATE
	AS
	BEGIN
		SET NOCOUNT ON

		update cn
		set cn.SoTien = cn.SoTien - i.SoTien
		from inserted i
		join tCongNoKhachHang cn on cn.MaKhachHang = i.MaKhachHang
		where cn.Ngay >= i.Ngay

		update cn
		set cn.SoTien = cn.SoTien + d.SoTien
		from deleted d
		join tCongNoKhachHang cn on cn.MaKhachHang = d.MaKhachHang
		where cn.Ngay >= d.Ngay

	END