using System.Collections.Generic;
using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhEFClientContext.Repository
{
    public static class tDonHangRepository
    {
        public static IQueryable<tDonHang> GetDataQuery(IQueryable<tDonHang> query)
        {
            return query.OrderByDescending(p => p.Ngay);
        }

        public static IQueryable<tDonHang> GetDataQueryAndRelatedTables(IQueryable<tDonHang> query, ref List<string> relatedTables)
        {
            relatedTables.Clear();
            relatedTables.Add("tDonHang");
            relatedTables.Add("tChiTietDonHang");
            return query.OrderByDescending(p => p.Ngay);
        }
    }
}
