using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhEFClientContext.Repository
{
    public static class tNhapNguyenLieuRepository
    {
        public static IQueryable<tNhapNguyenLieu> GetDataQuery(IQueryable<tNhapNguyenLieu> query)
        {
            return OrderBy(query);
        }

        public static IQueryable<tNhapNguyenLieu> OrderBy(IQueryable<tNhapNguyenLieu> query)
        {
            return query.OrderByDescending(p => p.Ngay);
        }
    }
}
