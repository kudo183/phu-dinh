using System.Data.Services.Client;
using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhODataClientContext.Repository
{
    public static class tChiTietToaHangRepository
    {
        public static IQueryable<tChiTietToaHang> GetDataQuery(IQueryable<tChiTietToaHang> query)
        {
            var result = query as DataServiceQuery<tChiTietToaHang>;
            result = result.Expand(p => p.tToaHang.rKhachHang)
                .Expand(p => p.tChiTietDonHang.tDonHang.rKhachHang)
                .Expand(p => p.tChiTietDonHang.tMatHang);
            return result.OrderByDescending(p => p.tToaHang.Ngay);
        }
    }
}
