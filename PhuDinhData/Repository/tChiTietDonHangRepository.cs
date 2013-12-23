using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhData.Repository
{
    public static class tChiTietDonHangRepository
    {
        public static int GetDataCount(PhuDinhEntities context, Expression<Func<tChiTietDonHang, bool>> filter)
        {
            var data = Repository<tChiTietDonHang>.GetData(context, filter).OrderByDescending(p => p.tDonHang.Ngay);
            return data.Count();
        }

        public static List<tChiTietDonHang> GetData(PhuDinhEntities context,
            Expression<Func<tChiTietDonHang, bool>> filter,
            int pageSize, int currentPageIndex, int itemCount)
        {
            var data = Repository<tChiTietDonHang>.GetData(context, filter).OrderByDescending(p => p.tDonHang.Ngay);

            return Repository<tChiTietDonHang>.PagingData(data, pageSize, currentPageIndex, itemCount);
        }

        public static List<tChiTietDonHang> GetData(PhuDinhEntities context, Expression<Func<tChiTietDonHang, bool>> filter)
        {
            return Repository<tChiTietDonHang>.GetData(context, filter).OrderByDescending(p => p.tDonHang.Ngay).ToList();
        }

        public static List<Repository<tChiTietDonHang>.ChangedItemData> Save(PhuDinhEntities context, List<tChiTietDonHang> data, List<tChiTietDonHang> origData)
        {
            var changed = Repository<tChiTietDonHang>.Save(context, data, origData, (p => p.Ma == 0), ((p1, p2) => p1.Ma == p2.Ma));

            if (changed.Count > 0)
            {
                var maDonHangs = changed.Select(p => p.CurrentValues.MaDonHang).ToList();

                BusinessLogics.BusinessLogics.UpdateXong(context, maDonHangs);
            }

            return changed;
        }
    }
}
