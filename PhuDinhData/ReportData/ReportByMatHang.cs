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
            public int MaMatHang { get; set; }
            public string TenMatHang { get; set; }
            public int SoLuong { get; set; }
            public int SoKy { get; set; }
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
                .ToList();

            var dic = new Dictionary<int, ReportByMatHangData>();
            foreach (var tChiTietDonHang in chiTietDonHangs)
            {
                var maMatHang = tChiTietDonHang.MaMatHang;
                if (dic.ContainsKey(maMatHang) == false)
                {
                    dic.Add(maMatHang, new ReportByMatHangData
                        {
                            MaMatHang = maMatHang,
                            TenMatHang = tChiTietDonHang.tMatHang.TenMatHangDayDu
                        }
                    );
                }

                dic[maMatHang].SoLuong += tChiTietDonHang.SoLuong;
                dic[maMatHang].SoKy += (tChiTietDonHang.SoLuong * tChiTietDonHang.tMatHang.SoKy) / 10;
            }

            var result = dic.Select(reportByMatHangData => reportByMatHangData.Value);

            return result.OrderBy(p => p.TenMatHang).ToList();
        }
    }
}
