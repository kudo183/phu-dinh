using System.Linq;

namespace PhuDinhData.Repository
{
    public static class tChuyenHangRepository
    {
        public static IQueryable<tChuyenHang> GetDataQuery(IQueryable<tChuyenHang> query)
        {
            return query.OrderByDescending(p => p.Ngay);
        }
    }
}
