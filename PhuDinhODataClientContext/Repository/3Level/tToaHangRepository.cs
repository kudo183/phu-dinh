using System.Data.Services.Client;
using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhODataClientContext.Repository
{
    public static class tToaHangRepository
    {
        public static IQueryable<tToaHang> GetDataQuery(IQueryable<tToaHang> query)
        {
            var result = query as DataServiceQuery<tToaHang>;
            result = result.Expand(p => p.rKhachHang);
            return result.OrderByDescending(p => p.Ngay);
        }
    }
}
