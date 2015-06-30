using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhODataClientContext.Repository
{
    public static class rNuocRepository
    {
        public static IQueryable<rNuoc> GetDataQuery(IQueryable<rNuoc> query)
        {
            return query.OrderBy(p => p.TenNuoc);
        }
    }
}
