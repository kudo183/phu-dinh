using System.Collections.Generic;
using System.Data.Entity;
using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhEFClientContext.Repository
{
    public static class tChiTietNhapHangRepository
    {
        public static IQueryable<tChiTietNhapHang> GetDataQuery(IQueryable<tChiTietNhapHang> query)
        {
            return query.Include("tNhapHang.rNhaCungCap").OrderByDescending(p => p.tNhapHang.Ngay);
        }

        public static IQueryable<tChiTietNhapHang> GetDataQueryAndRelatedTables(IQueryable<tChiTietNhapHang> query, ref List<string> relatedTables)
        {
            relatedTables.Clear();
            relatedTables.Add("tChiTietNhapHang");
            relatedTables.Add("tNhapHang");
            relatedTables.Add("rNhaCungCap");
            return query.Include("tNhapHang.rNhaCungCap").OrderByDescending(p => p.tNhapHang.Ngay);
        }
    }
}
