using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhData.Repository
{
    public static class rLoaiHangRepository
    {
        public static int GetDataCount(PhuDinhEntities context, Expression<Func<rLoaiHang, bool>> filter)
        {
            return GetDataQuery(context, filter).Count();
        }

        public static List<rLoaiHang> GetData(PhuDinhEntities context,
            Expression<Func<rLoaiHang, bool>> filter,
            int pageSize, int currentPageIndex, int itemCount)
        {
            return Repository<rLoaiHang>.PagingData(GetDataQuery(context, filter)
                , pageSize, currentPageIndex, itemCount);
        }

        public static List<rLoaiHang> GetData(PhuDinhEntities context, Expression<Func<rLoaiHang, bool>> filter)
        {
            return GetDataQuery(context, filter).ToList();
        }

        public static List<Repository<rLoaiHang>.ChangedItemData> Save(PhuDinhEntities context, List<rLoaiHang> data, List<rLoaiHang> origData)
        {
            return Repository<rLoaiHang>.Save(context, data, origData, (p => p.Ma == 0), ((p1, p2) => p1.Ma == p2.Ma));
        }

        private static IQueryable<rLoaiHang> GetDataQuery(PhuDinhEntities context
            , Expression<Func<rLoaiHang, bool>> filter)
        {
            return Repository<rLoaiHang>.GetData(context, filter).OrderBy(p => p.TenLoai);
        }
    }
}
