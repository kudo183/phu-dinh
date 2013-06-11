using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhData.Repository
{
    public static class rNhanVienGiaoHangRepository
    {

        private static void RemoveOrUpdateItem(PhuDinhEntities context, List<rNhanVienGiaoHang> gridDataSource, Expression<Func<rNhanVienGiaoHang, bool>> filter)
        {
            foreach (var item in GetData(context, filter))
            {
                var entity = gridDataSource.FirstOrDefault(p => p.Ma == item.Ma);
                //remove deleted item
                if (entity == null)
                {
                    context.rNhanVienGiaoHangs.Remove(item);
                }
                //update exist item
                else
                {
                    item.MaPhuongTien = entity.MaPhuongTien;
                    item.TenNhanVien = entity.TenNhanVien;
                }
            }
        }

        private static void AddNewItem(PhuDinhEntities context, List<rNhanVienGiaoHang> gridDataSource)
        {
            foreach (var item in gridDataSource)
            {
                if (item.Ma == 0)
                {
                    context.rNhanVienGiaoHangs.Add(item);
                }
            }
        }

        public static List<rNhanVienGiaoHang> GetData(Expression<Func<rNhanVienGiaoHang, bool>> filter)
        {
            var context = new PhuDinhEntities();
            return GetData(context, filter);
        }

        public static List<rNhanVienGiaoHang> GetData(PhuDinhEntities context, Expression<Func<rNhanVienGiaoHang, bool>> filter)
        {
            return context.rNhanVienGiaoHangs.Where(filter).ToList();
        }

        public static void Save(List<rNhanVienGiaoHang> data, Expression<Func<rNhanVienGiaoHang, bool>> filter)
        {
            var context = new PhuDinhEntities();
            Save(context, data, filter);
        }

        public static void Save(PhuDinhEntities context, List<rNhanVienGiaoHang> data, Expression<Func<rNhanVienGiaoHang, bool>> filter)
        {
            RemoveOrUpdateItem(context, data, filter);
            AddNewItem(context, data);
            context.SaveChanges();
        }
    }
}
