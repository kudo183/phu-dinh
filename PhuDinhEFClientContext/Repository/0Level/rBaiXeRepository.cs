using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhEFClientContext.Repository
{
    public static class rBaiXeRepository
    {
        public static IQueryable<rBaiXe> OrderBy(IQueryable<rBaiXe> query)
        {
            return query.OrderBy(p => p.DiaDiemBaiXe);
        }
    }
}
