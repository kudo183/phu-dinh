using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhData.Repository
{
    public static class rMatHangNguyenLieuRepository
    {
        public static List<rMatHangNguyenLieu> GetData(PhuDinhEntities context, Expression<Func<rMatHangNguyenLieu, bool>> filter)
        {
            return Repository<rMatHangNguyenLieu>.GetData(context, filter).OrderByDescending(p => p.MaMatHang).ToList();
        }

        public static List<Repository<rMatHangNguyenLieu>.ChangedItemData> Save(PhuDinhEntities context, List<rMatHangNguyenLieu> data, List<rMatHangNguyenLieu> origData)
        {
            return Repository<rMatHangNguyenLieu>.Save(context, data, origData, (p => p.Ma == 0), ((p1, p2) => p1.Ma == p2.Ma));
        }
    }
}
