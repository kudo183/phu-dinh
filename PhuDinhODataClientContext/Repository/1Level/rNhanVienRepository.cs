using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhODataClientContext.Repository
{
    public static class rNhanVienRepository
    {
        public static IQueryable<rNhanVien> GetDataQuery(IQueryable<rNhanVien> query)
        {
            return query.OrderBy(p => p.TenNhanVien);
        }
    }
}
