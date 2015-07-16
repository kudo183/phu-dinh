using System.Collections.Generic;
using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhEFClientContext.Repository
{
    public static class rNhaCungCapRepository
    {
        public static IQueryable<rNhaCungCap> GetDataQuery(IQueryable<rNhaCungCap> query)
        {
            return query.OrderBy(p => p.TenNhaCungCap);
        }

        public static IQueryable<rNhaCungCap> GetDataQueryAndRelatedTables(IQueryable<rNhaCungCap> query, ref List<string> relatedTables)
        {
            relatedTables.Clear();
            relatedTables.Add("rNhaCungCap");
            return query.OrderBy(p => p.TenNhaCungCap);
        }
    }
}
