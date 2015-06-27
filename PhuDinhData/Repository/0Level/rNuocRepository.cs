using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhData.Repository
{
    public static class rNuocRepository
    {
        public static IQueryable<rNuoc> GetDataQuery(IQueryable<rNuoc> query)
        {
            return query.OrderBy(p => p.TenNuoc);
        }
    }
}
