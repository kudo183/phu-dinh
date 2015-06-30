using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhODataClientContext.Repository
{
    public static class tNhapNguyenLieuRepository
    {
        public static IQueryable<tNhapNguyenLieu> GetDataQuery(IQueryable<tNhapNguyenLieu> query)
        {
            return query.OrderByDescending(p => p.Ngay);
        }
    }
}
