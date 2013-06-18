using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PhuDinhData.Repository
{
    public static class tDonHangRepository
    {
        public static List<tDonHang> GetData(PhuDinhEntities context, Expression<Func<tDonHang, bool>> filter)
        {
            return Repository<tDonHang>.GetData(context, filter);
        }

        public static void Save(PhuDinhEntities context, List<tDonHang> data, Expression<Func<tDonHang, bool>> filter)
        {
            Repository<tDonHang>.Save(context, data, filter, (p => p.Ma == 0), ((p1, p2) => p1.Ma == p2.Ma));
        }
    }
}
