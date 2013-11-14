using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhData.Repository
{
    public static class rNguyenLieuRepository
    {
        public static List<rNguyenLieu> GetData(PhuDinhEntities context, Expression<Func<rNguyenLieu, bool>> filter)
        {
            return Repository<rNguyenLieu>.GetData(context, filter).ToList().OrderBy(p => p.rLoaiNguyenLieu.TenLoai).ThenBy(p => p.TenNguyenLieu).ToList();
        }

        public static void Save(PhuDinhEntities context, List<rNguyenLieu> data, Expression<Func<rNguyenLieu, bool>> filter)
        {
            Repository<rNguyenLieu>.Save(context, data, filter, (p => p.Ma == 0), ((p1, p2) => p1.Ma == p2.Ma));
        }
    }
}
