using System.Collections.Generic;
using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhEFClientContext.Repository
{
    public static class tNhapNguyenLieuRepository
    {
        public static IQueryable<tNhapNguyenLieu> GetDataQuery(IQueryable<tNhapNguyenLieu> query)
        {
            return query.OrderByDescending(p => p.Ngay);
        }

        public static IQueryable<tNhapNguyenLieu> GetDataQueryAndRelatedTables(IQueryable<tNhapNguyenLieu> query, ref List<string> relatedTables)
        {
            relatedTables.Clear();
            relatedTables.Add("tNhapNguyenLieu");
            return query.OrderByDescending(p => p.Ngay);
        }
    }
}
