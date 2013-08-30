using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhData.Repository
{
    public static class rNuocRepository
    {
        public static List<rNuoc> GetData(PhuDinhEntities context, Expression<Func<rNuoc, bool>> filter)
        {
            return Repository<rNuoc>.GetData(context, filter).ToList();
        }

        public static void Save(PhuDinhEntities context, List<rNuoc> data, Expression<Func<rNuoc, bool>> filter)
        {
            Repository<rNuoc>.Save(context, data, filter, (p => p.Ma == 0), ((p1, p2) => p1.Ma == p2.Ma));
        }
    }
}
