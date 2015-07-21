using System.Collections.Generic;
using PhuDinhDataEntity;
using System.Linq;
using System.Data.Entity;

namespace PhuDinhEFClientContext.Repository
{
    public static class tChuyenKhoRepository
    {
        public static IQueryable<tChuyenKho> GetDataQuery(IQueryable<tChuyenKho> query)
        {
            return query.Include(p => p.tChiTietChuyenKhoes).OrderByDescending(p => p.Ngay);
        }

        public static IQueryable<tChuyenKho> GetDataQueryAndRelatedTables(IQueryable<tChuyenKho> query, ref List<string> relatedTables)
        {
            relatedTables.Clear();
            relatedTables.Add("tChuyenKho");
            return query.OrderByDescending(p => p.Ngay);
        }
    }
}
