using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PhuDinhData.Repository
{
    public static class tChuyenHangRepository
    {
        public static List<tChuyenHang> GetData(PhuDinhEntities context, Expression<Func<tChuyenHang, bool>> filter)
        {
            return Repository<tChuyenHang>.GetData(context, filter);
        }

        public static void Save(PhuDinhEntities context, List<tChuyenHang> data, Expression<Func<tChuyenHang, bool>> filter)
        {
            Repository<tChuyenHang>.Save(context, data, filter, (p => p.Ma == 0), ((p1, p2) => p1.Ma == p2.Ma));
        }
    }
}
