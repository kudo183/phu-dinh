using System.Data.Services.Client;
using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhODataClientContext.Repository
{
    public static class rDiaDiemRepository
    {
        public static IQueryable<rDiaDiem> GetDataQuery(IQueryable<rDiaDiem> query)
        {
            var result = query as DataServiceQuery<rDiaDiem>;
            result = result.Expand(p => p.rNuoc);
            return result.OrderBy(p => p.Tinh);
        }
    }
}
