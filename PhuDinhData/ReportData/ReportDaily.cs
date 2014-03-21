using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
                    var ten = tDonHang.rKhachHang.TenKhachHang;
                    if (ten == "Chợ")
                    {
                        var sb = new StringBuilder();

                        if (tDonHang.tChuyenHangDonHangs.Count > 0)
                        {
                            foreach (var tChuyenHangDonHang in tDonHang.tChuyenHangDonHangs)
                            {
                                sb.AppendFormat("{0}, ", tChuyenHangDonHang.tChuyenHang.rNhanVien.TenNhanVien);
                            }

                            ten = string.Format("{0} ({1})", ten, sb.ToString(0, sb.Length - 2));
                        }
                    }

                    result.Add(new ReportDailyData()
                    {
                        TenKhachHang = ten
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
