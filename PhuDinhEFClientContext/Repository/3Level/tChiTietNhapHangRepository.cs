using System.Collections.Generic;
using System.Data.Entity;
using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhEFClientContext.Repository
{
    public static class tChiTietNhapHangRepository
    {
        public static IQueryable<tChiTietNhapHang> GetDataQuery(IQueryable<tChiTietNhapHang> query)
        {
            return query.OrderByDescending(p => p.tNhapHang.Ngay);
        }

        public static IQueryable<tChiTietNhapHang> GetDataQueryAndRelatedTables(IQueryable<tChiTietNhapHang> query, ref List<string> relatedTables)
        {
            relatedTables.Clear();
            relatedTables.Add("tChiTietNhapHang");
            relatedTables.Add("tNhapHang");
            return query.Include("tNhapHang").OrderByDescending(p => p.tNhapHang.Ngay);
        }
    }
}
