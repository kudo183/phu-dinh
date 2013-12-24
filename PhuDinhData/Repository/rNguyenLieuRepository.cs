using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhData.Repository
{
    public static class rNguyenLieuRepository
    {
        public static int GetDataCount(PhuDinhEntities context, Expression<Func<rNguyenLieu, bool>> filter)
        {
            return GetDataQuery(context, filter).Count();
        }

        public static List<rNguyenLieu> GetData(PhuDinhEntities context,
            Expression<Func<rNguyenLieu, bool>> filter,
            int pageSize, int currentPageIndex, int itemCount)
        {
            return Repository<rNguyenLieu>.PagingData(GetDataQuery(context, filter)
                , pageSize, currentPageIndex, itemCount);
        }

        public static List<rNguyenLieu> GetData(PhuDinhEntities context, Expression<Func<rNguyenLieu, bool>> filter)
        {
            return GetDataQuery(context, filter).ToList();
        }

        public static List<Repository<rNguyenLieu>.ChangedItemData> Save(PhuDinhEntities context
            , List<rNguyenLieu> data, List<rNguyenLieu> origData)
        {
            return Repository<rNguyenLieu>.Save(context, data, origData
                , (p => p.Ma == 0)
                , ((p1, p2) => p1.Ma == p2.Ma));
        }

        private static IQueryable<rNguyenLieu> GetDataQuery(PhuDinhEntities context
            , Expression<Func<rNguyenLieu, bool>> filter)
        {
            return Repository<rNguyenLieu>.GetData(context, filter)
                .OrderBy(p => p.rLoaiNguyenLieu.TenLoai)
                .ThenBy(p => p.DuongKinh);
        }
    }
}
