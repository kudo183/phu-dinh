using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhData.Repository
{
    public static class tMatHangRepository
    {
        private static void RemoveOrUpdateItem(PhuDinhEntities context, List<tMatHang> gridDataSource, Expression<Func<tMatHang, bool>> filter)
        {
            foreach (var item in GetData(context, filter))
            {
                var entity = gridDataSource.FirstOrDefault(p => p.Ma == item.Ma);
                //remove deleted item
                if (entity == null)
                {
                    context.tMatHangs.Remove(item);
                }
                //update exist item
                else
                {
                    item.MaLoai = entity.MaLoai;
                    item.TenMatHang = entity.TenMatHang;
                }
            }
        }

        private static void AddNewItem(PhuDinhEntities context, List<tMatHang> gridDataSource)
        {
            foreach (var item in gridDataSource)
            {
                if (item.Ma == 0)
                {
                    context.tMatHangs.Add(item);
                }
            }
        }

        public static List<tMatHang> GetData(Expression<Func<tMatHang, bool>> filter)
        {
            var context = new PhuDinhEntities();
            return GetData(context, filter);
        }

        public static List<tMatHang> GetData(PhuDinhEntities context, Expression<Func<tMatHang, bool>> filter)
        {
            return context.tMatHangs.Where(filter).ToList();
        }

        public static void Save(List<tMatHang> data, Expression<Func<tMatHang, bool>> filter)
        {
            var context = new PhuDinhEntities();
            Save(context, data, filter);
        }

        public static void Save(PhuDinhEntities context, List<tMatHang> data, Expression<Func<tMatHang, bool>> filter)
        {
            RemoveOrUpdateItem(context, data, filter);
            AddNewItem(context, data);
            context.SaveChanges();
        }
    }
}
