using System.Data.Entity;
using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhEFClientContext.Repository
{
    public static class tChiTietNhapHangRepository
    {
        public static IQueryable<tChiTietNhapHang> GetDataQuery(IQueryable<tChiTietNhapHang> query)
        {
            return OrderBy(query.Include("tNhapHang.rNhaCungCap"));
        }

        public static IQueryable<tChiTietNhapHang> OrderBy(IQueryable<tChiTietNhapHang> query)
        {
            return query.OrderByDescending(p => p.tNhapHang.Ngay);
        }
    }
}
