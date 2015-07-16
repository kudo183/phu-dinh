using System.Collections.Generic;
using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhEFClientContext.Repository
{
    public static class tChuyenHangRepository
    {
        public static IQueryable<tChuyenHang> GetDataQuery(IQueryable<tChuyenHang> query)
        {
            return query.OrderByDescending(p => p.Ngay);
        }

        public static IQueryable<tChuyenHang> GetDataQueryAndRelatedTables(IQueryable<tChuyenHang> query, ref List<string> relatedTables)
        {
            relatedTables.Clear();
            relatedTables.Add("tChuyenHang");
            return query.OrderByDescending(p => p.Ngay);
        }
    }
}
