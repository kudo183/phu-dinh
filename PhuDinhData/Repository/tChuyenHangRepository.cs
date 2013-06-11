using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhData.Repository
{
    public static class tChuyenHangRepository
    {
        private static void RemoveOrUpdateItem(PhuDinhEntities context, List<tChuyenHang> gridDataSource, Expression<Func<tChuyenHang, bool>> filter)
        {
            foreach (var item in GetData(context, filter))
            {
                var entity = gridDataSource.FirstOrDefault(p => p.Ma == item.Ma);
                //remove deleted item
                if (entity == null)
                {
                    context.tChuyenHangs.Remove(item);
                }
                //update exist item
                else
                {
                    item.MaNhanVienGiaoHang = entity.MaNhanVienGiaoHang;
                    item.Ngay = entity.Ngay;
                    item.Gio = entity.Gio;
                }
            }
        }

        private static void AddNewItem(PhuDinhEntities context, List<tChuyenHang> gridDataSource)
        {
            foreach (var item in gridDataSource)
            {
                if (item.Ma == 0)
                {
                    context.tChuyenHangs.Add(item);
                }
            }
        }

        public static List<tChuyenHang> GetData(Expression<Func<tChuyenHang, bool>> filter)
        {
            var context = new PhuDinhEntities();
            return GetData(context, filter);
        }

        public static List<tChuyenHang> GetData(PhuDinhEntities context, Expression<Func<tChuyenHang, bool>> filter)
        {
            return context.tChuyenHangs.Where(filter).ToList();
        }

        public static void Save(List<tChuyenHang> data, Expression<Func<tChuyenHang, bool>> filter)
        {
            var context = new PhuDinhEntities();
            Save(context, data, filter);
        }

        public static void Save(PhuDinhEntities context, List<tChuyenHang> data, Expression<Func<tChuyenHang, bool>> filter)
        {
            RemoveOrUpdateItem(context, data, filter);
            AddNewItem(context, data);
            context.SaveChanges();
        }
    }
}
