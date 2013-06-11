using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhData.Repository
{
    public static class rKhachHangRepository
    {
        private static void RemoveOrUpdateItem(PhuDinhEntities context, List<rKhachHang> gridDataSource, Expression<Func<rKhachHang, bool>> filter)
        {
            foreach (var item in GetData(context, filter))
            {
                var entity = gridDataSource.FirstOrDefault(p => p.Ma == item.Ma);
                //remove deleted item
                if (entity == null)
                {
                    context.rKhachHangs.Remove(item);
                }
                //update exist item
                else
                {
                    item.TenKhachHang = entity.TenKhachHang;
                }
            }
        }

        private static void AddNewItem(PhuDinhEntities context, List<rKhachHang> gridDataSource)
        {
            foreach (var item in gridDataSource)
            {
                if (item.Ma == 0)
                {
                    context.rKhachHangs.Add(item);
                }
            }
        }

        public static List<rKhachHang> GetData(Expression<Func<rKhachHang, bool>> filter)
        {
            var context = new PhuDinhEntities();
            return GetData(context, filter);
        }

        public static List<rKhachHang> GetData(PhuDinhEntities context, Expression<Func<rKhachHang, bool>> filter)
        {
            return context.rKhachHangs.Where(filter).ToList();
        }

        public static void Save(List<rKhachHang> data, Expression<Func<rKhachHang, bool>> filter)
        {
            var context = new PhuDinhEntities();
            Save(context, data, filter);
        }

        public static void Save(PhuDinhEntities context, List<rKhachHang> data, Expression<Func<rKhachHang, bool>> filter)
        {
            RemoveOrUpdateItem(context, data, filter);
            AddNewItem(context, data);
            context.SaveChanges();
        }
    }
}
