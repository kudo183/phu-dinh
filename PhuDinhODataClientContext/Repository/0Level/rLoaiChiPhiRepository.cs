using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhODataClientContext.Repository
{
    public static class rLoaiChiPhiRepository
    {
        public static IQueryable<rLoaiChiPhi> GetDataQuery(IQueryable<rLoaiChiPhi> query)
        {
            return query.OrderBy(p => p.TenLoaiChiPhi);
        }
    }
}
