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
            return query
                .Include("tChuyenKho.rKhoHangNhap")
                .Include("tChuyenKho.rKhoHangXuat")
                .OrderByDescending(p => p.tChuyenKho.Ngay);
        }

        public static IQueryable<tChiTietChuyenKho> GetDataQueryAndRelatedTables(IQueryable<tChiTietChuyenKho> query, ref List<string> relatedTables)
        {
            relatedTables.Clear();
            relatedTables.Add("tChiTietChuyenKho");
            relatedTables.Add("tChuyenKho");
            relatedTables.Add("rKhoHangNhap");
            relatedTables.Add("rKhoHangXuat");
            return query
                .Include("tChuyenKho.rKhoHangNhap")
                .Include("tChuyenKho.rKhoHangXuat")
                .OrderByDescending(p => p.tChuyenKho.Ngay);
        }
    }
}
