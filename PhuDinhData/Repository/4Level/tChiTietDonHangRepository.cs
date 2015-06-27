using PhuDinhDataEntity;
using System.Data.Entity;
using System.Linq;

namespace PhuDinhData.Repository
{
    public static class tChiTietDonHangRepository
    {
        public static IQueryable<tChiTietDonHang> GetDataQuery(IQueryable<tChiTietDonHang> query)
        {
            return query
                .Include("tDonHang.rKhachHang")
                .OrderByDescending(p => p.tDonHang.Ngay);
        }
    }
}
