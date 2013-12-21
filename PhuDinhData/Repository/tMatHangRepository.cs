using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhData.Repository
{
    public static class tMatHangRepository
    {
        public static int GetDataCount(PhuDinhEntities context, Expression<Func<tMatHang, bool>> filter)
        {
            var data = Repository<tMatHang>.GetData(context, filter).OrderBy(p => p.TenMatHang);
            return data.Count();
        }

        public static List<tMatHang> GetData(PhuDinhEntities context,
            Expression<Func<tMatHang, bool>> filter,
            int pageSize, int currentPageIndex, int itemCount)
        {
            var data = Repository<tMatHang>.GetData(context, filter).OrderBy(p => p.TenMatHang);

            var skippedItem = pageSize * (currentPageIndex - 1);

            var takeItem = itemCount - skippedItem;
            if (takeItem > pageSize)
            {
                takeItem = pageSize;
            }

            if (takeItem <= 0)
            {
                return new List<tMatHang>();
            }

            return data.Skip(skippedItem).Take(takeItem).ToList();
        }

        public static List<tMatHang> GetData(PhuDinhEntities context, Expression<Func<tMatHang, bool>> filter)
        {
            return Repository<tMatHang>.GetData(context, filter).OrderBy(p => p.TenMatHang).ToList();
        }

        public static List<Repository<tMatHang>.ChangedItemData> Save(PhuDinhEntities context, List<tMatHang> data, List<tMatHang> origData)
        {
            return Repository<tMatHang>.Save(context, data, origData, (p => p.Ma == 0), ((p1, p2) => p1.Ma == p2.Ma));
        }
    }
}
