using System.Collections.Generic;
using System.Linq;

namespace PhuDinhData.BusinessLogics
{
    public static class TinhTrongLuongHang
    {
        public static double TinhTrongLuongTheoChiTietDonHang(
            IEnumerable<PhuDinhDataEntity.tChiTietDonHang> chiTietDonHangs, out string msg)
        {
            msg = string.Empty;

            if (chiTietDonHangs == null)
                return 0;

            double trongLuong = 0;
            foreach (var tChiTietDonHang in chiTietDonHangs)
            {
                string temp;

                trongLuong += TinhTrongLuong(tChiTietDonHang.tMatHang,
                    tChiTietDonHang.MaMatHang,
                    tChiTietDonHang.SoLuong,
                    out temp);

                msg += temp;
            }

            return trongLuong / 10;
        }

        public static double TinhTrongLuongTheoChiTietChuyenKho(
            IEnumerable<PhuDinhDataEntity.tChiTietChuyenKho> chiTietChuyenKhos, out string msg)
        {
            msg = string.Empty;

            if (chiTietChuyenKhos == null)
                return 0;

            double trongLuong = 0;
            foreach (var tChiTietChuyenKho in chiTietChuyenKhos)
            {
                string temp;

                trongLuong += TinhTrongLuong(tChiTietChuyenKho.tMatHang,
                    tChiTietChuyenKho.MaMatHang,
                    tChiTietChuyenKho.SoLuong,
                    out temp);

                msg += temp;
            }

            return trongLuong / 10;
        }

        public static double TinhTrongLuongTheoChiTietNhapHang(
            IEnumerable<PhuDinhDataEntity.tChiTietNhapHang> chiTietNhapHangs, out string msg)
        {
            msg = string.Empty;

            if (chiTietNhapHangs == null)
                return 0;

            double trongLuong = 0;
            foreach (var tChiTietNhapHang in chiTietNhapHangs)
            {
                string temp;

                trongLuong += TinhTrongLuong(tChiTietNhapHang.tMatHang,
                    tChiTietNhapHang.MaMatHang,
                    tChiTietNhapHang.SoLuong,
                    out temp);

                msg += temp;
            }

            return trongLuong / 10;
        }

        private static int TinhTrongLuong(PhuDinhDataEntity.tMatHang matHang, int maMatHang, int soLuong, out string msg)
        {
            int trongLuong = 0;
            msg = string.Empty;

            if (matHang == null)
            {
                matHang = ClientContext.Instance.GetData<PhuDinhDataEntity.tMatHang>(p => p.Ma == maMatHang).First();
            }

            if (matHang.SoKy != 0)
            {
                trongLuong = trongLuong + (matHang.SoKy * soLuong);
            }
            else
            {
                msg += string.Format(", {0}", matHang.TenMatHang);
            }

            return trongLuong;
        }
    }
}
