using System.Linq;

namespace PhuDinhData.Repository
{
    public static class tChiTietDonHangRepository
    {
        public static IQueryable<tChiTietDonHang> GetDataQuery(IQueryable<tChiTietDonHang> query)
        {
            return query.OrderByDescending(p => p.tDonHang.Ngay);
        }
    }
}
