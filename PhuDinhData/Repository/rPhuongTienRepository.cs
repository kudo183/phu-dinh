using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhData.Repository
{
    public static class rPhuongTienRepository
    {
        public static int GetDataCount(PhuDinhEntities context, Expression<Func<rPhuongTien, bool>> filter)
        {
            return GetDataQuery(context, filter).Count();
        }

        public static List<rPhuongTien> GetData(PhuDinhEntities context,
            Expression<Func<rPhuongTien, bool>> filter,
            int pageSize, int currentPageIndex, int itemCount)
        {
            return Repository<rPhuongTien>.PagingData(GetDataQuery(context, filter)
                , pageSize, currentPageIndex, itemCount);
        }

        public static List<rPhuongTien> GetData(PhuDinhEntities context, Expression<Func<rPhuongTien, bool>> filter)
        {
            return GetDataQuery(context, filter).ToList();
        }

        public static List<Repository<rPhuongTien>.ChangedItemData> Save(PhuDinhEntities context, List<rPhuongTien> data, List<rPhuongTien> origData)
        {
            return Repository<rPhuongTien>.Save(context, data, origData, (p => p.Ma == 0), ((p1, p2) => p1.Ma == p2.Ma));
        }

        private static IQueryable<rPhuongTien> GetDataQuery(PhuDinhEntities context
            , Expression<Func<rPhuongTien, bool>> filter)
        {
            return Repository<rPhuongTien>.GetData(context, filter).OrderBy(p => p.TenPhuongTien);
        }
    }
}
