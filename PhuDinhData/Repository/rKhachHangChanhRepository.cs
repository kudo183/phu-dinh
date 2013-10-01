using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhData.Repository
{
    public static class rKhachHangChanhRepository
    {
        public static List<rKhachHangChanh> GetData(PhuDinhEntities context, Expression<Func<rKhachHangChanh, bool>> filter)
        {
            return Repository<rKhachHangChanh>.GetData(context, filter).ToList().OrderBy(p => p.rKhachHang.TenKhachHang).ToList();
        }

        public static void Save(PhuDinhEntities context, List<rKhachHangChanh> data, Expression<Func<rKhachHangChanh, bool>> filter)
        {
            Repository<rKhachHangChanh>.Save(context, data, filter, (p => p.Ma == 0), ((p1, p2) => p1.Ma == p2.Ma));
        }
    }
}
