using PhuDinhDataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using PhuDinhEFClientContext;

namespace PhuDinhData.ReportData
{
    public static class ReportDaily
    {
        public class ReportDailyData
        {
            public List<ReportDailyRowData> RowData { get; set; }

            public DateTime DateFilter { get; set; }
        }

        public class ReportDailyRowData
        {
            public string TenKho { get; set; }
            public string TenKhachHang { get; set; }
            public string TenMatHang { get; set; }
            public string SoLuong { get; set; }
        }

        public static List<ReportDailyRowData> FilterByDate(DateTime ngay)
        {
            return Filter(p => p.Ngay == ngay);
        }

        private static List<ReportDailyRowData> Filter(System.Linq.Expressions.Expression<Func<tDonHang, bool>> filter)
        {
            var context = ContextFactory.CreateContext();

            var donHangs = context.tDonHangs
                .Where(filter)
                .Include(p => p.rKhoHang)
                .Include(p => p.rKhachHang)
                .ToList()
                .GroupBy(p => p.MaKhoHang);

            var result = new List<ReportDailyRowData>();

            foreach (var donHang in donHangs)
            {
                result.Add(new ReportDailyRowData()
                {
                    TenKho = donHang.First().rKhoHang.TenKho
                });
                foreach (var tDonHang in donHang)
                {
                    var maDonHang = tDonHang.Ma;

                    var ten = tDonHang.rKhachHang.TenKhachHang;
                    if (ten == "Chợ")
                    {
                        var sb = new StringBuilder();

                        var tChuyenHangDonHangs = context.tChuyenHangDonHangs
                            .Where(p => p.MaDonHang == maDonHang)
                            .Include(p => p.tChuyenHang.rNhanVien);

                        if (tChuyenHangDonHangs.Any())
                        {
                            foreach (var tChuyenHangDonHang in tChuyenHangDonHangs)
                            {
                                sb.AppendFormat("{0}, ", tChuyenHangDonHang.tChuyenHang.rNhanVien.TenNhanVien);
                            }

                            ten = string.Format("{0} ({1})", ten, sb.ToString(0, sb.Length - 2));
                        }
                    }

                    result.Add(new ReportDailyRowData()
                    {
                        TenKhachHang = ten
                    });

                    var tChiTietDonHangs = context.tChiTietDonHangs
                        .Where(p => p.MaDonHang == maDonHang)
                        .Include(p => p.tMatHang);

                    foreach (var tChiTietDonHang in tChiTietDonHangs)
                    {
                        result.Add(new ReportDailyRowData()
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
