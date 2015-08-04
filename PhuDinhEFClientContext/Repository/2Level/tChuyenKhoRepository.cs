using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhEFClientContext.Repository
{
    public static class tChuyenKhoRepository
    {
        public static IQueryable<tChuyenKho> OrderBy(IQueryable<tChuyenKho> query)
        {
            return query.OrderByDescending(p => p.Ngay);
        }
    }
}
