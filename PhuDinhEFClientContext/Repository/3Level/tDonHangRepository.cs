using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhEFClientContext.Repository
{
    public static class tDonHangRepository
    {
        public static IQueryable<tDonHang> OrderBy(IQueryable<tDonHang> query)
        {
            return query.OrderByDescending(p => p.Ngay);
        }
    }
}
