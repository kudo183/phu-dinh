using System.Collections.Generic;
using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhEFClientContext.Repository
{
    public static class rKhachHangChanhRepository
    {
        public static IQueryable<rKhachHangChanh> GetDataQuery(IQueryable<rKhachHangChanh> query)
        {
            return query.OrderBy(p => p.rKhachHang.TenKhachHang);
        }

        public static IQueryable<rKhachHangChanh> GetDataQueryAndRelatedTables(IQueryable<rKhachHangChanh> query, ref List<string> relatedTables)
        {
            relatedTables.Clear();
            relatedTables.Add("rKhachHangChanh");
            return query.OrderBy(p => p.rKhachHang.TenKhachHang);
        }
    }
}
