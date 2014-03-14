using System;
using System.Collections.Generic;
using System.Linq;

namespace PhuDinhData.ReportData
{
    public static class ReportByDonHang
    {
        public class ReportDailyByDonHangData
        {
            public string TenKho { get; set; }
            public string TenKhachHang { get; set; }
            public string TenMatHang { get; set; }
            public string SoLuong { get; set; }
        }

        public static List<ReportDailyByDonHangData> FilterByDate(DateTime ngay)
        {
            return Filter(p => p.Ngay == ngay);
        }

        public static List<ReportDailyByDonHangData> FilterByDate(DateTime tuNgay, DateTime denNgay)
        {
            return Filter(p => p.Ngay >= tuNgay && p.Ngay <= denNgay);
        }

        private static List<ReportDailyByDonHangData> Filter(System.Linq.Expressions.Expression<Func<tDonHang, bool>> filter)
        {
            var context = ContextFactory.CreateContext();

            var donHangs = context.tDonHangs
                .Where(filter);

            var khoHangs = new Dictionary<int, string>();
            var khachHangs = new Dictionary<int, string>();
            var matHangs = new Dictionary<int, string>();

            var gKhoHangs = new Dictionary<int, Dictionary<int, Dictionary<int, int>>>();
            foreach (var donHang in donHangs)
            {
                if (khoHangs.ContainsKey(donHang.MaKhoHang) == false)
                {
                    khoHangs.Add(donHang.MaKhoHang, donHang.rKhoHang.TenKho);
                }
                if (khachHangs.ContainsKey(donHang.MaKhachHang) == false)
                {
                    khachHangs.Add(donHang.MaKhachHang, donHang.rKhachHang.TenKhachHang);
                }

                if (gKhoHangs.ContainsKey(donHang.MaKhoHang) == false)
                {
                    gKhoHangs.Add(donHang.MaKhoHang, new Dictionary<int, Dictionary<int, int>>());
                }

                var gKhachHangs = gKhoHangs[donHang.MaKhoHang];
                if (gKhachHangs.ContainsKey(donHang.MaKhachHang) == false)
                {
                    gKhachHangs.Add(donHang.MaKhachHang, new Dictionary<int, int>());
                }
                var gMatHangs = gKhachHangs[donHang.MaKhachHang];
                foreach (var chiTietDonHang in donHang.tChiTietDonHangs)
                {
                    if (matHangs.ContainsKey(chiTietDonHang.MaMatHang) == false)
                    {
                        matHangs.Add(chiTietDonHang.MaMatHang, chiTietDonHang.tMatHang.TenMatHangDayDu);
                    }

                    if (gMatHangs.ContainsKey(chiTietDonHang.MaMatHang) == false)
                    {
                        gMatHangs.Add(chiTietDonHang.MaMatHang, 0);
                    }

                    gMatHangs[chiTietDonHang.MaMatHang] += chiTietDonHang.SoLuong;
                }
            }

            var result = new List<ReportDailyByDonHangData>();

            foreach (var gKhoHang in gKhoHangs)
            {
                result.Add(new ReportDailyByDonHangData()
                {
                    TenKho = khoHangs[gKhoHang.Key]
                });
                foreach (var gKhachHang in gKhoHang.Value)
                {
                    result.Add(new ReportDailyByDonHangData()
                    {
                        TenKhachHang = khachHangs[gKhachHang.Key]
                    });
                    foreach (var gMatHang in gKhachHang.Value)
                    {
                        result.Add(new ReportDailyByDonHangData()
                        {
                            TenMatHang = matHangs[gMatHang.Key],
                            SoLuong = gMatHang.Value.ToString()
                        });
                    }
                }
            }

            return result;
        }
    }
}
