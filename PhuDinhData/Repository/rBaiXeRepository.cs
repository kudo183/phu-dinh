using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhData.Repository
{
    public static class rBaiXeRepository
    {
        private static void RemoveOrUpdateItem(PhuDinhEntities context, List<rBaiXe> gridDataSource, Expression<Func<rBaiXe, bool>> filter)
        {
            foreach (var item in GetData(context, filter))
            {
                var entity = gridDataSource.FirstOrDefault(p => p.Ma == item.Ma);
                //remove deleted item
                if (entity == null)
                {
                    context.rBaiXes.Remove(item);
                }
                //update exist item
                else
                {
                    item.DiaDiemBaiXe = entity.DiaDiemBaiXe;
                }
            }
        }

        private static void AddNewItem(PhuDinhEntities context, List<rBaiXe> gridDataSource)
        {
            foreach (var item in gridDataSource)
            {
                if (item.Ma == 0)
                {
                    context.rBaiXes.Add(item);
                }
            }
        }

        public static List<rBaiXe> GetData(Expression<Func<rBaiXe, bool>> filter)
        {
            var context = new PhuDinhEntities();
            return GetData(context, filter);
        }

        public static List<rBaiXe> GetData(PhuDinhEntities context, Expression<Func<rBaiXe, bool>> filter)
        {
            return context.rBaiXes.Where(filter).ToList();
        }

        public static void Save(List<rBaiXe> data, Expression<Func<rBaiXe, bool>> filter)
        {
            var context = new PhuDinhEntities();
            Save(context, data, filter);
        }

        public static void Save(PhuDinhEntities context, List<rBaiXe> data, Expression<Func<rBaiXe, bool>> filter)
        {
            RemoveOrUpdateItem(context, data, filter);
            AddNewItem(context, data);
            context.SaveChanges();
        }
    }
}
