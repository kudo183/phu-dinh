﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhData.Repository
{
    public static class tDonHangRepository
    {
        private static void RemoveOrUpdateItem(PhuDinhEntities context, List<tDonHang> gridDataSource, Expression<Func<tDonHang, bool>> filter)
        {
            foreach (var item in GetData(context, filter))
            {
                var entity = gridDataSource.FirstOrDefault(p => p.Ma == item.Ma);
                //remove deleted item
                if (entity == null)
                {
                    context.tDonHangs.Remove(item);
                }
                //update exist item
                else
                {
                    item.MaKhachHang = entity.MaKhachHang;
                    item.MaChanh = entity.MaChanh;
                    item.Ngay = entity.Ngay;
                }
            }
        }

        private static void AddNewItem(PhuDinhEntities context, List<tDonHang> gridDataSource)
        {
            foreach (var item in gridDataSource)
            {
                if (item.Ma == 0)
                {
                    context.tDonHangs.Add(item);
                }
            }
        }

        public static List<tDonHang> GetData(Expression<Func<tDonHang, bool>> filter)
        {
            var context = new PhuDinhEntities();
            return GetData(context, filter);
        }

        public static List<tDonHang> GetData(PhuDinhEntities context, Expression<Func<tDonHang, bool>> filter)
        {
            return context.tDonHangs.Where(filter).ToList();
        }

        public static void Save(List<tDonHang> data, Expression<Func<tDonHang, bool>> filter)
        {
            var context = new PhuDinhEntities();
            Save(context, data, filter);
        }

        public static void Save(PhuDinhEntities context, List<tDonHang> data, Expression<Func<tDonHang, bool>> filter)
        {
            RemoveOrUpdateItem(context, data, filter);
            AddNewItem(context, data);
            context.SaveChanges();
        }
    }
}
