using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhEFClientContext.Repository
{
    public static class tChuyenHangRepository
    {
        public static IQueryable<tChuyenHang> OrderBy(IQueryable<tChuyenHang> query)
        {
            return query.OrderByDescending(p => p.Ngay);
        }
    }
}
