using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhData.Repository
{
    public static class rDiaDiemRepository
    {
        public static int GetDataCount(PhuDinhEntities context, Expression<Func<rDiaDiem, bool>> filter)
        {
            return GetDataQuery(context, filter).Count();
        }

        public static List<rDiaDiem> GetData(PhuDinhEntities context,
            Expression<Func<rDiaDiem, bool>> filter,
            int pageSize, int currentPageIndex, int itemCount)
        {
            return Repository<rDiaDiem>.PagingData(GetDataQuery(context, filter)
                , pageSize, currentPageIndex, itemCount);
        }

        public static List<rDiaDiem> GetData(PhuDinhEntities context, Expression<Func<rDiaDiem, bool>> filter)
        {
            return GetDataQuery(context, filter).ToList();
        }

        public static List<Repository<rDiaDiem>.ChangedItemData> Save(PhuDinhEntities context, List<rDiaDiem> data, List<rDiaDiem> origData)
        {
            return Repository<rDiaDiem>.Save(context, data, origData, (p => p.Ma == 0), ((p1, p2) => p1.Ma == p2.Ma));
        }

        private static IQueryable<rDiaDiem> GetDataQuery(PhuDinhEntities context
            , Expression<Func<rDiaDiem, bool>> filter)
        {
            return Repository<rDiaDiem>.GetData(context, filter).OrderBy(p => p.Tinh);
        }
    }
}
