using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhData.Repository
{
    public static class rLoaiHangRepository
    {
        private static void RemoveOrUpdateItem(PhuDinhEntities context, List<rLoaiHang> gridDataSource, Expression<Func<rLoaiHang, bool>> filter)
        {
            foreach (var item in GetData(context, filter))
            {
                var entity = gridDataSource.FirstOrDefault(p => p.Ma == item.Ma);
                //remove deleted item
                if (entity == null)
                {
                    context.rLoaiHangs.Remove(item);
                }
                //update exist item
                else
                {
                    item.TenLoai = entity.TenLoai;
                }
            }
        }

        private static void AddNewItem(PhuDinhEntities context, List<rLoaiHang> gridDataSource)
        {
            foreach (var item in gridDataSource)
            {
                if (item.Ma == 0)
                {
                    context.rLoaiHangs.Add(item);
                }
            }
        }

        public static List<rLoaiHang> GetData(Expression<Func<rLoaiHang, bool>> filter)
        {
            var context = new PhuDinhEntities();
            return GetData(context, filter);
        }

        public static List<rLoaiHang> GetData(PhuDinhEntities context, Expression<Func<rLoaiHang, bool>> filter)
        {
            return context.rLoaiHangs.Where(filter).ToList();
        }

        public static void Save(List<rLoaiHang> data, Expression<Func<rLoaiHang, bool>> filter)
        {
            var context = new PhuDinhEntities();
            Save(context, data, filter);
        }

        public static void Save(PhuDinhEntities context, List<rLoaiHang> data, Expression<Func<rLoaiHang, bool>> filter)
        {
            RemoveOrUpdateItem(context, data, filter);
            AddNewItem(context, data);
            context.SaveChanges();
        }
    }
}
