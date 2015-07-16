using System.Collections.Generic;
using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhEFClientContext.Repository
{
    public static class rKhachHangRepository
    {
        public static IQueryable<rKhachHang> GetDataQuery(IQueryable<rKhachHang> query)
        {
            return query.OrderBy(p => p.TenKhachHang);
        }

        public static IQueryable<rKhachHang> GetDataQueryAndRelatedTables(IQueryable<rKhachHang> query, ref List<string> relatedTables)
        {
            relatedTables.Clear();
            relatedTables.Add("rKhachHang");
            return query.OrderBy(p => p.TenKhachHang);
        }
    }
}
