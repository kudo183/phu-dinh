using System.Collections.Generic;
using System.Data.Entity;
using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhEFClientContext.Repository
{
    public static class rChanhRepository
    {
        public static IQueryable<rChanh> GetDataQuery(IQueryable<rChanh> query)
        {
            return query.OrderBy(p => p.rBaiXe.DiaDiemBaiXe).ThenBy(p => p.TenChanh);
        }

        public static IQueryable<rChanh> GetDataQueryAndRelatedTables(IQueryable<rChanh> query, ref List<string> relatedTables)
        {
            relatedTables.Clear();
            relatedTables.Add("rChanh");
            relatedTables.Add("rBaiXe");
            return query.Include("rBaiXe").OrderBy(p => p.rBaiXe.DiaDiemBaiXe).ThenBy(p => p.TenChanh);
        }
    }
}
