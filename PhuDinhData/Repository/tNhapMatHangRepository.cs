using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhData.Repository
{
    public static class tNhapMatHangRepository
    {
        public static List<tNhapMatHang> GetData(PhuDinhEntities context, Expression<Func<tNhapMatHang, bool>> filter)
        {
            return Repository<tNhapMatHang>.GetData(context, filter).OrderByDescending(p => p.Ngay).ToList();
        }

        public static void Save(PhuDinhEntities context, List<tNhapMatHang> data, Expression<Func<tNhapMatHang, bool>> filter)
        {
            Repository<tNhapMatHang>.Save(context, data, filter, (p => p.Ma == 0), ((p1, p2) => p1.Ma == p2.Ma));
        }
    }
}
