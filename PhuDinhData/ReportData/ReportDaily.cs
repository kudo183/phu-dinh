using System;
using System.Collections.Generic;
using System.Linq;

namespace PhuDinhData.ReportData
{
    public static class ReportDaily
    {
        public class ReportDailyData
        {
            public string TenKho { get; set; }
            public string TenKhachHang { get; set; }
            public string TenMatHang { get; set; }
            public string SoLuong { get; set; }
        }

        public static List<ReportDailyData> FilterByDate(DateTime ngay)
        {
            return Filter(p => p.Ngay == ngay);
        }

        private static List<ReportDailyData> Filter(System.Linq.Expressions.Expression<Func<tDonHang, bool>> filter)
        {
            var context = ContextFactory.CreateContext();

            var donHangs = context.tDonHangs
                .Where(filter).GroupBy(p => p.MaKhoHang);

            var result = new List<ReportDailyData>();

            foreach (var donHang in donHangs)
            {
                result.Add(new ReportDailyData()
                {
                    TenKho = donHang.First().rKhoHang.TenKho
                });
                foreach (var tDonHang in donHang)
                {
                    result.Add(new ReportDailyData()
                    {
                        TenKhachHang = tDonHang.rKhachHang.TenKhachHang
                    });
                    foreach (var tChiTietDonHang in tDonHang.tChiTietDonHangs)
                    {
                        result.Add(new ReportDailyData()
                        {
                            TenMatHang = tChiTietDonHang.tMatHang.TenMatHangDayDu,
                            SoLuong = tChiTietDonHang.SoLuong.ToString()
                        });
                    }
                }
            }

            return result;
        }
    }
}
