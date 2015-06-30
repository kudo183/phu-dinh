﻿using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhEFClientContext.Repository
{
    public static class rNhanVienRepository
    {
        public static IQueryable<rNhanVien> GetDataQuery(IQueryable<rNhanVien> query)
        {
            return query.OrderBy(p => p.TenNhanVien);
        }
    }
}
