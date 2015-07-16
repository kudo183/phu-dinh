using System.Collections.Generic;
using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhEFClientContext.Repository
{
    public static class rPhuongTienRepository
    {
        public static IQueryable<rPhuongTien> GetDataQuery(IQueryable<rPhuongTien> query)
        {
            return query.OrderBy(p => p.TenPhuongTien);
        }

        public static IQueryable<rPhuongTien> GetDataQueryAndRelatedTables(IQueryable<rPhuongTien> query, ref List<string> relatedTables)
        {
            relatedTables.Clear();
            relatedTables.Add("rPhuongTien");
            return query.OrderBy(p => p.TenPhuongTien);
        }
    }
}
