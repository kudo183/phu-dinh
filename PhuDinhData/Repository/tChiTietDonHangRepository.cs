using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhData.Repository
{
    public static class tChiTietDonHangRepository
    {
        private static void RemoveOrUpdateItem(PhuDinhEntities context, List<tChiTietDonHang> gridDataSource, Expression<Func<tChiTietDonHang, bool>> filter)
        {
            foreach (var item in GetData(context, filter))
            {
                var entity = gridDataSource.FirstOrDefault(p => p.Ma == item.Ma);
                //remove deleted item
                if (entity == null)
                {
                    context.tChiTietDonHangs.Remove(item);
                }
                //update exist item
                else
                {
                    item.MaDonHang = entity.MaDonHang;
                    item.MaMatHang = entity.MaMatHang;
                    item.SoLuong = entity.SoLuong;
                }
            }
        }

        private static void AddNewItem(PhuDinhEntities context, List<tChiTietDonHang> gridDataSource)
        {
            foreach (var item in gridDataSource)
            {
                if (item.Ma == 0)
                {
                    context.tChiTietDonHangs.Add(item);
                }
            }
        }

        public static List<tChiTietDonHang> GetData(Expression<Func<tChiTietDonHang, bool>> filter)
        {
            var context = new PhuDinhEntities();
            return GetData(context, filter);
        }

        public static List<tChiTietDonHang> GetData(PhuDinhEntities context, Expression<Func<tChiTietDonHang, bool>> filter)
        {
            return context.tChiTietDonHangs.Where(filter).ToList();
        }

        public static void Save(List<tChiTietDonHang> data, Expression<Func<tChiTietDonHang, bool>> filter)
        {
            var context = new PhuDinhEntities();
            Save(context, data, filter);
        }

        public static void Save(PhuDinhEntities context, List<tChiTietDonHang> data, Expression<Func<tChiTietDonHang, bool>> filter)
        {
            RemoveOrUpdateItem(context, data, filter);
            AddNewItem(context, data);
            context.SaveChanges();
        }
    }
}
