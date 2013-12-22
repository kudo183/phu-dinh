﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhData.Repository
{
    public static class tChiTietChuyenHangDonHangRepository
    {
        public static int GetDataCount(PhuDinhEntities context, Expression<Func<tChiTietChuyenHangDonHang, bool>> filter)
        {
            var data = Repository<tChiTietChuyenHangDonHang>.GetData(context, filter).
                OrderByDescending(p => p.tChuyenHangDonHang.tChuyenHang.Ngay).
                ThenBy(p => p.tChuyenHangDonHang.tChuyenHang.Gio);
            return data.Count();
        }

        public static List<tChiTietChuyenHangDonHang> GetData(PhuDinhEntities context,
            Expression<Func<tChiTietChuyenHangDonHang, bool>> filter,
            int pageSize, int currentPageIndex, int itemCount)
        {
            var data = Repository<tChiTietChuyenHangDonHang>.GetData(context, filter).
                OrderByDescending(p => p.tChuyenHangDonHang.tChuyenHang.Ngay).
                ThenBy(p => p.tChuyenHangDonHang.tChuyenHang.Gio);

            var skippedItem = pageSize * (currentPageIndex - 1);

            var takeItem = itemCount - skippedItem;
            if (takeItem > pageSize)
            {
                takeItem = pageSize;
            }

            if (takeItem <= 0)
            {
                return new List<tChiTietChuyenHangDonHang>();
            }

            return data.Skip(skippedItem).Take(takeItem).ToList();
        }

        public static List<tChiTietChuyenHangDonHang> GetData(PhuDinhEntities context, Expression<Func<tChiTietChuyenHangDonHang, bool>> filter)
        {
            return Repository<tChiTietChuyenHangDonHang>.GetData(context, filter).
                OrderByDescending(p => p.tChuyenHangDonHang.tChuyenHang.Ngay).
                ThenBy(p => p.tChuyenHangDonHang.tChuyenHang.Gio).ToList();
        }

        public static List<Repository<tChiTietChuyenHangDonHang>.ChangedItemData> Save(PhuDinhEntities context, List<tChiTietChuyenHangDonHang> data, List<tChiTietChuyenHangDonHang> origData)
        {
            var changed = Repository<tChiTietChuyenHangDonHang>.Save(context, data, origData, (p => p.Ma == 0), ((p1, p2) => p1.Ma == p2.Ma));

            if (changed.Count > 0)
            {
                var maChuyenHangDonHangs = changed.Select(p => p.CurrentValues.MaChuyenHangDonHang).ToList();

                var maDonHangs = maChuyenHangDonHangs.Select(
                    ma => Repository<tChuyenHangDonHang>.GetData(context, (p => p.Ma == ma)).First())
                    .Select(chuyenHangDonHang => chuyenHangDonHang.MaDonHang).ToList();

                BusinessLogics.BusinessLogics.UpdateXong(context, maDonHangs);
            }
            return changed;
        }
    }
}
