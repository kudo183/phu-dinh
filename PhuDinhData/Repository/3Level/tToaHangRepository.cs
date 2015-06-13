using System.Linq;
using System.Data.Entity;

namespace PhuDinhData.Repository
{
    public static class tToaHangRepository
    {
        public static IQueryable<tToaHang> GetDataQuery(IQueryable<tToaHang> query)
        {
            return query.OrderByDescending(p => p.Ngay);
        }
    }
}
