using System.Collections.Generic;
using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhEFClientContext.Repository
{
    public static class rLoaiHangRepository
    {
        public static IQueryable<rLoaiHang> GetDataQuery(IQueryable<rLoaiHang> query)
        {
            return query.OrderBy(p => p.TenLoai);
        }

        public static IQueryable<rLoaiHang> GetDataQueryAndRelatedTables(IQueryable<rLoaiHang> query, ref List<string> relatedTables)
        {
            relatedTables.Clear();
            relatedTables.Add("rLoaiHang");
            return query.OrderBy(p => p.TenLoai);
        }
    }
}
