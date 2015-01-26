using System.Linq;

namespace PhuDinhData.Repository
{
    public static class rMatHangNguyenLieuRepository
    {
        public static IQueryable<rMatHangNguyenLieu> GetDataQuery(IQueryable<rMatHangNguyenLieu> query)
        {
            return query.OrderByDescending(p => p.MaMatHang);
        }
    }
}
