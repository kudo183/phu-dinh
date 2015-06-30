using System.Data.Services.Client;
using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhODataClientContext.Repository
{
    public static class tChuyenHangDonHangRepository
    {
        public static IQueryable<tChuyenHangDonHang> GetDataQuery(IQueryable<tChuyenHangDonHang> query)
        {
            var result = query as DataServiceQuery<tChuyenHangDonHang>;
            result = result.Expand(p => p.tDonHang.rKhachHang).Expand(p => p.tChuyenHang.rNhanVien);

            return result
                .OrderByDescending(p => p.tChuyenHang.Ngay)
                .ThenByDescending(p => p.tChuyenHang.Gio);
        }
    }
}
