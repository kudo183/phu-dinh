using System.Collections.Generic;
using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhEFClientContext.Repository
{
    public static class rNguyenLieuRepository
    {
        public static IQueryable<rNguyenLieu> GetDataQuery(IQueryable<rNguyenLieu> query)
        {
            return query.OrderBy(p => p.rLoaiNguyenLieu.TenLoai).ThenBy(p => p.DuongKinh);
        }

        public static IQueryable<rNguyenLieu> GetDataQueryAndRelatedTables(IQueryable<rNguyenLieu> query, ref List<string> relatedTables)
        {
            relatedTables.Clear();
            relatedTables.Add("rNguyenLieu");
            return query.OrderBy(p => p.rLoaiNguyenLieu.TenLoai).ThenBy(p => p.DuongKinh);
        }
    }
}
