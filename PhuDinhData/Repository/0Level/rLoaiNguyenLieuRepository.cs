using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhData.Repository
{
    public static class rLoaiNguyenLieuRepository
    {
        public static IQueryable<rLoaiNguyenLieu> GetDataQuery(IQueryable<rLoaiNguyenLieu> query)
        {
            return query.OrderBy(p => p.TenLoai);
        }
    }
}
