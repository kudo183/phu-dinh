using PhuDinhDataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using PhuDinhEFClientContext;

namespace PhuDinhData.ReportData
{
    public static class ReportByNhapNguyenLieu
    {
        public class ReportByNhapNguyenLieuData
        {
            public int MaNguyenLieu { get; set; }
            public string TenNguyenLieu { get; set; }
            public int SoLuong { get; set; }
        }

        public static List<ReportByNhapNguyenLieuData> FilterByDate(DateTime ngay)
        {
            return Filter(p => p.Ngay == ngay);
        }

        public static List<ReportByNhapNguyenLieuData> FilterByDate(DateTime tuNgay, DateTime denNgay)
        {
            return Filter(p => p.Ngay >= tuNgay && p.Ngay <= denNgay);
        }

        private static List<ReportByNhapNguyenLieuData> Filter(System.Linq.Expressions.Expression<Func<tNhapNguyenLieu, bool>> filter)
        {
            var context = ContextFactory.CreateContext();

            var nguyenLieu = context.rNguyenLieux.ToDictionary(p => p.Ma, p => p.TenNguyenLieu);

            var nhapNguyenLieus = context.tNhapNguyenLieux
                .Where(filter)
                .GroupBy(p => p.MaNguyenLieu);

            var result = (from nhapNguyenLieu in nhapNguyenLieus
                          let soLuong = nhapNguyenLieu.Sum(p => p.SoLuong)
                          select new ReportByNhapNguyenLieuData
                          {
                              MaNguyenLieu = nhapNguyenLieu.Key,
                              SoLuong = soLuong
                          }).ToList();

            foreach (var nhapNguyenLieuData in result)
            {
                nhapNguyenLieuData.TenNguyenLieu = nguyenLieu[nhapNguyenLieuData.MaNguyenLieu];
            }

            return result.OrderBy(p => p.MaNguyenLieu).ToList();
        }
    }
}
