using System.Collections.Generic;
using PhuDinhDataEntity;
using System.Linq;
using System.Data.Entity;

namespace PhuDinhEFClientContext.Repository
{
    public static class tNhapHangRepository
    {
        public static IQueryable<tNhapHang> GetDataQuery(IQueryable<tNhapHang> query)
        {
            return query.Include(p => p.tChiTietNhapHangs).OrderByDescending(p => p.Ngay);
        }

        public static IQueryable<tNhapHang> GetDataQueryAndRelatedTables(IQueryable<tNhapHang> query, ref List<string> relatedTables)
        {
            relatedTables.Clear();
            relatedTables.Add("tNhapHang");
            return query.OrderByDescending(p => p.Ngay);
        }
    }
}
