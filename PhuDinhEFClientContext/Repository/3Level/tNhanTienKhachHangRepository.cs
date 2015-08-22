using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhEFClientContext.Repository
{
    public static class tNhanTienKhachHangRepository
    {
        public static IQueryable<tNhanTienKhachHang> OrderBy(IQueryable<tNhanTienKhachHang> query)
        {
            return query.OrderByDescending(p => p.Ngay);
        }
    }
}
