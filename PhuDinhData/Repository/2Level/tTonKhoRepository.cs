using System;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhData.Repository
{
    public static class tTonKhoRepository
    {
        public static int SumSoLuong(PhuDinhEntities context, Expression<Func<tTonKho, bool>> filter)
        {
            var q = Repository<tTonKho>.GetDataQuery(context, filter);
            
            if (q.Any() == false)
                return 0;

            return q.Sum(p => p.SoLuong);
        }

        public static IQueryable<tTonKho> GetDataQuery(IQueryable<tTonKho> query)
        {
            return query.OrderByDescending(p => p.Ngay);
        }
    }
}
