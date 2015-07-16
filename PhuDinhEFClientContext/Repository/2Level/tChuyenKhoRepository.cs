using System.Collections.Generic;
using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhEFClientContext.Repository
{
    public static class tChuyenKhoRepository
    {
        public static IQueryable<tChuyenKho> GetDataQuery(IQueryable<tChuyenKho> query)
        {
            return query.OrderByDescending(p => p.Ngay);
        }

        public static IQueryable<tChuyenKho> GetDataQueryAndRelatedTables(IQueryable<tChuyenKho> query, ref List<string> relatedTables)
        {
            relatedTables.Clear();
            relatedTables.Add("tChuyenKho");
            return query.OrderByDescending(p => p.Ngay);
        }
    }
}
