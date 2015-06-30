using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhODataClientContext.Repository
{
    public static class rMatHangNguyenLieuRepository
    {
        public static IQueryable<rMatHangNguyenLieu> GetDataQuery(IQueryable<rMatHangNguyenLieu> query)
        {
            return query.OrderByDescending(p => p.MaMatHang);
        }
    }
}
