using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhData.Repository
{
    public static class rKhachHangChanhRepository
    {
        public static int GetDataCount(PhuDinhEntities context, Expression<Func<rKhachHangChanh, bool>> filter)
        {
            var data = Repository<rKhachHangChanh>.GetData(context, filter).OrderBy(p => p.rKhachHang.TenKhachHang);
            return data.Count();
        }

        public static List<rKhachHangChanh> GetData(PhuDinhEntities context,
            Expression<Func<rKhachHangChanh, bool>> filter,
            int pageSize, int currentPageIndex, int itemCount)
        {
            var data = Repository<rKhachHangChanh>.GetData(context, filter).OrderBy(p => p.rKhachHang.TenKhachHang);

            return Repository<rKhachHangChanh>.PagingData(data, pageSize, currentPageIndex, itemCount);
        }

        public static List<rKhachHangChanh> GetData(PhuDinhEntities context, Expression<Func<rKhachHangChanh, bool>> filter)
        {
            return Repository<rKhachHangChanh>.GetData(context, filter).OrderBy(p => p.rKhachHang.TenKhachHang).ToList();
        }

        public static List<Repository<rKhachHangChanh>.ChangedItemData> Save(PhuDinhEntities context, List<rKhachHangChanh> data, List<rKhachHangChanh> origData)
        {
            return Repository<rKhachHangChanh>.Save(context, data, origData, (p => p.Ma == 0), ((p1, p2) => p1.Ma == p2.Ma));
        }
    }
}
