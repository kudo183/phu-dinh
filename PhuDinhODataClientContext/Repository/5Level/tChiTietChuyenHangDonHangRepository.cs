using System.Data.Services.Client;
using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhODataClientContext.Repository
{
    public static class tChiTietChuyenHangDonHangRepository
    {
        public static IQueryable<tChiTietChuyenHangDonHang> GetDataQuery(IQueryable<tChiTietChuyenHangDonHang> query)
        {
            var result = query as DataServiceQuery<tChiTietChuyenHangDonHang>;
            result = result.Expand(p => p.tChuyenHangDonHang.tChuyenHang.rNhanVien)
                .Expand(p => p.tChuyenHangDonHang.tDonHang.rKhachHang)
                .Expand(p => p.tChiTietDonHang.tDonHang.rKhachHang)
                .Expand(p => p.tChiTietDonHang.tMatHang);

            return result
                .OrderByDescending(p => p.tChuyenHangDonHang.tChuyenHang.Ngay)
                .ThenBy(p => p.tChuyenHangDonHang.tChuyenHang.Gio);
        }
    }
}
