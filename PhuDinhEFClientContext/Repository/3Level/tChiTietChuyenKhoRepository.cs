using System.Data.Entity;
using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhEFClientContext.Repository
{
    public static class tChiTietChuyenKhoRepository
    {
        public static IQueryable<tChiTietChuyenKho> GetDataQuery(IQueryable<tChiTietChuyenKho> query)
        {
            return OrderBy(query
                .Include("tChuyenKho.rKhoHangNhap")
                .Include("tChuyenKho.rKhoHangXuat"));
        }

        public static IQueryable<tChiTietChuyenKho> OrderBy(IQueryable<tChiTietChuyenKho> query)
        {
            return query.OrderByDescending(p => p.tChuyenKho.Ngay);
        }
    }
}
