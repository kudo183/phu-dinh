using PhuDinhDataEntity;
using System.Linq;
using System.Data.Entity;

namespace PhuDinhEFClientContext.Repository
{
    public static class tNhapHangRepository
    {
        public static IQueryable<tNhapHang> GetDataQuery(IQueryable<tNhapHang> query)
        {
            return OrderBy(query.Include(p => p.tChiTietNhapHangs));
        }

        public static IQueryable<tNhapHang> OrderBy(IQueryable<tNhapHang> query)
        {
            return query.OrderByDescending(p => p.Ngay);
        }
    }
}
