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
            return Repository<rDiaDiem>.GetData(context, filter).ToList().OrderBy(p => p.Tinh).ToList();
        }

        public static List<Repository<rDiaDiem>.ChangedItemData> Save(PhuDinhEntities context, List<rDiaDiem> data, List<rDiaDiem> origData)
        {
            return Repository<rDiaDiem>.Save(context, data, origData, (p => p.Ma == 0), ((p1, p2) => p1.Ma == p2.Ma));
        }
    }
}
