using System;
using System.Collections.Generic;
using System.Linq;

namespace PhuDinhData.ReportData
{
    public static class ReportByLoaiHang
    {
        public class ReportByLoaiHangData
        {
            public int MaLoaiHang { get; set; }
            public string TenLoaiHang { get; set; }
            public int SoLuong { get; set; }
        }

        public static List<ReportByLoaiHangData> FilterByDate(DateTime ngay)
        {
            return Filter(p => p.tDonHang.Ngay == ngay);
        }

        public static List<ReportByLoaiHangData> FilterByDate(DateTime tuNgay, DateTime denNgay)
        {
            return Filter(p => p.tDonHang.Ngay >= tuNgay && p.tDonHang.Ngay <= denNgay);
        }

        private static List<ReportByLoaiHangData> Filter(System.Linq.Expressions.Expression<Func<tChiTietDonHang, bool>> filter)
        {
            var context = ContextFactory.CreateContext();

            var chiTietDonHangs = context.tChiTietDonHangs
                .Where(filter)
                .GroupBy(p => p.tMatHang.MaLoai);

            var loaiHangs = context.rLoaiHangs.ToDictionary(p => p.Ma);

            var result = (from chiTietDonHang in chiTietDonHangs
                          let soLuong = chiTietDonHang.Sum(p => p.SoLuong)
                          select new ReportByLoaiHangData
                          {
                              MaLoaiHang = chiTietDonHang.Key,
                              SoLuong = soLuong
                          }).ToList();

            foreach (var loaiHangData in result)
            {
                loaiHangData.TenLoaiHang = loaiHangs[loaiHangData.MaLoaiHang].TenLoai;
            }

            return result.OrderBy(p => p.TenLoaiHang).ToList();
        }
    }
}
