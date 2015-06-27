using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhData.Repository
{
    public static class tNhapHangRepository
    {
        public static IQueryable<tNhapHang> GetDataQuery(IQueryable<tNhapHang> query)
        {
            return query.OrderByDescending(p => p.Ngay);
        }
    }
}
