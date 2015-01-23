using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhData.Repository
{
    public static class tChiTietChuyenKhoRepository
    {
        public static int GetDataCount(PhuDinhEntities context, Expression<Func<tChiTietChuyenKho, bool>> filter)
        {
            return GetDataQuery(context, filter).Count();
        }

        public static List<tChiTietChuyenKho> GetData(PhuDinhEntities context,
            Expression<Func<tChiTietChuyenKho, bool>> filter,
            int pageSize, int currentPageIndex, int itemCount)
        {
            return Repository<tChiTietChuyenKho>.PagingData(GetDataQuery(context, filter)
                , pageSize, currentPageIndex, itemCount);
        }

        public static List<tChiTietChuyenKho> GetData(PhuDinhEntities context, Expression<Func<tChiTietChuyenKho, bool>> filter)
        {
            return GetDataQuery(context, filter).ToList();
        }

        public static List<Repository<tChiTietChuyenKho>.ChangedItemData> Save(PhuDinhEntities context, List<tChiTietChuyenKho> data, List<tChiTietChuyenKho> origData)
        {
            return Repository<tChiTietChuyenKho>.Save(context, data, origData, (p => p.Ma == 0), ((p1, p2) => p1.Ma == p2.Ma));
        }

        private static IQueryable<tChiTietChuyenKho> GetDataQuery(PhuDinhEntities context
            , Expression<Func<tChiTietChuyenKho, bool>> filter)
        {
            return Repository<tChiTietChuyenKho>.GetData(context, filter).OrderByDescending(p => p.tChuyenKho.Ngay);
        }
    }
}
