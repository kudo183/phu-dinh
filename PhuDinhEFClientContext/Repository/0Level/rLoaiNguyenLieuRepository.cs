using System.Collections.Generic;
using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhEFClientContext.Repository
{
    public static class rLoaiNguyenLieuRepository
    {
        public static IQueryable<rLoaiNguyenLieu> GetDataQuery(IQueryable<rLoaiNguyenLieu> query)
        {
            return query.OrderBy(p => p.TenLoai);
        }

        public static IQueryable<rLoaiNguyenLieu> GetDataQueryAndRelatedTables(IQueryable<rLoaiNguyenLieu> query, ref List<string> relatedTables)
        {
            relatedTables.Clear();
            relatedTables.Add("rLoaiNguyenLieu");
            return query.OrderBy(p => p.TenLoai);
        }
    }
}
