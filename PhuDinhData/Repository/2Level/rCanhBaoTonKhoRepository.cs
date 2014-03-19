using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhData.Repository
{
    public static class rCanhBaoTonKhoRepository
    {
        public static int GetDataCount(PhuDinhEntities context, Expression<Func<rCanhBaoTonKho, bool>> filter)
        {
            return GetDataQuery(context, filter).Count();
        }

        public static List<rCanhBaoTonKho> GetData(PhuDinhEntities context,
            Expression<Func<rCanhBaoTonKho, bool>> filter,
            int pageSize, int currentPageIndex, int itemCount)
        {
            return Repository<rCanhBaoTonKho>.PagingData(GetDataQuery(context, filter)
                , pageSize, currentPageIndex, itemCount);
        }

        public static List<rCanhBaoTonKho> GetData(PhuDinhEntities context, Expression<Func<rCanhBaoTonKho, bool>> filter)
        {
            return GetDataQuery(context, filter).ToList();
        }

        public static List<Repository<rCanhBaoTonKho>.ChangedItemData> Save(PhuDinhEntities context, List<rCanhBaoTonKho> data, List<rCanhBaoTonKho> origData)
        {
            return Repository<rCanhBaoTonKho>.Save(context, data, origData, (p => p.Ma == 0), ((p1, p2) => p1.Ma == p2.Ma));
        }

        private static IQueryable<rCanhBaoTonKho> GetDataQuery(PhuDinhEntities context
            , Expression<Func<rCanhBaoTonKho, bool>> filter)
        {
            return Repository<rCanhBaoTonKho>.GetData(context, filter).OrderBy(p => p.rKhoHang.TenKho).ThenBy(p => p.tMatHang.TenMatHang);
        }
    }
}
