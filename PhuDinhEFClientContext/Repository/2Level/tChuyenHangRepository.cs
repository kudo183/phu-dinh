using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhEFClientContext.Repository
{
    public static class tChuyenHangRepository
    {
        public static IQueryable<tChuyenHang> GetDataQuery(IQueryable<tChuyenHang> query)
        {
            return OrderBy(query);
        }

        public static IQueryable<tChuyenHang> OrderBy(IQueryable<tChuyenHang> query)
        {
            return query.OrderByDescending(p => p.Ngay);
        }
    }
}
