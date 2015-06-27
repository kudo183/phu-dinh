using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhData.Repository
{
    public static class rLoaiHangRepository
    {
        public static IQueryable<rLoaiHang> GetDataQuery(IQueryable<rLoaiHang> query)
        {
            return query.OrderBy(p => p.TenLoai);
        }
    }
}
