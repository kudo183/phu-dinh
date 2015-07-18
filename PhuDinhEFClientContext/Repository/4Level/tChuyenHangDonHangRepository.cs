using System.Collections.Generic;
using PhuDinhDataEntity;
using System.Data.Entity;
using System.Linq;

namespace PhuDinhEFClientContext.Repository
{
    public static class tChuyenHangDonHangRepository
    {
        public static IQueryable<tChuyenHangDonHang> GetDataQuery(IQueryable<tChuyenHangDonHang> query)
        {
            return query
                .Include("tChuyenHang.rNhanVien")
                .Include("tDonHang.rKhachHang")
                .Include("tDonHang.rKhoHang")
                .OrderByDescending(p => p.tChuyenHang.Ngay)
                .ThenByDescending(p => p.tChuyenHang.Gio);
        }

        public static IQueryable<tChuyenHangDonHang> GetDataQueryAndRelatedTables(IQueryable<tChuyenHangDonHang> query, ref List<string> relatedTables)
        {
            relatedTables.Clear();
            relatedTables.Add("tChuyenHangDonHang");
            relatedTables.Add("tChuyenHang");
            relatedTables.Add("rNhanVien");
            relatedTables.Add("tDonHang");
            relatedTables.Add("rKhachHang");
            return query
                .Include("tChuyenHang.rNhanVien")
                .Include("tDonHang.rKhachHang")
                .OrderByDescending(p => p.tChuyenHang.Ngay)
                .ThenByDescending(p => p.tChuyenHang.Gio);
        }
    }
}
