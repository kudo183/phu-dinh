using PhuDinhDataEntity;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhODataClientContext.Repository
{
    public static class tTonKhoRepository
    {
        public static IQueryable<tTonKho> GetDataQuery(IQueryable<tTonKho> query)
        {
            return query.OrderByDescending(p => p.Ngay);
        }
    }
}
