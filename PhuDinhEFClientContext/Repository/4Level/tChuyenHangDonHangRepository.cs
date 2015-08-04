using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhEFClientContext.Repository
{
    public static class tChuyenHangDonHangRepository
    {
        public static IQueryable<tChuyenHangDonHang> OrderBy(IQueryable<tChuyenHangDonHang> query)
        {
            return query
                .OrderByDescending(p => p.tChuyenHang.Ngay)
                .ThenByDescending(p => p.tChuyenHang.Gio);
        }
    }
}
