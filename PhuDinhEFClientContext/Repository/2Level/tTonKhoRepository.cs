using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhEFClientContext.Repository
{
    public static class tTonKhoRepository
    {
        public static IQueryable<tTonKho> OrderBy(IQueryable<tTonKho> query)
        {
            return query.OrderByDescending(p => p.Ngay).ThenBy(p => p.tMatHang.TenMatHang);
        }
    }
}
