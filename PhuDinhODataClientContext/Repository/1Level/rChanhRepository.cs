using System.Data.Services.Client;
using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhODataClientContext.Repository
{
    public static class rChanhRepository
    {
        public static IQueryable<rChanh> GetDataQuery(IQueryable<rChanh> query)
        {
            var result = query as DataServiceQuery<rChanh>;
            result = result.Expand(p => p.rBaiXe);
            return result.OrderBy(p => p.rBaiXe.DiaDiemBaiXe).ThenBy(p => p.TenChanh);
        }
    }
}
