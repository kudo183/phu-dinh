using System.Data.Entity;
using System.Linq;

namespace PhuDinhData.Repository
{
    public static class tChiTietToaHangRepository
    {
        public static IQueryable<tChiTietToaHang> GetDataQuery(IQueryable<tChiTietToaHang> query)
        {
            return query
                .Include("tToaHang")
                .OrderByDescending(p => p.tToaHang.Ngay);
        }
    }
}
