using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhData.Repository
{
    public static class rNuocRepository
    {
        private static void RemoveOrUpdateItem(PhuDinhEntities context, List<rNuoc> gridDataSource, Expression<Func<rNuoc, bool>> filter)
        {
            foreach (var item in GetData(context, filter))
            {
                var entity = gridDataSource.FirstOrDefault(p => p.Ma == item.Ma);
                //remove deleted item
                if (entity == null)
                {
                    context.rNuocs.Remove(item);
                }
                //update exist item
                else
                {
                    item.TenNuoc = entity.TenNuoc;
                }
            }
        }

        private static void AddNewItem(PhuDinhEntities context, List<rNuoc> gridDataSource)
        {
            foreach (var item in gridDataSource)
            {
                if (item.Ma == 0)
                {
                    context.rNuocs.Add(item);
                }
            }
        }

        public static List<rNuoc> GetData(Expression<Func<rNuoc, bool>> filter)
        {
            var context = new PhuDinhEntities();
            return GetData(context, filter);
        }

        public static List<rNuoc> GetData(PhuDinhEntities context, Expression<Func<rNuoc, bool>> filter)
        {
            return context.rNuocs.Where(filter).ToList();
        }

        public static void Save(List<rNuoc> data, Expression<Func<rNuoc, bool>> filter)
        {
            var context = new PhuDinhEntities();
            Save(context, data, filter);
        }

        public static void Save(PhuDinhEntities context, List<rNuoc> data, Expression<Func<rNuoc, bool>> filter)
        {
            RemoveOrUpdateItem(context, data, filter);
            AddNewItem(context, data);
            context.SaveChanges();
        }
    }
}
