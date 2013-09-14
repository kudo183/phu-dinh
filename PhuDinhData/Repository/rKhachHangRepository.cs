using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhData.Repository
{
    public static class rKhachHangRepository
    {
        public static List<rKhachHang> GetData(PhuDinhEntities context, Expression<Func<rKhachHang, bool>> filter)
        {
            return Repository<rKhachHang>.GetData(context, filter).ToList().OrderBy(p => p.TenKhachHang).ToList();
        }

        public static void Save(PhuDinhEntities context, List<rKhachHang> data, Expression<Func<rKhachHang, bool>> filter)
        {
            Repository<rKhachHang>.Save(context, data, filter, (p => p.Ma == 0), ((p1, p2) => p1.Ma == p2.Ma));
        }
    }
}
