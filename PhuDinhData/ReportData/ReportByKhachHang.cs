using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace PhuDinhData.ReportData
{
    public static class ReportByKhachHang
    {
        public class ReportByKhachHangData
        {
            public string TenKhachHang { get; set; }
            public string Ngay { get; set; }
            public string TenMatHang { get; set; }
            public int SoLuong { get; set; }
            public string SoLuongText { get; set; }
        }

        public static List<ReportByKhachHangData> FilterByDate(DateTime ngay)
        {
            return Filter(p => p.Ngay == ngay);
        }

        public static List<ReportByKhachHangData> FilterByDate(DateTime tuNgay, DateTime denNgay)
        {
            return Filter(p => p.Ngay >= tuNgay && p.Ngay <= denNgay);
        }

        private static List<ReportByKhachHangData> Filter(System.Linq.Expressions.Expression<Func<tDonHang, bool>> filter)
        {
            var context = ContextFactory.CreateContext();

            var gbKhachHang = context.tDonHangs
                .Where(filter)
                .Include(p => p.tChiTietDonHangs.Select(ct => ct.tMatHang))
                .OrderBy(p => p.Ngay)
                .GroupBy(p => p.rKhachHang.TenKhachHang);

            var result = new List<ReportByKhachHangData>();

            foreach (var donHang in gbKhachHang)
            {
                result.Add(new ReportByKhachHangData()
                {
                    TenKhachHang = donHang.Key
                });

                foreach (var gbNgay in donHang.GroupBy(p => p.Ngay))
                {
                    result.Add(new ReportByKhachHangData()
                    {
                        Ngay = gbNgay.Key.ToString("dd/MM/yyyy")
                    });
                    
                    foreach (var matHang in GroupByMatHang(gbNgay))
                    {
                        result.Add(new ReportByKhachHangData()
                        {
                            TenMatHang = matHang.Value.Key,
                            SoLuong = matHang.Value.Value,
                            SoLuongText = matHang.Value.Value.ToString("N0")
                        });
                    }
                }
            }

            return result;
        }

        private static Dictionary<int, KeyValuePair<string, int>> GroupByMatHang(IEnumerable<tDonHang> gbNgay)
        {
            var dMatHangSoLuong = new Dictionary<int, KeyValuePair<string, int>>();

            foreach (var dh in gbNgay)
            {
                foreach (var ct in dh.tChiTietDonHangs)
                {
                    if (dMatHangSoLuong.ContainsKey(ct.MaMatHang) == false)
                    {
                        dMatHangSoLuong.Add(ct.MaMatHang, new KeyValuePair<string, int>(ct.tMatHang.TenMatHang, 0));
                    }

                    var t = new KeyValuePair<string, int>(ct.tMatHang.TenMatHang, dMatHangSoLuong[ct.MaMatHang].Value + ct.SoLuong);

                    dMatHangSoLuong[ct.MaMatHang] = t;
                }
            }

            return dMatHangSoLuong;
        }
    }
}
