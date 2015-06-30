using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhEFClientContext.Repository
{
    public static class rKhachHangChanhRepository
    {
        public static IQueryable<rKhachHangChanh> GetDataQuery(IQueryable<rKhachHangChanh> query)
        {
            return query.OrderBy(p => p.rKhachHang.TenKhachHang);
        }
    }
}
