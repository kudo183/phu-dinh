using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PhuDinhData.Repository
{
    public static class rLoaiHangRepository
    {
        public static List<rLoaiHang> GetData(PhuDinhEntities context, Expression<Func<rLoaiHang, bool>> filter)
        {
            return Repository<rLoaiHang>.GetData(context, filter);
        }

        public static void Save(PhuDinhEntities context, List<rLoaiHang> data, Expression<Func<rLoaiHang, bool>> filter)
        {
            Repository<rLoaiHang>.Save(context, data, filter, (p => p.Ma == 0), ((p1, p2) => p1.Ma == p2.Ma));
        }
    }
}
