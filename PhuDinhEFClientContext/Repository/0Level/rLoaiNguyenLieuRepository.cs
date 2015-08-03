﻿using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhEFClientContext.Repository
{
    public static class rLoaiNguyenLieuRepository
    {
        public static IQueryable<rLoaiNguyenLieu> GetDataQuery(IQueryable<rLoaiNguyenLieu> query)
        {
            return OrderBy(query);
        }

        public static IQueryable<rLoaiNguyenLieu> OrderBy(IQueryable<rLoaiNguyenLieu> query)
        {
            return query.OrderBy(p => p.TenLoai);
        }
    }
}
