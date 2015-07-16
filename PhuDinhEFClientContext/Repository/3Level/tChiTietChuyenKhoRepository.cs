using System.Collections.Generic;
using System.Data.Entity;
using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhEFClientContext.Repository
{
    public static class tChiTietChuyenKhoRepository
    {
        public static IQueryable<tChiTietChuyenKho> GetDataQuery(IQueryable<tChiTietChuyenKho> query)
        {
            return query.OrderByDescending(p => p.tChuyenKho.Ngay);
        }

        public static IQueryable<tChiTietChuyenKho> GetDataQueryAndRelatedTables(IQueryable<tChiTietChuyenKho> query, ref List<string> relatedTables)
        {
            relatedTables.Clear();
            relatedTables.Add("tChiTietChuyenKho");
            relatedTables.Add("tChuyenKho");
            return query.Include("tChuyenKho").OrderByDescending(p => p.tChuyenKho.Ngay);
        }
    }
}
