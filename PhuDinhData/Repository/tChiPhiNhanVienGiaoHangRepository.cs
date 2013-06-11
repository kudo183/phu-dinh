using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhData.Repository
{
    public static class tChiPhiNhanVienGiaoHangRepository
    {
        private static void RemoveOrUpdateItem(PhuDinhEntities context, List<tChiPhiNhanVienGiaoHang> gridDataSource, Expression<Func<tChiPhiNhanVienGiaoHang, bool>> filter)
        {
            foreach (var item in GetData(context, filter))
            {
                var entity = gridDataSource.FirstOrDefault(p => p.Ma == item.Ma);
                //remove deleted item
                if (entity == null)
                {
                    context.tChiPhiNhanVienGiaoHangs.Remove(item);
                }
                //update exist item
                else
                {
                    item.MaLoaiChiPhi = entity.MaLoaiChiPhi;
                    item.MaNhanVienGiaoHang = entity.MaNhanVienGiaoHang;
                    item.Ngay = entity.Ngay;
                    item.SoTien = entity.SoTien;
                    item.GhiChu = entity.GhiChu;
                }
            }
        }

        private static void AddNewItem(PhuDinhEntities context, List<tChiPhiNhanVienGiaoHang> gridDataSource)
        {
            foreach (var item in gridDataSource)
            {
                if (item.Ma == 0)
                {
                    context.tChiPhiNhanVienGiaoHangs.Add(item);
                }
            }
        }

        public static List<tChiPhiNhanVienGiaoHang> GetData(Expression<Func<tChiPhiNhanVienGiaoHang, bool>> filter)
        {
            var context = new PhuDinhEntities();
            return GetData(context, filter);
        }

        public static List<tChiPhiNhanVienGiaoHang> GetData(PhuDinhEntities context, Expression<Func<tChiPhiNhanVienGiaoHang, bool>> filter)
        {
            return context.tChiPhiNhanVienGiaoHangs.Where(filter).ToList();
        }

        public static void Save(List<tChiPhiNhanVienGiaoHang> data, Expression<Func<tChiPhiNhanVienGiaoHang, bool>> filter)
        {
            var context = new PhuDinhEntities();
            Save(context, data, filter);
        }

        public static void Save(PhuDinhEntities context, List<tChiPhiNhanVienGiaoHang> data, Expression<Func<tChiPhiNhanVienGiaoHang, bool>> filter)
        {
            RemoveOrUpdateItem(context, data, filter);
            AddNewItem(context, data);
            context.SaveChanges();
        }
    }
}
