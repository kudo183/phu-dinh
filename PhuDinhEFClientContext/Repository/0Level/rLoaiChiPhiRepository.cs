using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhEFClientContext.Repository
{
    public static class rLoaiChiPhiRepository
    {
        public static IQueryable<rLoaiChiPhi> GetDataQuery(IQueryable<rLoaiChiPhi> query)
        {
            return OrderBy(query);
        }

        public static IQueryable<rLoaiChiPhi> OrderBy(IQueryable<rLoaiChiPhi> query)
        {
            return query.OrderBy(p => p.TenLoaiChiPhi);
        }
    }
}
