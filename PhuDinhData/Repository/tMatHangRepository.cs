using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PhuDinhData.Repository
{
    public static class tMatHangRepository
    {
        public static List<tMatHang> GetData(PhuDinhEntities context, Expression<Func<tMatHang, bool>> filter)
        {
            return Repository<tMatHang>.GetData(context, filter);
        }

        public static void Save(PhuDinhEntities context, List<tMatHang> data, Expression<Func<tMatHang, bool>> filter)
        {
            Repository<tMatHang>.Save(context, data, filter, (p => p.Ma == 0), ((p1, p2) => p1.Ma == p2.Ma));
        }
    }
}
