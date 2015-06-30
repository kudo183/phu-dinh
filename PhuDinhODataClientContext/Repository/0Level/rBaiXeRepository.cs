using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhODataClientContext.Repository
{
    public static class rBaiXeRepository
    {
        public static IQueryable<rBaiXe> GetDataQuery(IQueryable<rBaiXe> query)
        {
            return query.OrderBy(p => p.DiaDiemBaiXe);
        }
    }
}
