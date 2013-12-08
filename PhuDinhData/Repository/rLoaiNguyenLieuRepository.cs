using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhData.Repository
{
    public static class rLoaiNguyenLieuRepository
    {
        public static List<rLoaiNguyenLieu> GetData(PhuDinhEntities context, Expression<Func<rLoaiNguyenLieu, bool>> filter)
        {
            return Repository<rLoaiNguyenLieu>.GetData(context, filter).ToList().OrderBy(p => p.TenLoai).ToList();
        }

        public static List<Repository<rLoaiNguyenLieu>.ChangedItemData> Save(PhuDinhEntities context, List<rLoaiNguyenLieu> data, List<rLoaiNguyenLieu> origData)
        {
            return Repository<rLoaiNguyenLieu>.Save(context, data, origData, (p => p.Ma == 0), ((p1, p2) => p1.Ma == p2.Ma));
        }
    }
}
