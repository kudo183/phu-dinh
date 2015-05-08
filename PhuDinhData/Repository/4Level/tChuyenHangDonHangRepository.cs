using System.Data.Entity;
using System.Linq;

namespace PhuDinhData.Repository
{
    public static class tChuyenHangDonHangRepository
    {
        public static IQueryable<tChuyenHangDonHang> GetDataQuery(IQueryable<tChuyenHangDonHang> query)
        {
            return query
                .Include("tChuyenHang.rNhanVien")
                .Include("tDonHang.rKhachHang")
                .OrderByDescending(p => p.tChuyenHang.Ngay)
                .ThenByDescending(p => p.tChuyenHang.Gio);
        }
    }
}
