using System.Collections.Generic;
using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhEFClientContext.Repository
{
    public static class rKhoHangRepository
    {
        public static IQueryable<rKhoHang> GetDataQuery(IQueryable<rKhoHang> query)
        {
            return query.OrderBy(p => p.TenKho);
        }

        public static IQueryable<rKhoHang> GetDataQueryAndRelatedTables(IQueryable<rKhoHang> query, ref List<string> relatedTables)
        {
            relatedTables.Clear();
            relatedTables.Add("rKhoHang");
            return query.OrderBy(p => p.TenKho);
        }
    }
}
