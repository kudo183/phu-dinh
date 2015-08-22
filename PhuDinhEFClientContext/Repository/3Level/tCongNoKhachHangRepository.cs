using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhEFClientContext.Repository
{
    public static class tCongNoKhachHangRepository
    {
        public static IQueryable<tCongNoKhachHang> OrderBy(IQueryable<tCongNoKhachHang> query)
        {
            return query.OrderByDescending(p => p.Ngay);
        }
    }
}
