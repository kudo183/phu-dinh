﻿using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhEFClientContext.Repository
{
    public static class tChiPhiRepository
    {
        public static IQueryable<tChiPhi> GetDataQuery(IQueryable<tChiPhi> query)
        {
            return query.OrderByDescending(p => p.Ngay);
        }
    }
}