using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhData.Repository
{
    public static class tNhapNguyenLieuRepository
    {
        public static List<tNhapNguyenLieu> GetData(PhuDinhEntities context, Expression<Func<tNhapNguyenLieu, bool>> filter)
        {
            return Repository<tNhapNguyenLieu>.GetData(context, filter).OrderByDescending(p => p.Ngay).ToList();
        }

        public static void Save(PhuDinhEntities context, List<tNhapNguyenLieu> data, Expression<Func<tNhapNguyenLieu, bool>> filter)
        {
            Repository<tNhapNguyenLieu>.Save(context, data, filter, (p => p.Ma == 0), ((p1, p2) => p1.Ma == p2.Ma));
        }
    }
}
