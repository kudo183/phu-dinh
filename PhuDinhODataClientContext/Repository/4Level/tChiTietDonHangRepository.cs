using System.Data.Services.Client;
using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhODataClientContext.Repository
{
    public static class tChiTietDonHangRepository
    {
        public static IQueryable<tChiTietDonHang> GetDataQuery(IQueryable<tChiTietDonHang> query)
        {
            var result = query as DataServiceQuery<tChiTietDonHang>;
            result = result.Expand(p => p.tDonHang.rKhachHang).Expand(p => p.tMatHang);
            return result.OrderByDescending(p => p.tDonHang.Ngay);
        }
    }
}
