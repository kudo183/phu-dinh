using System.Data.Services.Client;
using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhODataClientContext.Repository
{
    public static class rNguyenLieuRepository
    {
        public static IQueryable<rNguyenLieu> GetDataQuery(IQueryable<rNguyenLieu> query)
        {
            var result = query as DataServiceQuery<rNguyenLieu>;
            result = result.Expand(p => p.rLoaiNguyenLieu);
            return result.OrderBy(p => p.rLoaiNguyenLieu.TenLoai).ThenBy(p => p.DuongKinh);
        }
    }
}
