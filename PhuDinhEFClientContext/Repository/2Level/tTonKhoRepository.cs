using System.Collections.Generic;
using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhEFClientContext.Repository
{
    public static class tTonKhoRepository
    {
        public static IQueryable<tTonKho> GetDataQuery(IQueryable<tTonKho> query)
        {
            return query.OrderByDescending(p => p.Ngay).OrderBy(p => p.tMatHang.TenMatHang);
        }

        public static IQueryable<tTonKho> GetDataQueryAndRelatedTables(IQueryable<tTonKho> query, ref List<string> relatedTables)
        {
            relatedTables.Clear();
            relatedTables.Add("tTonKho");
            return query.OrderByDescending(p => p.Ngay);
        }
    }
}
