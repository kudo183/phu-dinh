using PhuDinhDataEntity;
using System.Linq;
using System.Data.Entity;

namespace PhuDinhEFClientContext.Repository
{
    public static class tChuyenKhoRepository
    {
        public static IQueryable<tChuyenKho> GetDataQuery(IQueryable<tChuyenKho> query)
        {
            return OrderBy(query.Include(p => p.tChiTietChuyenKhoes));
        }

        public static IQueryable<tChuyenKho> OrderBy(IQueryable<tChuyenKho> query)
        {
            return query.OrderByDescending(p => p.Ngay);
        }
    }
}
