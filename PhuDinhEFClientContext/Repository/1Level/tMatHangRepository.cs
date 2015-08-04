﻿using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhEFClientContext.Repository
{
    public static class tMatHangRepository
    {
        public static IQueryable<tMatHang> OrderBy(IQueryable<tMatHang> query)
        {
            return query.OrderBy(p => p.TenMatHang);
        }
    }
}
