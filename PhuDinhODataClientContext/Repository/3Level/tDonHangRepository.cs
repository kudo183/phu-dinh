using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhODataClientContext.Repository
{
    public static class tDonHangRepository
    {
        public static IQueryable<tDonHang> GetDataQuery(IQueryable<tDonHang> query)
        {
            return query.OrderByDescending(p => p.Ngay);
        }
    }
}
