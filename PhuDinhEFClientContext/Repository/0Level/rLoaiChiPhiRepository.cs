using System.Collections.Generic;
using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhEFClientContext.Repository
{
    public static class rLoaiChiPhiRepository
    {
        public static IQueryable<rLoaiChiPhi> GetDataQuery(IQueryable<rLoaiChiPhi> query)
        {
            return query.OrderBy(p => p.TenLoaiChiPhi);
        }

        public static IQueryable<rLoaiChiPhi> GetDataQueryAndRelatedTables(IQueryable<rLoaiChiPhi> query, ref List<string> relatedTables)
        {
            relatedTables.Clear();
            relatedTables.Add("rLoaiChiPhi");
            return query.OrderBy(p => p.TenLoaiChiPhi);
        }
    }
}
