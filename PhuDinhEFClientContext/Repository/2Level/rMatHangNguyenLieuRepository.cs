﻿using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhEFClientContext.Repository
{
    public static class rMatHangNguyenLieuRepository
    {
        public static IQueryable<rMatHangNguyenLieu> OrderBy(IQueryable<rMatHangNguyenLieu> query)
        {
            return query.OrderByDescending(p => p.MaMatHang);
        }
    }
}
