using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhData.Repository
{
    public static class rNhanVienRepository
    {
        public static List<rNhanVien> GetData(PhuDinhEntities context, Expression<Func<rNhanVien, bool>> filter)
        {
            return Repository<rNhanVien>.GetData(context, filter).ToList();
        }

        public static void Save(PhuDinhEntities context, List<rNhanVien> data, Expression<Func<rNhanVien, bool>> filter)
        {
            Repository<rNhanVien>.Save(context, data, filter, (p => p.Ma == 0), ((p1, p2) => p1.Ma == p2.Ma));
        }
    }
}
