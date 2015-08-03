using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhEFClientContext.Repository
{
    public static class rCanhBaoTonKhoRepository
    {
        public static IQueryable<rCanhBaoTonKho> GetDataQuery(IQueryable<rCanhBaoTonKho> query)
        {
            return OrderBy(query);
        }

        public static IQueryable<rCanhBaoTonKho> OrderBy(IQueryable<rCanhBaoTonKho> query)
        {
            return query.OrderBy(p => p.rKhoHang.TenKho).ThenBy(p => p.tMatHang.TenMatHang);
        }
    }
}
