using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhEFClientContext.Repository
{
    public static class tToaHangRepository
    {
        public static IQueryable<tToaHang> OrderBy(IQueryable<tToaHang> query)
        {
            return query.OrderByDescending(p => p.Ngay);
        }
    }
}
