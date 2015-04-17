namespace PhuDinhCommon
{
    public class Constant
    {
        #region enum
        public enum MainMenuItems
        {
            rBaiXe,
            rChanh,
            rDiaDiem,
            rKhachHang,
            rLoaiChiPhi,
            rLoaiHang,
            rNhanVien,
            rNuoc,
            rPhuongTien,
            tChiPhi,
            tChiTietChuyenHangDonHang,
            tChiTietDonHang,
            tChuyenHang,
            tChuyenHangDonHang,
            tDonHang,
            tMatHang
        }
        #endregion

        public const string ThamSo_NgayCapNhatTonKhoCuoiCung = "NgayCapNhatTonKhoCuoiCung";

        public const string ColumnName_MaKhachHang = "Ma Khach Hang";
        public const string ColumnName_MaBaiXe = "Ma Bai Xe";
        public const string ColumnName_MaNuoc = "Ma Nuoc";
        public const string ColumnName_MaLoaiHang = "Ma Loai Hang";
        public const string ColumnName_MaChuyenHang = "Ma Chuyen Hang";
        public const string ColumnName_MaDonHang = "Ma Don Hang";
        public const string ColumnName_MaChuyenHangDonHang = "Ma Chuyen Hang Don Hang";
        public const string ColumnName_MaLoaiNguyenLieu = "Ma Loai Nguyen Lieu";
        public const string ColumnName_MaPhuongTien = "Ma Phuong Tien";
        public const string ColumnName_MaDiaDiem = "Ma Dia Diem";
        public const string ColumnName_MaMatHang = "Ma Mat Hang";
        public const string ColumnName_MaNguyenLieu = "Ma Nguyen Lieu";
        public const string ColumnName_MaNhanVien = "Ma Nhan Vien";
        public const string ColumnName_MaLoaiChiPhi = "Ma Loai Chi Phi";
        public const string ColumnName_MaNhaCungCap = "Ma Nha Cung Cap";
        public const string ColumnName_MaNhapHang = "Ma Nhap Hang";
        public const string ColumnName_MaKhoHang = "Ma Kho Hang";
        public const string ColumnName_MaKhoHangNhap = "Ma Kho Hang Nhap";
        public const string ColumnName_MaKhoHangXuat = "Ma Kho Hang Xuat";
        public const string ColumnName_MaChuyenKho = "Ma Chuyen Kho";

        public const string ColumnName_BaiXe = "Bai Xe";
        public const string ColumnName_Nuoc = "Nuoc";
        public const string ColumnName_Ngay = "Ngay";
        public const string ColumnName_KhachHang = "Khach Hang";
        public const string ColumnName_Chanh = "Chanh";
        public const string ColumnName_KhachHangChanh = "Khach Hang Chanh";
        public const string ColumnName_LoaiHang = "Loai Hang";
        public const string ColumnName_ChuyenHangDonHang = "Chuyen Hang Don Hang";
        public const string ColumnName_ChiTietDonHang = "Chi Tiet Don Hang";
        public const string ColumnName_ChuyenHang = "Chuyen Hang";
        public const string ColumnName_DonHang = "Don Hang";
        public const string ColumnName_MatHang = "Mat Hang";
        public const string ColumnName_LoaiNguyenLieu = "Loai Nguyen Lieu";
        public const string ColumnName_PhuongTien = "Phuong Tien";
        public const string ColumnName_DiaDiem = "Dia Diem";
        public const string ColumnName_NguyenLieu = "Nguyen Lieu";
        public const string ColumnName_LoaiChiPhi = "Loai Chi Phi";
        public const string ColumnName_NhanVien = "Nhan Vien";
        public const string ColumnName_NhanCungCap = "Nha Cung Cap";
        public const string ColumnName_NhapHang = "Nhap Hang";
        public const string ColumnName_KhoHang = "Kho Hang";
        public const string ColumnName_KhoHangNhap = "Kho Hang Nhap";
        public const string ColumnName_KhoHangXuat = "Kho Hang Xuat";
        public const string ColumnName_ChuyenKho = "Chuyen Kho";
        //0Level
        public const string ViewName_BaiXe = "Bai Xe";
        public const string ViewName_LoaiChiPhi = "Loai Chi Phi";
        public const string ViewName_LoaiHang = "Loai Hang";
        public const string ViewName_LoaiNguyenLieu = "Loai Nguyen Lieu";
        public const string ViewName_NhaCungCap = "Nha Cung Cap";
        public const string ViewName_Nuoc = "Nuoc";
        public const string ViewName_PhuongTien = "Phuong Tien";
        public const string ViewName_KhoHang = "Kho Hang";
        //1Level
        public const string ViewName_Chanh = "Chanh";
        public const string ViewName_DiaDiem = "Dia Diem";
        public const string ViewName_NguyenLieu = "Nguyen Lieu";
        public const string ViewName_NhanVien = "Nhan Vien";
        public const string ViewName_MatHang = "Mat Hang";
        //2Level
        public const string ViewName_CanhBaoTonKho = "Canh Bao Ton Kho";
        public const string ViewName_KhachHang = "Khach Hang";
        public const string ViewName_MatHangNguyenLieu = "Mat Hang Nguyen Lieu";
        public const string ViewName_ChiPhi = "Chi Phi";
        public const string ViewName_ChuyenHang = "Chuyen Hang";
        public const string ViewName_NhapNguyenLieu = "Nhap Nguyen Lieu";
        public const string ViewName_NhapHang = "Nhap Hang";
        public const string ViewName_TonKho = "Ton Kho";
        public const string ViewName_ChuyenKho = "Chuyen Kho";
        //3Level
        public const string ViewName_KhachHangChanh = "Khach Hang Chanh";
        public const string ViewName_ChiTietChuyenKho = "Chi Tiet Chuyen Kho";
        public const string ViewName_ChiTietNhapHang = "Chi Tiet Nhap Hang";
        public const string ViewName_DonHang = "Don Hang";
        //4Level
        public const string ViewName_ChiTietDonHang = "Chi Tiet Don Hang";
        public const string ViewName_ChuyenHangDonHang = "Chuyen Hang Don Hang";
        //5Level
        public const string ViewName_ChiTietChuyenHangDonHang = "Chi Tiet Chuyen Hang Don Hang";
        //complex
        public const string ViewName_AdminView = "AdminView";
        public const string ViewName_BaiXeView = "BaiXeView";
        public const string ViewName_ChiPhiView = "ChiPhiView";
        public const string ViewName_ChuyenHangView = "ChuyenHangView";
        public const string ViewName_ChuyenKhoView = "ChuyenKhoView";
        public const string ViewName_DonHangView = "DonHangView";
        public const string ViewName_KhachHangChanhView = "KhachHangChanhView";
        public const string ViewName_KhachHangView = "KhachHangView";
        public const string ViewName_MatHangView = "MatHangView";
        public const string ViewName_NhanVienView = "NhanVienView";
        public const string ViewName_NhapHangView = "NhapHangView";
        //report
        public const string ViewName_ReportByKhachHangView = "ReportByKhachHangView";
        public const string ViewName_ReportByLoaiHangView = "ReportByLoaiHangView";
        public const string ViewName_ReportByMatHangView = "ReportByMatHangView";
        public const string ViewName_ReportDailyView = "ReportDailyView";
        public const string ViewName_ReportTuLamView = "ReportTuLamView";
        public const string ViewName_ReportNhapHangView = "ReportNhapHangView";
        public const string ViewName_ReportByNhapNguyenLieuView = "ReportByNhapNguyenLieuView";
        public const string ViewName_ReportByChiPhiView = "ReportByChiPhiView";
    }
}
