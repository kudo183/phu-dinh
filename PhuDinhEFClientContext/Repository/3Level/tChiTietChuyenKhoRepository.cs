using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhEFClientContext.Repository
{
    public static class tChiTietChuyenKhoRepository
    {
        public static IQueryable<tChiTietChuyenKho> GetDataQuery(IQueryable<tChiTietChuyenKho> query)
        {
            return query.OrderByDescending(p => p.tChuyenKho.Ngay);
        }
    }
}
