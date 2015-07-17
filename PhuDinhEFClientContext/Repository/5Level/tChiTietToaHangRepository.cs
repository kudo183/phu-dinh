using System.Collections.Generic;
using PhuDinhDataEntity;
using System.Data.Entity;
using System.Linq;

namespace PhuDinhEFClientContext.Repository
{
    public static class tChiTietToaHangRepository
    {
        public static IQueryable<tChiTietToaHang> GetDataQuery(IQueryable<tChiTietToaHang> query)
        {
            return query
                .Include("tToaHang.rKhachHang")
                .Include("tChiTietDonHang.tDonHang")
                .Include("tChiTietDonHang.tMatHang")
                .OrderByDescending(p => p.tToaHang.Ngay);
        }

        public static IQueryable<tChiTietToaHang> GetDataQueryAndRelatedTables(IQueryable<tChiTietToaHang> query, ref List<string> relatedTables)
        {
            relatedTables.Clear();
            relatedTables.Add("tChiTietToaHang");
            relatedTables.Add("tToaHang");
            relatedTables.Add("rKhachHang");
            relatedTables.Add("tChiTietDonHang");
            relatedTables.Add("tDonHang");
            relatedTables.Add("tMatHang");
            return query
                .Include("tToaHang.rKhachHang")
                .Include("tChiTietDonHang.tDonHang")
                .Include("tChiTietDonHang.tMatHang")
                .OrderByDescending(p => p.tToaHang.Ngay);
        }
    }
}
