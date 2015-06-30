using System.Data.Services.Client;
using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhODataClientContext.Repository
{
    public static class tChiTietChuyenKhoRepository
    {
        public static IQueryable<tChiTietChuyenKho> GetDataQuery(IQueryable<tChiTietChuyenKho> query)
        {
            var result = query as DataServiceQuery<tChiTietChuyenKho>;
            result = result.Expand(p => p.tChuyenKho.rKhoHangNhap).Expand(p => p.tChuyenKho.rKhoHangXuat);
            return result.OrderByDescending(p => p.tChuyenKho.Ngay);
        }
    }
}
