using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhData.Repository
{
    public static class rChanhRepository
    {

        private static void RemoveOrUpdateItem(PhuDinhEntities context, List<rChanh> gridDataSource, Expression<Func<rChanh, bool>> filter)
        {
            foreach (var item in GetData(context, filter))
            {
                var entity = gridDataSource.FirstOrDefault(p => p.Ma == item.Ma);
                //remove deleted item
                if (entity == null)
                {
                    context.rChanhs.Remove(item);
                }
                //update exist item
                else
                {
                    item.MaBaiXe = entity.MaBaiXe;
                    item.TenChanh = entity.TenChanh;
                }
            }
        }

        private static void AddNewItem(PhuDinhEntities context, List<rChanh> gridDataSource)
        {
            foreach (var item in gridDataSource)
            {
                if (item.Ma == 0)
                {
                    context.rChanhs.Add(item);
                }
            }
        }

        public static List<rChanh> GetData(Expression<Func<rChanh, bool>> filter)
        {
            var context = new PhuDinhEntities();
            return GetData(context, filter);
        }

        public static List<rChanh> GetData(PhuDinhEntities context, Expression<Func<rChanh, bool>> filter)
        {
            return context.rChanhs.Where(filter).ToList();
        }

        public static void Save(List<rChanh> data, Expression<Func<rChanh, bool>> filter)
        {
            var context = new PhuDinhEntities();
            Save(context, data, filter);
        }

        public static void Save(PhuDinhEntities context, List<rChanh> data, Expression<Func<rChanh, bool>> filter)
        {
            RemoveOrUpdateItem(context, data, filter);
            AddNewItem(context, data);
            context.SaveChanges();
        }
    }
}
