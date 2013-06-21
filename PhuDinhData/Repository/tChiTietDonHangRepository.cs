using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhData.Repository
{
    public static class tChiTietDonHangRepository
    {
        public static List<tChiTietDonHang> GetData(PhuDinhEntities context, Expression<Func<tChiTietDonHang, bool>> filter)
        {
            return Repository<tChiTietDonHang>.GetData(context, filter).OrderByDescending(p => p.tDonHang.Ngay).ToList();
        }

        public static void Save(PhuDinhEntities context, List<tChiTietDonHang> data, Expression<Func<tChiTietDonHang, bool>> filter)
        {
            Repository<tChiTietDonHang>.Save(context, data, filter, (p => p.Ma == 0), ((p1, p2) => p1.Ma == p2.Ma));
        }
    }
}
