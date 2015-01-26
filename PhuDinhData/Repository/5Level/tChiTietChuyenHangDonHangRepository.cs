using System.Linq;

namespace PhuDinhData.Repository
{
    public static class tChiTietChuyenHangDonHangRepository
    {
        public static IQueryable<tChiTietChuyenHangDonHang> GetDataQuery(IQueryable<tChiTietChuyenHangDonHang> query)
        {
            return query.OrderByDescending(p => p.tChuyenHangDonHang.tChuyenHang.Ngay).
                ThenBy(p => p.tChuyenHangDonHang.tChuyenHang.Gio);
        }
    }
}
