using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhData.Repository
{
    public static class tChuyenHangDonHangRepository
    {
        private static void RemoveOrUpdateItem(PhuDinhEntities context, List<tChuyenHangDonHang> gridDataSource, Expression<Func<tChuyenHangDonHang, bool>> filter)
        {
            foreach (var item in GetData(context, filter))
            {
                var entity = gridDataSource.FirstOrDefault(p => p.Ma == item.Ma);
                //remove deleted item
                if (entity == null)
                {
                    context.tChuyenHangDonHangs.Remove(item);
                }
                //update exist item
                else
                {
                    item.MaChuyenHang = entity.MaChuyenHang;
                    item.MaDonHang = entity.MaDonHang;
                }
            }
        }

        private static void AddNewItem(PhuDinhEntities context, List<tChuyenHangDonHang> gridDataSource)
        {
            foreach (var item in gridDataSource)
            {
                if (item.Ma == 0)
                {
                    context.tChuyenHangDonHangs.Add(item);
                }
            }
        }

        public static List<tChuyenHangDonHang> GetData(Expression<Func<tChuyenHangDonHang, bool>> filter)
        {
            var context = new PhuDinhEntities();
            return GetData(context, filter);
        }

        public static List<tChuyenHangDonHang> GetData(PhuDinhEntities context, Expression<Func<tChuyenHangDonHang, bool>> filter)
        {
            return context.tChuyenHangDonHangs.Where(filter).ToList();
        }

        public static void Save(List<tChuyenHangDonHang> data, Expression<Func<tChuyenHangDonHang, bool>> filter)
        {
            var context = new PhuDinhEntities();
            Save(context, data, filter);
        }

        public static void Save(PhuDinhEntities context, List<tChuyenHangDonHang> data, Expression<Func<tChuyenHangDonHang, bool>> filter)
        {
            RemoveOrUpdateItem(context, data, filter);
            AddNewItem(context, data);
            context.SaveChanges();
        }
    }
}
