using System.Collections.Generic;
using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhEFClientContext.Repository
{
    public static class tMatHangRepository
    {
        public static IQueryable<tMatHang> GetDataQuery(IQueryable<tMatHang> query)
        {
            return query.OrderBy(p => p.TenMatHang);
        }

        public static IQueryable<tMatHang> GetDataQueryAndRelatedTables(IQueryable<tMatHang> query, ref List<string> relatedTables)
        {
            relatedTables.Clear();
            relatedTables.Add("tMatHang");
            return query.OrderBy(p => p.TenMatHang);
        }
    }
}
