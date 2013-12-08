using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhData.Repository
{
    public static class tDonHangRepository
    {
        public static List<tDonHang> GetData(PhuDinhEntities context, Expression<Func<tDonHang, bool>> filter)
        {
            return Repository<tDonHang>.GetData(context, filter).OrderByDescending(p => p.Ngay).ToList();
        }

        public static List<Repository<tDonHang>.ChangedItemData> Save(PhuDinhEntities context, List<tDonHang> data, List<tDonHang> origData)
        {
            return Repository<tDonHang>.Save(context, data, origData, (p => p.Ma == 0), ((p1, p2) => p1.Ma == p2.Ma));
        }
    }
}
