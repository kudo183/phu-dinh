using PhuDinhDataEntity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PhuDinhData.ReportData
{
    public static class ReportByChiPhi
    {
        public class ReportByChiPhiData
        {
            public int MaLoaiChiPhi { get; set; }
            public string LoaiChiPhi { get; set; }
            public int SoTien { get; set; }
        }

        public static List<ReportByChiPhiData> FilterByDate(DateTime ngay)
        {
            return Filter(p => p.Ngay == ngay);
        }

        public static List<ReportByChiPhiData> FilterByDate(DateTime tuNgay, DateTime denNgay)
        {
            return Filter(p => p.Ngay >= tuNgay && p.Ngay <= denNgay);
        }

        private static List<ReportByChiPhiData> Filter(System.Linq.Expressions.Expression<Func<tChiPhi, bool>> filter)
        {
            var context = ContextFactory.CreateContext();

            var chiPhis = context.tChiPhis
                .Where(filter)
                .GroupBy(p => p.MaLoaiChiPhi);

            var loaiChiPhis = context.rLoaiChiPhis.ToDictionary(p => p.Ma);

            var result = (from chiPhi in chiPhis
                          let soTien = chiPhi.Sum(p => p.SoTien)
                          select new ReportByChiPhiData()
                          {
                              MaLoaiChiPhi = chiPhi.Key,
                              SoTien = soTien
                          }).ToList();

            foreach (var chiPhiData in result)
            {
                chiPhiData.LoaiChiPhi = loaiChiPhis[chiPhiData.MaLoaiChiPhi].TenLoaiChiPhi;
            }

            return result.OrderBy(p => p.LoaiChiPhi).ToList();
        }
    }
}
