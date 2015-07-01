﻿using PhuDinhDataEntity;
using System.Linq;
using System.Data.Entity;

namespace PhuDinhEFClientContext.Repository
{
    public static class tDonHangRepository
    {
        public static IQueryable<tDonHang> GetDataQuery(IQueryable<tDonHang> query)
        {
            return query.Include("tChiTietDonHangs").OrderByDescending(p => p.Ngay);
        }
    }
}