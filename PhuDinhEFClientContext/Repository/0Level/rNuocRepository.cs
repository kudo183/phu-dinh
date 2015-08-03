﻿using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhEFClientContext.Repository
{
    public static class rNuocRepository
    {
        public static IQueryable<rNuoc> GetDataQuery(IQueryable<rNuoc> query)
        {
            return OrderBy(query);
        }

        public static IQueryable<rNuoc> OrderBy(IQueryable<rNuoc> query)
        {
            return query.OrderBy(p => p.TenNuoc);
        }
    }
}
