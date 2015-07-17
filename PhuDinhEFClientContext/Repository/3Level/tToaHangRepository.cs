using System.Collections.Generic;
using System.Data.Entity;
using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhEFClientContext.Repository
{
    public static class tToaHangRepository
    {
        public static IQueryable<tToaHang> GetDataQuery(IQueryable<tToaHang> query)
        {
            return query.Include("tChiTietToaHangs.tChiTietDonHang").Include("rKhachHang").OrderByDescending(p => p.Ngay);
        }

        public static IQueryable<tToaHang> GetDataQueryAndRelatedTables(IQueryable<tToaHang> query, ref List<string> relatedTables)
        {
            relatedTables.Clear();
            relatedTables.Add("tToaHang");
            relatedTables.Add("rKhachHang");
            return query.Include("rKhachHang").OrderByDescending(p => p.Ngay);
        }
    }
}
