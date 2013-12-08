﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhData.Repository
{
    public static class tMatHangRepository
    {
        public static List<tMatHang> GetData(PhuDinhEntities context, Expression<Func<tMatHang, bool>> filter)
        {
            return Repository<tMatHang>.GetData(context, filter).ToList().OrderBy(p => p.TenMatHang).ToList();
        }

        public static List<Repository<tMatHang>.ChangedItemData> Save(PhuDinhEntities context, List<tMatHang> data, List<tMatHang> origData)
        {
            return Repository<tMatHang>.Save(context, data, origData, (p => p.Ma == 0), ((p1, p2) => p1.Ma == p2.Ma));
        }
    }
}
