using PhuDinhCommon;
using System.Windows.Controls;
using PhuDinhReport;

namespace PhuDinhCommonControl
{
    public static class ViewFactory
    {
        public static UserControl CreateView(string viewName)
        {
            UserControl view = null;
            switch (viewName)
            {
                //0Level
                case Constant.ViewName_BaiXe:
                    view = new rBaiXeView();
                    break;
                case Constant.ViewName_LoaiChiPhi:
                    view = new rLoaiChiPhiView();
                    break;
                case Constant.ViewName_LoaiHang:
                    view = new rLoaiHangView();
                    break;
                case Constant.ViewName_LoaiNguyenLieu:
                    view = new rLoaiNguyenLieuView();
                    break;
                case Constant.ViewName_NhaCungCap:
                    view = new rNhaCungCapView();
                    break;
                case Constant.ViewName_Nuoc:
                    view = new rNuocView();
                    break;
                case Constant.ViewName_PhuongTien:
                    view = new rPhuongTienView();
                    break;
                case Constant.ViewName_KhoHang:
                    view = new rKhoHangView();
                    break;
                //1Level
                case Constant.ViewName_Chanh:
                    view = new rChanhView();
                    break;
                case Constant.ViewName_DiaDiem:
                    view = new rDiaDiemView();
                    break;
                case Constant.ViewName_NguyenLieu:
                    view = new rNguyenLieuView();
                    break;
                case Constant.ViewName_NhanVien:
                    view = new rNhanVienView();
                    break;
                case Constant.ViewName_MatHang:
                    view = new tMatHangView();
                    break;
                //2Level
                case Constant.ViewName_CanhBaoTonKho:
                    view = new rCanhBaoTonKhoView();
                    break;
                case Constant.ViewName_KhachHang:
                    view = new rKhachHangView();
                    break;
                case Constant.ViewName_MatHangNguyenLieu:
                    view = new rMatHangNguyenLieuView();
                    break;
                case Constant.ViewName_ChiPhi:
                    view = new tChiPhiView();
                    break;
                case Constant.ViewName_ChuyenHang:
                    view = new tChuyenHangView();
                    break;
                case Constant.ViewName_ChuyenKho:
                    view = new tChuyenKhoView();
                    break;
                case Constant.ViewName_NhapHang:
                    view = new tNhapHangView();
                    break;
                case Constant.ViewName_NhapNguyenLieu:
                    view = new tNhapNguyenLieuView();
                    break;
                case Constant.ViewName_TonKho:
                    view = new tTonKhoView();
                    break;
                //3Level
                case Constant.ViewName_KhachHangChanh:
                    view = new rKhachHangChanhView();
                    break;
                case Constant.ViewName_ChiTietChuyenKho:
                    view = new tChiTietChuyenKhoView();
                    break;
                case Constant.ViewName_ChiTietNhapHang:
                    view = new tChiTietNhapHangView();
                    break;
                case Constant.ViewName_DonHang:
                    view = new tDonHangView();
                    break;
                //4Level
                case Constant.ViewName_ChiTietDonHang:
                    view = new tChiTietDonHangView();
                    break;
                case Constant.ViewName_ChuyenHangDonHang:
                    view = new tChuyenHangDonHangView();
                    break;
                //5Level
                case Constant.ViewName_ChiTietChuyenHangDonHang:
                    view = new tChiTietChuyenHangDonHangView();
                    break;
                //Complex
                case Constant.ViewName_AdminView:
                    view = new AdminView();
                    break;
                case Constant.ViewName_BaiXeView:
                    view = new BaiXeView();
                    break;
                case Constant.ViewName_ChiPhiView:
                    view = new ChiPhiView();
                    break;
                case Constant.ViewName_ChuyenHangView:
                    view = new ChuyenHangView();
                    break;
                case Constant.ViewName_ChuyenKhoView:
                    view = new ChuyenKhoView();
                    break;
                case Constant.ViewName_DonHangView:
                    view = new DonHangView();
                    break;
                case Constant.ViewName_KhachHangChanhView:
                    view = new KhachHangChanhView();
                    break;
                case Constant.ViewName_KhachHangView:
                    view = new KhachHangView();
                    break;
                case Constant.ViewName_MatHangView:
                    view = new MatHangView();
                    break;
                case Constant.ViewName_NhanVienView:
                    view = new NhanVienView();
                    break;
                case Constant.ViewName_NhapHangView:
                    view = new NhapHangView();
                    break;
                //Report
                case Constant.ViewName_ReportByDonHangView:
                    view = new ReportByDonHangView();
                    break;
                case Constant.ViewName_ReportByLoaiHangView:
                    view = new ReportByLoaiHangView();
                    break;
                case Constant.ViewName_ReportByMatHangView:
                    view = new ReportByMatHangView();
                    break;
                case Constant.ViewName_ReportDailyView:
                    view = new ReportDailyView();
                    break;
                case Constant.ViewName_ReportByNhapNguyenLieuView:
                    view = new ReportByNhapNguyenLieuView();
                    break;
                case Constant.ViewName_ReportNhapHangView:
                    view = new ReportNhapHangView();
                    break;
                case Constant.ViewName_ReportByChiPhiView:
                    view = new ReportByChiPhiView();
                    break;
            }

            return view;
        }
    }
}
