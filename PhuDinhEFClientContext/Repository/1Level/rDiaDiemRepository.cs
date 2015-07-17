using System.Collections.Generic;
using System.Data.Entity;
using PhuDinhDataEntity;
using System.Linq;

namespace PhuDinhEFClientContext.Repository
{
    public static class rDiaDiemRepository
    {
        public static IQueryable<rDiaDiem> GetDataQuery(IQueryable<rDiaDiem> query)
        {
            return query.OrderBy(p => p.Tinh);
        }

        public static IQueryable<rDiaDiem> GetDataQueryAndRelatedTables(IQueryable<rDiaDiem> query, ref List<string> relatedTables)
        {
            relatedTables.Clear();
            relatedTables.Add("rDiaDiem");
            relatedTables.Add("rNuoc");
            return query.Include("rNuoc").OrderBy(p => p.Tinh);
        }
    }
}
