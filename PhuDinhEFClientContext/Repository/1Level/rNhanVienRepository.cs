using System.Collections.Generic;
using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhEFClientContext.Repository
{
    public static class rNhanVienRepository
    {
        public static IQueryable<rNhanVien> GetDataQuery(IQueryable<rNhanVien> query)
        {
            return query.OrderBy(p => p.TenNhanVien);
        }

        public static IQueryable<rNhanVien> GetDataQueryAndRelatedTables(IQueryable<rNhanVien> query, ref List<string> relatedTables)
        {
            relatedTables.Clear();
            relatedTables.Add("rNhanVien");
            return query.OrderBy(p => p.TenNhanVien);
        }
    }
}
