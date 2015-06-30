using System.Data.Services.Client;
using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhODataClientContext.Repository
{
    public static class rKhachHangRepository
    {
        public static IQueryable<rKhachHang> GetDataQuery(IQueryable<rKhachHang> query)
        {
            var result = query as DataServiceQuery<rKhachHang>;
            result = result.Expand(p => p.rDiaDiem.rNuoc);
            return result.OrderBy(p => p.TenKhachHang);
        }
    }
}
