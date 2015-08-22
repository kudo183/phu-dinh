using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhEFClientContext.Repository
{
    public static class tPhuThuKhachHangRepository
    {
        public static IQueryable<tPhuThuKhachHang> OrderBy(IQueryable<tPhuThuKhachHang> query)
        {
            return query.OrderByDescending(p => p.Ngay);
        }
    }
}
