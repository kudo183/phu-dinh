using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhData.Repository
{
    public static class tChuyenKhoRepository
    {
        public static IQueryable<tChuyenKho> GetDataQuery(IQueryable<tChuyenKho> query)
        {
            return query.OrderByDescending(p => p.Ngay);
        }
    }
}
