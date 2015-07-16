using System.Collections.Generic;
using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhEFClientContext.Repository
{
    public static class rBaiXeRepository
    {
        public static IQueryable<rBaiXe> GetDataQuery(IQueryable<rBaiXe> query)
        {
            return query.OrderBy(p => p.DiaDiemBaiXe);
        }

        public static IQueryable<rBaiXe> GetDataQueryAndRelatedTables(IQueryable<rBaiXe> query, ref List<string> relatedTables)
        {
            relatedTables.Clear();
            relatedTables.Add("rBaiXe");
            return query.OrderBy(p => p.DiaDiemBaiXe);
        }
    }
}
