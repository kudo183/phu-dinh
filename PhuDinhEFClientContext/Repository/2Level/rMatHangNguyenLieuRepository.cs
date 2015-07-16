using System.Collections.Generic;
using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhEFClientContext.Repository
{
    public static class rMatHangNguyenLieuRepository
    {
        public static IQueryable<rMatHangNguyenLieu> GetDataQuery(IQueryable<rMatHangNguyenLieu> query)
        {
            return query.OrderByDescending(p => p.MaMatHang);
        }

        public static IQueryable<rMatHangNguyenLieu> GetDataQueryAndRelatedTables(IQueryable<rMatHangNguyenLieu> query, ref List<string> relatedTables)
        {
            relatedTables.Clear();
            relatedTables.Add("rMatHangNguyenLieu");
            return query.OrderByDescending(p => p.MaMatHang);
        }
    }
}
