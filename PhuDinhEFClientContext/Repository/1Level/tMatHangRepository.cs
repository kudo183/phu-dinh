using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhEFClientContext.Repository
{
    public static class tMatHangRepository
    {
        public static IQueryable<tMatHang> GetDataQuery(IQueryable<tMatHang> query)
        {
            return OrderBy(query);
        }

        public static IQueryable<tMatHang> OrderBy(IQueryable<tMatHang> query)
        {
            return query.OrderBy(p => p.TenMatHang);
        }
    }
}
