using System.Linq;

namespace PhuDinhData.Repository
{
    public static class rCanhBaoTonKhoRepository
    {
        public static IQueryable<rCanhBaoTonKho> GetDataQuery(IQueryable<rCanhBaoTonKho> query)
        {
            return query.OrderBy(p => p.rKhoHang.TenKho).ThenBy(p => p.tMatHang.TenMatHang);
        }
    }
}
