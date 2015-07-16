using System.Collections.Generic;
using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhEFClientContext.Repository
{
    public static class rCanhBaoTonKhoRepository
    {
        public static IQueryable<rCanhBaoTonKho> GetDataQuery(IQueryable<rCanhBaoTonKho> query)
        {
            return query.OrderBy(p => p.rKhoHang.TenKho).ThenBy(p => p.tMatHang.TenMatHang);
        }

        public static IQueryable<rCanhBaoTonKho> GetDataQueryAndRelatedTables(IQueryable<rCanhBaoTonKho> query, ref List<string> relatedTables)
        {
            relatedTables.Clear();
            relatedTables.Add("rCanhBaoTonKho");
            return query.OrderBy(p => p.rKhoHang.TenKho).ThenBy(p => p.tMatHang.TenMatHang);
        }
    }
}
