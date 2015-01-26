using System.Linq;

namespace PhuDinhData.Repository
{
    public static class rDiaDiemRepository
    {
        public static IQueryable<rDiaDiem> GetDataQuery(IQueryable<rDiaDiem> query)
        {
            return query.OrderBy(p => p.Tinh);
        }
    }
}
