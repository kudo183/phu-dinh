using PhuDinhCommon;
using System.Windows.Controls;

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
                case Constant.ViewName_NhapMatHang:
                    view = new tNhapMatHangView();
                    break;
                case Constant.ViewName_NhapNguyenLieu:
                    view = new tNhapNguyenLieuView();
                    break;
                //3Level
                case Constant.ViewName_KhachHangChanh:
                    view = new rKhachHangChanhView();
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
            }

            return view;
        }
    }
}
