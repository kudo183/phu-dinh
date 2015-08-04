using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhEFClientContext.Repository
{
    public static class rKhachHangRepository
    {
        public static IQueryable<rKhachHang> OrderBy(IQueryable<rKhachHang> query)
        {
            return query.OrderBy(p => p.TenKhachHang);
        }
    }
}
