using PhuDinhDataEntity;
using System.Data.Entity;
using System.Linq;

namespace PhuDinhEFClientContext.Repository
{
    public static class tChuyenHangDonHangRepository
    {
        public static IQueryable<tChuyenHangDonHang> GetDataQuery(IQueryable<tChuyenHangDonHang> query)
        {
            return OrderBy(query
                .Include("tChuyenHang.rNhanVien")
                .Include("tDonHang.rKhachHang")
                .Include("tDonHang.rKhoHang"));
        }

        public static IQueryable<tChuyenHangDonHang> OrderBy(IQueryable<tChuyenHangDonHang> query)
        {
            return query
                .OrderByDescending(p => p.tChuyenHang.Ngay)
                .ThenByDescending(p => p.tChuyenHang.Gio);
        }
    }
}
