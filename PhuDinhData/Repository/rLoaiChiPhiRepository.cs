using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhData.Repository
{
    public static class rLoaiChiPhiRepository
    {
        public static List<rLoaiChiPhi> GetData(PhuDinhEntities context, Expression<Func<rLoaiChiPhi, bool>> filter)
        {
            return Repository<rLoaiChiPhi>.GetData(context, filter).ToList().OrderBy(p => p.TenLoaiChiPhi).ToList();
        }

        public static List<Repository<rLoaiChiPhi>.ChangedItemData> Save(PhuDinhEntities context, List<rLoaiChiPhi> data, List<rLoaiChiPhi> origData)
        {
            return Repository<rLoaiChiPhi>.Save(context, data, origData, (p => p.Ma == 0), ((p1, p2) => p1.Ma == p2.Ma));
        }
    }
}
