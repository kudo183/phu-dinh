using System.Collections.Generic;
using PhuDinhDataEntity;
using System.Data.Entity;
using System.Linq;

namespace PhuDinhEFClientContext.Repository
{
    public static class tChiTietDonHangRepository
    {
        public static IQueryable<tChiTietDonHang> GetDataQuery(IQueryable<tChiTietDonHang> query)
        {
            return query
                .Include("tDonHang.rKhoHang")
                .Include("tDonHang.rKhachHang")
                .OrderByDescending(p => p.tDonHang.Ngay);
        }

        public static IQueryable<tChiTietDonHang> GetDataQueryAndRelatedTables(IQueryable<tChiTietDonHang> query, ref List<string> relatedTables)
        {
            relatedTables.Clear();
            relatedTables.Add("tChiTietDonHang");
            relatedTables.Add("tDonHang");
            relatedTables.Add("rKhachHang");
            relatedTables.Add("rKhoHang");
            relatedTables.Add("tMatHang");
            return query
                .Include("tDonHang.rKhoHang")
                .Include("tDonHang.rKhachHang")
                .Include("tMatHang")
                .OrderByDescending(p => p.tDonHang.Ngay);
        }
    }
}
