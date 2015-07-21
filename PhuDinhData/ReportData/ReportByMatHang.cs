using PhuDinhDataEntity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PhuDinhData.ReportData
{
    public static class ReportByMatHang
    {
        public class ReportByMatHangData
        {
            public string TenMatHang { get; set; }
            public int SoLuong { get; set; }
        }

        public static List<ReportByMatHangData> FilterByDate(DateTime ngay)
        {
            return Filter(p => p.tDonHang.Ngay == ngay);
        }

        public static List<ReportByMatHangData> FilterByDate(DateTime tuNgay, DateTime denNgay)
        {
            return Filter(p => p.tDonHang.Ngay >= tuNgay && p.tDonHang.Ngay <= denNgay);
        }

        public static List<ReportByMatHangData> Filter(System.Linq.Expressions.Expression<Func<tChiTietDonHang, bool>> filter)
        {
            var chiTietDonHangs = ClientContext.Instance
                .GetDataWithRelated(filter, new List<string> { "tMatHang" })
                .ToList()
                .GroupBy(p => p.tMatHang.TenMatHangDayDu);

            var result = (from chiTietDonHang in chiTietDonHangs
                          let soLuong = chiTietDonHang.Sum(p => p.SoLuong)
                          select new ReportByMatHangData
                          {
                              TenMatHang = chiTietDonHang.Key,
                              SoLuong = soLuong
                          }).ToList();

            return result.OrderBy(p => p.TenMatHang).ToList();
        }
    }
}
