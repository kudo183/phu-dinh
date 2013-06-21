using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhData.Repository
{
    public static class tChiPhiNhanVienGiaoHangRepository
    {
        public static List<tChiPhiNhanVienGiaoHang> GetData(PhuDinhEntities context, Expression<Func<tChiPhiNhanVienGiaoHang, bool>> filter)
        {
            return Repository<tChiPhiNhanVienGiaoHang>.GetData(context, filter).OrderByDescending(p => p.Ngay).ToList();
        }

        public static void Save(PhuDinhEntities context, List<tChiPhiNhanVienGiaoHang> data, Expression<Func<tChiPhiNhanVienGiaoHang, bool>> filter)
        {
            Repository<tChiPhiNhanVienGiaoHang>.Save(context, data, filter, (p => p.Ma == 0), ((p1, p2) => p1.Ma == p2.Ma));
        }
    }
}
