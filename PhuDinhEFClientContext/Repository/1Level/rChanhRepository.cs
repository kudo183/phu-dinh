using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhEFClientContext.Repository
{
    public static class rChanhRepository
    {
        public static IQueryable<rChanh> OrderBy(IQueryable<rChanh> query)
        {
            return query.OrderBy(p => p.rBaiXe.DiaDiemBaiXe).ThenBy(p => p.TenChanh);
        }
    }
}
