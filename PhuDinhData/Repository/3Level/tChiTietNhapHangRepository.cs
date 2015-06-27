using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhData.Repository
{
    public static class tChiTietNhapHangRepository
    {
        public static IQueryable<tChiTietNhapHang> GetDataQuery(IQueryable<tChiTietNhapHang> query)
        {
            return query.OrderByDescending(p => p.tNhapHang.Ngay);
        }
    }
}
