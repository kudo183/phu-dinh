using System.Collections.Generic;
using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhEFClientContext.Repository
{
    public static class rNuocRepository
    {
        public static IQueryable<rNuoc> GetDataQuery(IQueryable<rNuoc> query)
        {
            return query.OrderBy(p => p.TenNuoc);
        }

        public static IQueryable<rNuoc> GetDataQueryAndRelatedTables(IQueryable<rNuoc> query, ref List<string> relatedTables)
        {
            relatedTables.Clear();
            relatedTables.Add("rNuoc");
            return query.OrderBy(p => p.TenNuoc);
        }
    }
}
