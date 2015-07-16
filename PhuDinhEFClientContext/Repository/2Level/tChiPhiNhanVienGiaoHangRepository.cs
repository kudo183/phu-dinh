using System.Collections.Generic;
using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhEFClientContext.Repository
{
    public static class tChiPhiRepository
    {
        public static IQueryable<tChiPhi> GetDataQuery(IQueryable<tChiPhi> query)
        {
            return query.OrderByDescending(p => p.Ngay);
        }

        public static IQueryable<tChiPhi> GetDataQueryAndRelatedTables(IQueryable<tChiPhi> query, ref List<string> relatedTables)
        {
            relatedTables.Clear();
            relatedTables.Add("tChiPhi");
            return query.OrderByDescending(p => p.Ngay);
        }
    }
}
