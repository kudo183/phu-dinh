using PhuDinhDataEntity;
using System.Data.Entity;
using System.Linq;

namespace PhuDinhEFClientContext.Repository
{
    public static class tChiTietDonHangRepository
    {
        public static IQueryable<tChiTietDonHang> GetDataQuery(IQueryable<tChiTietDonHang> query)
        {
            return OrderBy(query
                .Include("tDonHang.rKhoHang")
                .Include("tDonHang.rKhachHang"));
        }

        public static IQueryable<tChiTietDonHang> OrderBy(IQueryable<tChiTietDonHang> query)
        {
            return query.OrderByDescending(p => p.tDonHang.Ngay);
        }
    }
}
