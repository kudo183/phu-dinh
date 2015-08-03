using System.Data.Entity;
using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhEFClientContext.Repository
{
    public static class tDonHangRepository
    {
        public static IQueryable<tDonHang> GetDataQuery(IQueryable<tDonHang> query)
        {
            return OrderBy(query.Include("rKhachHang").Include("rKhoHang"));
        }

        public static IQueryable<tDonHang> OrderBy(IQueryable<tDonHang> query)
        {
            return query.OrderByDescending(p => p.Ngay);
        }
    }
}
