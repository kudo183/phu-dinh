using System.Collections.Generic;
using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhEFClientContext.Repository
{
    public static class tToaHangRepository
    {
        public static IQueryable<tToaHang> GetDataQuery(IQueryable<tToaHang> query)
        {
            return query.OrderByDescending(p => p.Ngay);
        }

        public static IQueryable<tToaHang> GetDataQueryAndRelatedTables(IQueryable<tToaHang> query, ref List<string> relatedTables)
        {
            relatedTables.Clear();
            relatedTables.Add("tToaHang");
            return query.OrderByDescending(p => p.Ngay);
        }
    }
}
