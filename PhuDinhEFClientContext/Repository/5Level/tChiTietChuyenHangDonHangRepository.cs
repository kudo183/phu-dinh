using PhuDinhDataEntity;
using System.Data.Entity;
using System.Linq;

namespace PhuDinhEFClientContext.Repository
{
    public static class tChiTietChuyenHangDonHangRepository
    {
        public static IQueryable<tChiTietChuyenHangDonHang> GetDataQuery(IQueryable<tChiTietChuyenHangDonHang> query)
        {
            return OrderBy(query
                .Include("tChuyenHangDonHang.tChuyenHang.rNhanVien")
                .Include("tChuyenHangDonHang.tDonHang.rKhachHang")
                .Include("tChuyenHangDonHang.tDonHang.rKhoHang")
                .Include("tChiTietDonHang.tMatHang")
                .Include("tChiTietDonHang.tDonHang.rKhachHang")
                .Include("tChiTietDonHang.tDonHang.rKhoHang"));
        }

        public static IQueryable<tChiTietChuyenHangDonHang> OrderBy(IQueryable<tChiTietChuyenHangDonHang> query)
        {
            return query
                .OrderByDescending(p => p.tChuyenHangDonHang.tChuyenHang.Ngay)
                .ThenBy(p => p.tChuyenHangDonHang.tChuyenHang.Gio);
        }
    }
}
