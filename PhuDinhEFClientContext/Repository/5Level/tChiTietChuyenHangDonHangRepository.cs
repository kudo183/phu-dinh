﻿using System.Collections.Generic;
using PhuDinhDataEntity;
using System.Data.Entity;
using System.Linq;

namespace PhuDinhEFClientContext.Repository
{
    public static class tChiTietChuyenHangDonHangRepository
    {
        public static IQueryable<tChiTietChuyenHangDonHang> GetDataQuery(IQueryable<tChiTietChuyenHangDonHang> query)
        {
            return query
                .Include("tChuyenHangDonHang.tChuyenHang.rNhanVien")
                .Include("tChuyenHangDonHang.tDonHang.rKhachHang")
                .Include("tChuyenHangDonHang.tDonHang.rKhoHang")
                .Include("tChiTietDonHang.tMatHang")
                .Include("tChiTietDonHang.tDonHang.rKhachHang")
                .Include("tChiTietDonHang.tDonHang.rKhoHang")
                .OrderByDescending(p => p.tChuyenHangDonHang.tChuyenHang.Ngay)
                .ThenBy(p => p.tChuyenHangDonHang.tChuyenHang.Gio);
        }

        public static IQueryable<tChiTietChuyenHangDonHang> GetDataQueryAndRelatedTables(IQueryable<tChiTietChuyenHangDonHang> query, ref List<string> relatedTables)
        {
            relatedTables.Clear();
            relatedTables.Add("tChiTietChuyenHangDonHang");
            relatedTables.Add("tChuyenHangDonHang");
            relatedTables.Add("tChuyenHang");
            relatedTables.Add("tDonHang");
            relatedTables.Add("rKhachHang");
            relatedTables.Add("tChiTietDonHang");
            relatedTables.Add("tMatHang");
            relatedTables.Add("rNhanVien");
            return query
                .Include("tChuyenHangDonHang.tChuyenHang.rNhanVien")
                .Include("tChuyenHangDonHang.tDonHang.rKhachHang")
                .Include("tChiTietDonHang.tMatHang")
                .OrderByDescending(p => p.tChuyenHangDonHang.tChuyenHang.Ngay)
                .ThenBy(p => p.tChuyenHangDonHang.tChuyenHang.Gio);
        }
    }
}
