﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PhuDinhData
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class PhuDinhEntities : DbContext
    {
        public PhuDinhEntities()
            : base("name=PhuDinhEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<rBaiXe> rBaiXes { get; set; }
        public DbSet<rChanh> rChanhs { get; set; }
        public DbSet<rDiaDiem> rDiaDiems { get; set; }
        public DbSet<rKhachHang> rKhachHangs { get; set; }
        public DbSet<rLoaiChiPhi> rLoaiChiPhis { get; set; }
        public DbSet<rLoaiHang> rLoaiHangs { get; set; }
        public DbSet<rNhanVienGiaoHang> rNhanVienGiaoHangs { get; set; }
        public DbSet<rNuoc> rNuocs { get; set; }
        public DbSet<rPhuongTien> rPhuongTiens { get; set; }
        public DbSet<tChiPhiNhanVienGiaoHang> tChiPhiNhanVienGiaoHangs { get; set; }
        public DbSet<tChiTietChuyenHangDonHang> tChiTietChuyenHangDonHangs { get; set; }
        public DbSet<tChiTietDonHang> tChiTietDonHangs { get; set; }
        public DbSet<tChuyenHang> tChuyenHangs { get; set; }
        public DbSet<tChuyenHangDonHang> tChuyenHangDonHangs { get; set; }
        public DbSet<tDonHang> tDonHangs { get; set; }
        public DbSet<tMatHang> tMatHangs { get; set; }
    }
}
