using PhuDinhDataEntity;
using System.Data.Entity;
using System.Linq;

namespace PhuDinhEFClientContext.Repository
{
    public static class tChiTietToaHangRepository
    {
        public static IQueryable<tChiTietToaHang> GetDataQuery(IQueryable<tChiTietToaHang> query)
        {
            return OrderBy(query
                .Include("tToaHang.rKhachHang")
                .Include("tChiTietDonHang.tMatHang")
                .Include("tChiTietDonHang.tDonHang.rKhachHang")
                .Include("tChiTietDonHang.tDonHang.rKhoHang"));
        }

        public static IQueryable<tChiTietToaHang> OrderBy(IQueryable<tChiTietToaHang> query)
        {
            return query.OrderByDescending(p => p.tToaHang.Ngay);
        }
    }
}
