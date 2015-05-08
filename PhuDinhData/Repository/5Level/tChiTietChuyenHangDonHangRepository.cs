using System.Data.Entity;
using System.Linq;

namespace PhuDinhData.Repository
{
    public static class tChiTietChuyenHangDonHangRepository
    {
        public static IQueryable<tChiTietChuyenHangDonHang> GetDataQuery(IQueryable<tChiTietChuyenHangDonHang> query)
        {
            return query
                .Include("tChuyenHangDonHang.tChuyenHang")
                .Include("tChuyenHangDonHang.tDonHang.rKhachHang")
                .Include("tChiTietDonHang.tMatHang")
                .OrderByDescending(p => p.tChuyenHangDonHang.tChuyenHang.Ngay)
                .ThenBy(p => p.tChuyenHangDonHang.tChuyenHang.Gio);
        }
    }
}
