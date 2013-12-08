using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhData.Repository
{
    public static class tChuyenHangDonHangRepository
    {
        public static List<tChuyenHangDonHang> GetData(PhuDinhEntities context, Expression<Func<tChuyenHangDonHang, bool>> filter)
        {
            return Repository<tChuyenHangDonHang>.GetData(context, filter).
                OrderByDescending(p => p.tChuyenHang.Ngay).
                ThenByDescending(p => p.tChuyenHang.Gio).ToList();
        }

        public static List<Repository<tChuyenHangDonHang>.ChangedItemData> Save(PhuDinhEntities context, List<tChuyenHangDonHang> data, List<tChuyenHangDonHang> origData)
        {
            return Repository<tChuyenHangDonHang>.Save(context, data, origData, (p => p.Ma == 0), ((p1, p2) => p1.Ma == p2.Ma));
        }
    }
}
