using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhEFClientContext.Repository
{
    public static class rLoaiHangRepository
    {
        public static IQueryable<rLoaiHang> OrderBy(IQueryable<rLoaiHang> query)
        {
            return query.OrderBy(p => p.TenLoai);
        }
    }
}
