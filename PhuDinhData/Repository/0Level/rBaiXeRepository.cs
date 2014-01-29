using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhData.Repository
{
    public static class rBaiXeRepository
    {
        public static int GetDataCount(PhuDinhEntities context, Expression<Func<rBaiXe, bool>> filter)
        {
            return GetDataQuery(context, filter).Count();
        }

        public static List<rBaiXe> GetData(PhuDinhEntities context,
            Expression<Func<rBaiXe, bool>> filter,
            int pageSize, int currentPageIndex, int itemCount)
        {
            return Repository<rBaiXe>.PagingData(GetDataQuery(context, filter)
                , pageSize, currentPageIndex, itemCount);
        }

        public static List<rBaiXe> GetData(PhuDinhEntities context, Expression<Func<rBaiXe, bool>> filter)
        {
            return GetDataQuery(context, filter).ToList();
        }

        public static List<Repository<rBaiXe>.ChangedItemData> Save(PhuDinhEntities context, List<rBaiXe> data, List<rBaiXe> origData)
        {
            return Repository<rBaiXe>.Save(context, data, origData, (p => p.Ma == 0), ((p1, p2) => p1.Ma == p2.Ma));
        }

        private static IQueryable<rBaiXe> GetDataQuery(PhuDinhEntities context
            , Expression<Func<rBaiXe, bool>> filter)
        {
            return Repository<rBaiXe>.GetData(context, filter).OrderBy(p => p.DiaDiemBaiXe);
        }
    }
}
