using System.Collections.Generic;

namespace PhuDinhDataEntity
{
    public static class EntityHelper
    {
        public static List<string> GetReferenceDataRelatedTables(string entityName)
        {
            var result = new List<string>();
            switch (entityName)
            {
                //1Level
                case "rDiaDiem":
                    result.Add("rNuoc");
                    break;
                case "rChanh":
                    result.Add("rBaiXe");
                    break;
                case "rNguyenLieu":
                    result.Add("rLoaiNguyenLieu");
                    break;
                case "rNhanVien":
                    break;
                case "rMatHang":
                    break;
                //2Level
                case "rCanhBaoTonKho":
                    break;
                case "rKhachHang":
                    result.Add("rDiaDiem.rNuoc");
                    break;
                case "rMatHangNguyenLieu":
                    break;
                case "tChiPhi":
                    break;
                case "tChuyenHang":
                    break;
                case "tChuyenKho":
                    break;
                case "tNhapHang":
                    break;
                case "tNhapNguyenLieu":
                    break;
                case "tTonKho":
                    break;
                //3Level
                case "rKhachHangChanh":
                    result.Add("rChanh.rBaiXe");
                    break;
                case "tChiTietChuyenKho":
                    result.Add("tChuyenKho.rKhoHangNhap");
                    result.Add("tChuyenKho.rKhoHangXuat");
                    break;
                case "tChiTietNhapHang":
                    result.Add("tNhapHang.rNhaCungCap");
                    break;
                case "tDonHang":
                    result.Add("rKhachHang");
                    result.Add("rKhoHang");
                    break;
                case "tToaHang":
                    result.Add("rKhachHang");
                    break;
                //4Level
                case "tChiTietDonHang":
                    result.Add("tDonHang.rKhoHang");
                    result.Add("tDonHang.rKhachHang");
                    result.Add("tMatHang");
                    break;
                case "tChuyenHangDonHang":
                    result.Add("tChuyenHang.rNhanVien");
                    result.Add("tDonHang.rKhachHang");
                    break;
                //5Level
                case "tChiTietChuyenHangDonHang":
                    break;
                case "tChiTietToaHang":
                    break;
            }
            return result;
        }
    }
}
