using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhData.Repository
{
    public static class rChanhRepository
    {
        public static List<rChanh> GetData(PhuDinhEntities context, Expression<Func<rChanh, bool>> filter)
        {
            return Repository<rChanh>.GetData(context, filter).ToList().OrderBy(p => p.TenChanh).ToList();
        }

        public static void Save(PhuDinhEntities context, List<rChanh> data, Expression<Func<rChanh, bool>> filter)
        {
            Repository<rChanh>.Save(context, data, filter, (p => p.Ma == 0), ((p1, p2) => p1.Ma == p2.Ma));
        }
    }
}
