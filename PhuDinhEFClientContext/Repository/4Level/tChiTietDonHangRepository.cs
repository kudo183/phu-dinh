using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhEFClientContext.Repository
{
    public static class tChiTietDonHangRepository
    {
        public static IQueryable<tChiTietDonHang> OrderBy(IQueryable<tChiTietDonHang> query)
        {
            return query.OrderByDescending(p => p.tDonHang.Ngay);
        }
    }
}
