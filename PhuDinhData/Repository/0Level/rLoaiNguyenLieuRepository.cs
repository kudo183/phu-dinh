using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhData.Repository
{
    public static class rLoaiNguyenLieuRepository
    {
        public static int GetDataCount(PhuDinhEntities context, Expression<Func<rLoaiNguyenLieu, bool>> filter)
        {
            return GetDataQuery(context, filter).Count();
        }

        public static List<rLoaiNguyenLieu> GetData(PhuDinhEntities context,
            Expression<Func<rLoaiNguyenLieu, bool>> filter,
            int pageSize, int currentPageIndex, int itemCount)
        {
            return Repository<rLoaiNguyenLieu>.PagingData(GetDataQuery(context, filter)
                , pageSize, currentPageIndex, itemCount);
        }

        public static List<rLoaiNguyenLieu> GetData(PhuDinhEntities context, Expression<Func<rLoaiNguyenLieu, bool>> filter)
        {
            return GetDataQuery(context, filter).ToList();
        }

        public static List<Repository<rLoaiNguyenLieu>.ChangedItemData> Save(PhuDinhEntities context, List<rLoaiNguyenLieu> data, List<rLoaiNguyenLieu> origData)
        {
            return Repository<rLoaiNguyenLieu>.Save(context, data, origData, (p => p.Ma == 0), ((p1, p2) => p1.Ma == p2.Ma));
        }

        private static IQueryable<rLoaiNguyenLieu> GetDataQuery(PhuDinhEntities context
            , Expression<Func<rLoaiNguyenLieu, bool>> filter)
        {
            return Repository<rLoaiNguyenLieu>.GetData(context, filter).OrderBy(p => p.TenLoai);
        }
    }
}
