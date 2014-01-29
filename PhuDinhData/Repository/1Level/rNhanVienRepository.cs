using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhData.Repository
{
    public static class rNhanVienRepository
    {
        public static int GetDataCount(PhuDinhEntities context, Expression<Func<rNhanVien, bool>> filter)
        {
            return GetDataQuery(context, filter).Count();
        }

        public static List<rNhanVien> GetData(PhuDinhEntities context,
            Expression<Func<rNhanVien, bool>> filter,
            int pageSize, int currentPageIndex, int itemCount)
        {
            return Repository<rNhanVien>.PagingData(GetDataQuery(context, filter)
                , pageSize, currentPageIndex, itemCount);
        }

        public static List<rNhanVien> GetData(PhuDinhEntities context, Expression<Func<rNhanVien, bool>> filter)
        {
            return GetDataQuery(context, filter).ToList();
        }

        public static List<Repository<rNhanVien>.ChangedItemData> Save(PhuDinhEntities context, List<rNhanVien> data, List<rNhanVien> origData)
        {
            return Repository<rNhanVien>.Save(context, data, origData, (p => p.Ma == 0), ((p1, p2) => p1.Ma == p2.Ma));
        }

        private static IQueryable<rNhanVien> GetDataQuery(PhuDinhEntities context
            , Expression<Func<rNhanVien, bool>> filter)
        {
            return Repository<rNhanVien>.GetData(context, filter).OrderBy(p => p.TenNhanVien);
        }
    }
}
