using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhData.Repository
{
    public static class rDiaDiemRepository
    {
        public static List<rDiaDiem> GetData(PhuDinhEntities context, Expression<Func<rDiaDiem, bool>> filter)
        {
            return Repository<rDiaDiem>.GetData(context, filter).ToList();
        }

        public static void Save(PhuDinhEntities context, List<rDiaDiem> data, Expression<Func<rDiaDiem, bool>> filter)
        {
            Repository<rDiaDiem>.Save(context, data, filter, (p => p.Ma == 0), ((p1, p2) => p1.Ma == p2.Ma));
        }
    }
}
