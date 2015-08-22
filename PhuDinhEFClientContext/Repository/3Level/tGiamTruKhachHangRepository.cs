using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhEFClientContext.Repository
{
    public static class tGiamTruKhachHangRepository
    {
        public static IQueryable<tGiamTruKhachHang> OrderBy(IQueryable<tGiamTruKhachHang> query)
        {
            return query.OrderByDescending(p => p.Ngay);
        }
    }
}
