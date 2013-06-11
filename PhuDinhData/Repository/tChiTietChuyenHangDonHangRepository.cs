using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhData.Repository
{
    public static class tChiTietChuyenHangDonHangRepository
    {
        private static void RemoveOrUpdateItem(PhuDinhEntities context, List<tChiTietChuyenHangDonHang> gridDataSource, Expression<Func<tChiTietChuyenHangDonHang, bool>> filter)
        {
            foreach (var item in GetData(context, filter))
            {
                var entity = gridDataSource.FirstOrDefault(p => p.Ma == item.Ma);
                //remove deleted item
                if (entity == null)
                {
                    context.tChiTietChuyenHangDonHangs.Remove(item);
                }
                //update exist item
                else
                {
                    item.MaChuyenHangDonHang = entity.MaChuyenHangDonHang;
                    item.MaMatHang = entity.MaMatHang;
                    item.SoLuong = entity.SoLuong;
                }
            }
        }

        private static void AddNewItem(PhuDinhEntities context, List<tChiTietChuyenHangDonHang> gridDataSource)
        {
            foreach (var item in gridDataSource)
            {
                if (item.Ma == 0)
                {
                    context.tChiTietChuyenHangDonHangs.Add(item);
                }
            }
        }

        public static List<tChiTietChuyenHangDonHang> GetData(Expression<Func<tChiTietChuyenHangDonHang, bool>> filter)
        {
            var context = new PhuDinhEntities();
            return GetData(context, filter);
        }

        public static List<tChiTietChuyenHangDonHang> GetData(PhuDinhEntities context, Expression<Func<tChiTietChuyenHangDonHang, bool>> filter)
        {
            return context.tChiTietChuyenHangDonHangs.Where(filter).ToList();
        }

        public static void Save(List<tChiTietChuyenHangDonHang> data, Expression<Func<tChiTietChuyenHangDonHang, bool>> filter)
        {
            var context = new PhuDinhEntities();
            Save(context, data, filter);
        }

        public static void Save(PhuDinhEntities context, List<tChiTietChuyenHangDonHang> data, Expression<Func<tChiTietChuyenHangDonHang, bool>> filter)
        {
            RemoveOrUpdateItem(context, data, filter);
            AddNewItem(context, data);
            context.SaveChanges();
        }
    }
}
