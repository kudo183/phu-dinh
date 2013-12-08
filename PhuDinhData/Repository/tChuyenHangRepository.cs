using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhData.Repository
{
    public static class tChuyenHangRepository
    {
        public static List<tChuyenHang> GetData(PhuDinhEntities context, Expression<Func<tChuyenHang, bool>> filter)
        {
            return Repository<tChuyenHang>.GetData(context, filter).OrderByDescending(p => p.Ngay).ToList();
        }

        public static List<Repository<tChuyenHang>.ChangedItemData> Save(PhuDinhEntities context, List<tChuyenHang> data, List<tChuyenHang> origData)
        {
            return Repository<tChuyenHang>.Save(context, data, origData, (p => p.Ma == 0), ((p1, p2) => p1.Ma == p2.Ma));
        }
    }
}
