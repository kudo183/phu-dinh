using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhData.Repository
{
    public static class rLoaiChiPhiRepository
    {
        private static void RemoveOrUpdateItem(PhuDinhEntities context, List<rLoaiChiPhi> gridDataSource, Expression<Func<rLoaiChiPhi, bool>> filter)
        {
            foreach (var item in GetData(context, filter))
            {
                var entity = gridDataSource.FirstOrDefault(p => p.Ma == item.Ma);
                //remove deleted item
                if (entity == null)
                {
                    context.rLoaiChiPhis.Remove(item);
                }
                //update exist item
                else
                {
                    item.TenLoaiChiPhi = entity.TenLoaiChiPhi;
                }
            }
        }

        private static void AddNewItem(PhuDinhEntities context, List<rLoaiChiPhi> gridDataSource)
        {
            foreach (var item in gridDataSource)
            {
                if (item.Ma == 0)
                {
                    context.rLoaiChiPhis.Add(item);
                }
            }
        }

        public static List<rLoaiChiPhi> GetData(Expression<Func<rLoaiChiPhi, bool>> filter)
        {
            var context = new PhuDinhEntities();
            return GetData(context, filter);
        }

        public static List<rLoaiChiPhi> GetData(PhuDinhEntities context, Expression<Func<rLoaiChiPhi, bool>> filter)
        {
            return context.rLoaiChiPhis.Where(filter).ToList();
        }

        public static void Save(List<rLoaiChiPhi> data, Expression<Func<rLoaiChiPhi, bool>> filter)
        {
            var context = new PhuDinhEntities();
            Save(context, data, filter);
        }

        public static void Save(PhuDinhEntities context, List<rLoaiChiPhi> data, Expression<Func<rLoaiChiPhi, bool>> filter)
        {
            RemoveOrUpdateItem(context, data, filter);
            AddNewItem(context, data);
            context.SaveChanges();
        }
    }
}
