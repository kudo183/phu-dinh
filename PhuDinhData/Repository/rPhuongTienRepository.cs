using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhData.Repository
{
    public static class rPhuongTienRepository
    {
        private static void RemoveOrUpdateItem(PhuDinhEntities context, List<rPhuongTien> gridDataSource, Expression<Func<rPhuongTien, bool>> filter)
        {
            foreach (var item in GetData(context, filter))
            {
                var entity = gridDataSource.FirstOrDefault(p => p.Ma == item.Ma);
                //remove deleted item
                if (entity == null)
                {
                    context.rPhuongTiens.Remove(item);
                }
                //update exist item
                else
                {
                    item.TenPhuongTien = entity.TenPhuongTien;
                }
            }
        }

        private static void AddNewItem(PhuDinhEntities context, List<rPhuongTien> gridDataSource)
        {
            foreach (var item in gridDataSource)
            {
                if (item.Ma == 0)
                {
                    context.rPhuongTiens.Add(item);
                }
            }
        }

        public static List<rPhuongTien> GetData(Expression<Func<rPhuongTien, bool>> filter)
        {
            var context = new PhuDinhEntities();
            return GetData(context, filter);
        }

        public static List<rPhuongTien> GetData(PhuDinhEntities context, Expression<Func<rPhuongTien, bool>> filter)
        {
            return context.rPhuongTiens.Where(filter).ToList();
        }

        public static void Save(List<rPhuongTien> data, Expression<Func<rPhuongTien, bool>> filter)
        {
            var context = new PhuDinhEntities();
            Save(context, data, filter);
        }

        public static void Save(PhuDinhEntities context, List<rPhuongTien> data, Expression<Func<rPhuongTien, bool>> filter)
        {
            RemoveOrUpdateItem(context, data, filter);
            AddNewItem(context, data);
            context.SaveChanges();
        }
    }
}
