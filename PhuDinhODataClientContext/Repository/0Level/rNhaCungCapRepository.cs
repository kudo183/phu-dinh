using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhODataClientContext.Repository
{
    public static class rNhaCungCapRepository
    {
        public static IQueryable<rNhaCungCap> GetDataQuery(IQueryable<rNhaCungCap> query)
        {
            return query.OrderBy(p => p.TenNhaCungCap);
        }
    }
}
