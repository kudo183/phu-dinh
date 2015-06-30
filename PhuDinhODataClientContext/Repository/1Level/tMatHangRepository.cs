using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhODataClientContext.Repository
{
    public static class tMatHangRepository
    {
        public static IQueryable<tMatHang> GetDataQuery(IQueryable<tMatHang> query)
        {
            return query.OrderBy(p => p.TenMatHang);
        }
    }
}
