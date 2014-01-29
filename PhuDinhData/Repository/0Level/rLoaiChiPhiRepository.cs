using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhData.Repository
{
    public static class rLoaiChiPhiRepository
    {
        public static int GetDataCount(PhuDinhEntities context, Expression<Func<rLoaiChiPhi, bool>> filter)
        {
            return GetDataQuery(context, filter).Count();
        }

        public static List<rLoaiChiPhi> GetData(PhuDinhEntities context,
            Expression<Func<rLoaiChiPhi, bool>> filter,
            int pageSize, int currentPageIndex, int itemCount)
        {
            return Repository<rLoaiChiPhi>.PagingData(GetDataQuery(context, filter)
                , pageSize, currentPageIndex, itemCount);
        }

        public static List<rLoaiChiPhi> GetData(PhuDinhEntities context, Expression<Func<rLoaiChiPhi, bool>> filter)
        {
            return GetDataQuery(context, filter).ToList();
        }

        public static List<Repository<rLoaiChiPhi>.ChangedItemData> Save(PhuDinhEntities context, List<rLoaiChiPhi> data, List<rLoaiChiPhi> origData)
        {
            return Repository<rLoaiChiPhi>.Save(context, data, origData, (p => p.Ma == 0), ((p1, p2) => p1.Ma == p2.Ma));
        }

        private static IQueryable<rLoaiChiPhi> GetDataQuery(PhuDinhEntities context
            , Expression<Func<rLoaiChiPhi, bool>> filter)
        {
            return Repository<rLoaiChiPhi>.GetData(context, filter).OrderBy(p => p.TenLoaiChiPhi);
        }
    }
}
