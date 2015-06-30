using System.Data.Services.Client;
using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhODataClientContext.Repository
{
    public static class tChiTietNhapHangRepository
    {
        public static IQueryable<tChiTietNhapHang> GetDataQuery(IQueryable<tChiTietNhapHang> query)
        {
            var result = query as DataServiceQuery<tChiTietNhapHang>;
            result = result.Expand(p => p.tNhapHang.rNhaCungCap);
            return result.OrderByDescending(p => p.tNhapHang.Ngay);
        }
    }
}
