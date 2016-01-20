using PhuDinhDataEntity;
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
            public int SoKy { get; set; }
        }

        public static List<ReportByLoaiHangData> FilterByDate(DateTime ngay)
        {
            return Filter(p => p.tDonHang.Ngay == ngay);
        }

        public static List<ReportByLoaiHangData> FilterByDate(DateTime tuNgay, DateTime denNgay)
        {
            return Filter(p => p.tDonHang.Ngay >= tuNgay && p.tDonHang.Ngay <= denNgay);
        }

        public static List<ReportByLoaiHangData> Filter(System.Linq.Expressions.Expression<Func<tChiTietDonHang, bool>> filter)
        {
            var chiTietDonHangs = ClientContext.Instance
                .GetDataWithRelated(filter, new List<string> {"tMatHang"})
                .ToList();

            var loaiHangs = ClientContext.Instance.GetData<rLoaiHang>(null).ToDictionary(p => p.Ma);

            var dic = new Dictionary<int, ReportByLoaiHangData>();
            foreach (var tChiTietDonHang in chiTietDonHangs)
            {
                var maLoai = tChiTietDonHang.tMatHang.MaLoai;
                if (dic.ContainsKey(maLoai) == false)
                {
                    dic.Add(maLoai, new ReportByLoaiHangData
                    {
                        MaLoaiHang = maLoai,
                        TenLoaiHang = loaiHangs[maLoai].TenLoai
                    }
                    );
                }

                dic[maLoai].SoLuong += tChiTietDonHang.SoLuong;
                dic[maLoai].SoKy += (tChiTietDonHang.SoLuong * tChiTietDonHang.tMatHang.SoKy) / 10;
            }

            var result = dic.Select(reportByMatHangData => reportByMatHangData.Value);

            return result.OrderBy(p => p.TenLoaiHang).ToList();
        }
    }
}
