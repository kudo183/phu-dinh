using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhData.Repository
{
    public static class rPhuongTienRepository
    {
        public static IQueryable<rPhuongTien> GetDataQuery(IQueryable<rPhuongTien> query)
        {
            return query.OrderBy(p => p.TenPhuongTien);
        }
    }
}
