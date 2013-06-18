using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PhuDinhData.Repository
{
    public static class rNhanVienGiaoHangRepository
    {
        public static List<rNhanVienGiaoHang> GetData(PhuDinhEntities context, Expression<Func<rNhanVienGiaoHang, bool>> filter)
        {
            return Repository<rNhanVienGiaoHang>.GetData(context, filter);
        }

        public static void Save(PhuDinhEntities context, List<rNhanVienGiaoHang> data, Expression<Func<rNhanVienGiaoHang, bool>> filter)
        {
            Repository<rNhanVienGiaoHang>.Save(context, data, filter, (p => p.Ma == 0), ((p1, p2) => p1.Ma == p2.Ma));
        }
    }
}
