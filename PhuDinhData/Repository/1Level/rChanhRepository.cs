using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhData.Repository
{
    public static class rChanhRepository
    {
        public static IQueryable<rChanh> GetDataQuery(IQueryable<rChanh> query)
        {
            return query.OrderBy(p => p.rBaiXe.DiaDiemBaiXe).ThenBy(p => p.TenChanh);
        }
    }
}
