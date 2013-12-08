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
            return Repository<rBaiXe>.GetData(context, filter).ToList().OrderBy(p => p.DiaDiemBaiXe).ToList();
        }

        public static List<Repository<rBaiXe>.ChangedItemData> Save(PhuDinhEntities context, List<rBaiXe> data, List<rBaiXe> origData)
        {
            return Repository<rBaiXe>.Save(context, data, origData, (p => p.Ma == 0), ((p1, p2) => p1.Ma == p2.Ma));
        }
    }
}
