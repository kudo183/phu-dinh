using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PhuDinhData.Repository
{
    public static class tChuyenHangDonHangRepository
    {
        public static List<tChuyenHangDonHang> GetData(PhuDinhEntities context, Expression<Func<tChuyenHangDonHang, bool>> filter)
        {
            return Repository<tChuyenHangDonHang>.GetData(context, filter);
        }

        public static void Save(PhuDinhEntities context, List<tChuyenHangDonHang> data, Expression<Func<tChuyenHangDonHang, bool>> filter)
        {
            Repository<tChuyenHangDonHang>.Save(context, data, filter, (p => p.Ma == 0), ((p1, p2) => p1.Ma == p2.Ma));
        }
    }
}
