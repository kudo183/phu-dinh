using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhEFClientContext.Repository
{
    public static class tChiTietChuyenHangDonHangRepository
    {
        public static IQueryable<tChiTietChuyenHangDonHang> OrderBy(IQueryable<tChiTietChuyenHangDonHang> query)
        {
            return query
                .OrderByDescending(p => p.tChuyenHangDonHang.tChuyenHang.Ngay)
                .ThenBy(p => p.tChuyenHangDonHang.tChuyenHang.Gio);
        }
    }
}
