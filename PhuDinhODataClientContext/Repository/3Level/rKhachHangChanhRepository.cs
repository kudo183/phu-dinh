using System.Data.Services.Client;
using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhODataClientContext.Repository
{
    public static class rKhachHangChanhRepository
    {
        public static IQueryable<rKhachHangChanh> GetDataQuery(IQueryable<rKhachHangChanh> query)
        {
            var result = query as DataServiceQuery<rKhachHangChanh>;
            result = result.Expand(p => p.rChanh.rBaiXe);
            return result.OrderBy(p => p.rKhachHang.TenKhachHang);
        }
    }
}
