using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhData.Repository
{
    public static class rKhachHangRepository
    {
        public static IQueryable<rKhachHang> GetDataQuery(IQueryable<rKhachHang> query)
        {
            return query.OrderBy(p => p.TenKhachHang);
        }
    }
}
