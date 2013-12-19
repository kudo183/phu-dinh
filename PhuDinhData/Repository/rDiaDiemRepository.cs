using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhData.Repository
{
    public static class rDiaDiemRepository
    {
        public static List<rDiaDiem> GetData(PhuDinhEntities context, Expression<Func<rDiaDiem, bool>> filter, int pageSize, int currentPageIndex, out int itemCount)
        {
            var data = Repository<rDiaDiem>.GetData(context, filter).OrderBy(p => p.Tinh);

            itemCount = data.Count();

            var skippedItem = pageSize * (currentPageIndex - 1);

            if (skippedItem < 0)
            {
                return new List<rDiaDiem>();
            }

            var takeItem = itemCount - skippedItem;
            if (takeItem > pageSize)
            {
                takeItem = pageSize;
            }

            return data.Skip(skippedItem).Take(takeItem).ToList();
        }

        public static List<rDiaDiem> GetData(PhuDinhEntities context, Expression<Func<rDiaDiem, bool>> filter)
        {
            return Repository<rDiaDiem>.GetData(context, filter).OrderBy(p => p.Tinh).ToList();
        }

        public static List<Repository<rDiaDiem>.ChangedItemData> Save(PhuDinhEntities context, List<rDiaDiem> data, List<rDiaDiem> origData)
        {
            return Repository<rDiaDiem>.Save(context, data, origData, (p => p.Ma == 0), ((p1, p2) => p1.Ma == p2.Ma));
        }
    }
}
