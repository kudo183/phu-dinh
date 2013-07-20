using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhData.Repository
{
    public static class tChiTietChuyenHangDonHangRepository
    {
        public static List<tChiTietChuyenHangDonHang> GetData(PhuDinhEntities context, Expression<Func<tChiTietChuyenHangDonHang, bool>> filter)
        {
            return Repository<tChiTietChuyenHangDonHang>.GetData(context, filter).
                OrderByDescending(p => p.tChuyenHangDonHang.tChuyenHang.Ngay).
                ThenBy(p => p.tChuyenHangDonHang.tChuyenHang.Gio).ToList();
        }

        public static void Save(PhuDinhEntities context, List<tChiTietChuyenHangDonHang> data, Expression<Func<tChiTietChuyenHangDonHang, bool>> filter)
        {
            var changed = Repository<tChiTietChuyenHangDonHang>.Save(context, data, filter, (p => p.Ma == 0), ((p1, p2) => p1.Ma == p2.Ma));

            if (changed.Count == 0)
            {
                return;
            }

            var maChuyenHangDonHangs = changed.Select(p => p.Entity.MaChuyenHangDonHang).ToList();

            var maDonHangs = maChuyenHangDonHangs.Select(
                ma => Repository<tChuyenHangDonHang>.GetData(context, (p => p.Ma == ma)).First())
                .Select(chuyenHangDonHang => chuyenHangDonHang.MaDonHang).ToList();
            
            BusinessLogics.BusinessLogics.UpdateXong(context, maDonHangs);
        }
    }
}
