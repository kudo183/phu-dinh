using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhData.Repository
{
    public static class rBaiXeRepository
    {
        public static List<rBaiXe> GetData(PhuDinhEntities context, Expression<Func<rBaiXe, bool>> filter)
        {
            return Repository<rBaiXe>.GetData(context, filter);
        }

        public static void Save(PhuDinhEntities context, List<rBaiXe> data, Expression<Func<rBaiXe, bool>> filter)
        {
            Repository<rBaiXe>.Save(context, data, filter, (p => p.Ma == 0), ((p1, p2) => p1.Ma == p2.Ma));
        }
    }
}
