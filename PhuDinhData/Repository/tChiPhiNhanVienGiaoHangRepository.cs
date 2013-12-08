using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhData.Repository
{
    public static class tChiPhiRepository
    {
        public static List<tChiPhi> GetData(PhuDinhEntities context, Expression<Func<tChiPhi, bool>> filter)
        {
            return Repository<tChiPhi>.GetData(context, filter).OrderByDescending(p => p.Ngay).ToList();
        }

        public static List<Repository<tChiPhi>.ChangedItemData> Save(PhuDinhEntities context, List<tChiPhi> data, List<tChiPhi> origData)
        {
            return Repository<tChiPhi>.Save(context, data, origData, (p => p.Ma == 0), ((p1, p2) => p1.Ma == p2.Ma));
        }
    }
}
