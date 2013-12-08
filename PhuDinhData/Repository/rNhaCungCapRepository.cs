using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhData.Repository
{
    public static class rNhaCungCapRepository
    {
        public static List<rNhaCungCap> GetData(PhuDinhEntities context, Expression<Func<rNhaCungCap, bool>> filter)
        {
            return Repository<rNhaCungCap>.GetData(context, filter).ToList().OrderBy(p => p.TenNhaCungCap).ToList();
        }

        public static List<Repository<rNhaCungCap>.ChangedItemData> Save(PhuDinhEntities context, List<rNhaCungCap> data, List<rNhaCungCap> origData)
        {
            return Repository<rNhaCungCap>.Save(context, data, origData, (p => p.Ma == 0), ((p1, p2) => p1.Ma == p2.Ma));
        }
    }
}
