using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhEFClientContext.Repository
{
    public static class rNhaCungCapRepository
    {
        public static IQueryable<rNhaCungCap> OrderBy(IQueryable<rNhaCungCap> query)
        {
            return query.OrderBy(p => p.TenNhaCungCap);
        }
    }
}
