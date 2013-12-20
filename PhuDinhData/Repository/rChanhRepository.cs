using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhData.Repository
{
    public static class rChanhRepository
    {
        public static int GetDataCount(PhuDinhEntities context, Expression<Func<rChanh, bool>> filter)
        {
            var data = Repository<rChanh>.GetData(context, filter).OrderBy(p => p.rBaiXe.DiaDiemBaiXe).ThenBy(p => p.TenChanh);
            return data.Count();
        }

        public static List<rChanh> GetData(PhuDinhEntities context,
            Expression<Func<rChanh, bool>> filter,
            int pageSize, int currentPageIndex, int itemCount)
        {
            var data = Repository<rChanh>.GetData(context, filter).OrderBy(p => p.rBaiXe.DiaDiemBaiXe).ThenBy(p => p.TenChanh);

            var skippedItem = pageSize * (currentPageIndex - 1);

            var takeItem = itemCount - skippedItem;
            if (takeItem > pageSize)
            {
                takeItem = pageSize;
            }

            if (takeItem <= 0)
            {
                return new List<rChanh>();
            }

            return data.Skip(skippedItem).Take(takeItem).ToList();
        }

        public static List<rChanh> GetData(PhuDinhEntities context, Expression<Func<rChanh, bool>> filter)
        {
            return Repository<rChanh>.GetData(context, filter).OrderBy(p => p.rBaiXe.DiaDiemBaiXe).ThenBy(p => p.TenChanh).ToList();
        }

        public static List<Repository<rChanh>.ChangedItemData> Save(PhuDinhEntities context, List<rChanh> data, List<rChanh> origData)
        {
            return Repository<rChanh>.Save(context, data, origData, (p => p.Ma == 0), ((p1, p2) => p1.Ma == p2.Ma));
        }
    }
}
