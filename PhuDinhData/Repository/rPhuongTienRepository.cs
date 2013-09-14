using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhData.Repository
{
    public static class rPhuongTienRepository
    {
        public static List<rPhuongTien> GetData(PhuDinhEntities context, Expression<Func<rPhuongTien, bool>> filter)
        {
            return Repository<rPhuongTien>.GetData(context, filter).ToList().OrderBy(p => p.TenPhuongTien).ToList();
        }

        public static void Save(PhuDinhEntities context, List<rPhuongTien> data, Expression<Func<rPhuongTien, bool>> filter)
        {
            Repository<rPhuongTien>.Save(context, data, filter, (p => p.Ma == 0), ((p1, p2) => p1.Ma == p2.Ma));
        }
    }
}
