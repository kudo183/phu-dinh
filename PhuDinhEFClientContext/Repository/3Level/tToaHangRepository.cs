using System.Data.Entity;
using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhEFClientContext.Repository
{
    public static class tToaHangRepository
    {
        public static IQueryable<tToaHang> GetDataQuery(IQueryable<tToaHang> query)
        {
            return OrderBy(query.Include("tChiTietToaHangs.tChiTietDonHang").Include("rKhachHang"));
        }

        public static IQueryable<tToaHang> OrderBy(IQueryable<tToaHang> query)
        {
            return query.OrderByDescending(p => p.Ngay);
        }
    }
}
