using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhEFClientContext.Repository
{
    public static class tNhapHangRepository
    {
        public static IQueryable<tNhapHang> OrderBy(IQueryable<tNhapHang> query)
        {
            return query.OrderByDescending(p => p.Ngay);
        }
    }
}
