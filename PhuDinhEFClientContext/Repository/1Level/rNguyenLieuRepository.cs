﻿using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhEFClientContext.Repository
{
    public static class rNguyenLieuRepository
    {
        public static IQueryable<rNguyenLieu> GetDataQuery(IQueryable<rNguyenLieu> query)
        {
            return query.OrderBy(p => p.rLoaiNguyenLieu.TenLoai).ThenBy(p => p.DuongKinh);
        }
    }
}
