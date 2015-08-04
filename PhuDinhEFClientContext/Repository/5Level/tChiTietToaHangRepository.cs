using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhEFClientContext.Repository
{
    public static class tChiTietToaHangRepository
    {
        public static IQueryable<tChiTietToaHang> OrderBy(IQueryable<tChiTietToaHang> query)
        {
            return query.OrderByDescending(p => p.tToaHang.Ngay);
        }
    }
}
