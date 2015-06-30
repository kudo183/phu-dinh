using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhEFClientContext.Repository
{
    public static class rKhoHangRepository
    {
        public static IQueryable<rKhoHang> GetDataQuery(IQueryable<rKhoHang> query)
        {
            return query.OrderBy(p => p.TenKho);
        }
    }
}
