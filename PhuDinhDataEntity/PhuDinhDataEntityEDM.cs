﻿using System.Web.Http.OData.Builder;
using Microsoft.Data.Edm;

namespace PhuDinhDataEntity
{
    public static class PhuDinhDataEntityEDM
    {
        private static IEdmModel _edmModel = null;
        public static IEdmModel GetPhuDinhDataEntityEDM()
        {
            if (_edmModel != null)
            {
                return _edmModel;
            }

            var builder = new ODataConventionModelBuilder { ContainerName = "PhuDinh_testContext" };
            builder.EntitySet<rBaiXe>("rBaiXes").EntityType.HasKey(p => p.Ma);
            builder.EntitySet<rCanhBaoTonKho>("rCanhBaoTonKhos").EntityType.HasKey(p => p.Ma);
            builder.EntitySet<rChanh>("rChanhs").EntityType.HasKey(p => p.Ma);
            builder.EntitySet<rDiaDiem>("rDiaDiems").EntityType.HasKey(p => p.Ma);
            builder.EntitySet<rKhachHangChanh>("rKhachHangChanhs").EntityType.HasKey(p => p.Ma);
            builder.EntitySet<rKhachHang>("rKhachHangs").EntityType.HasKey(p => p.Ma);
            builder.EntitySet<rKhoHang>("rKhoHangs").EntityType.HasKey(p => p.Ma);
            builder.EntitySet<rLoaiChiPhi>("rLoaiChiPhis").EntityType.HasKey(p => p.Ma);
            builder.EntitySet<rLoaiHang>("rLoaiHangs").EntityType.HasKey(p => p.Ma);
            builder.EntitySet<rLoaiNguyenLieu>("rLoaiNguyenLieus").EntityType.HasKey(p => p.Ma);
            builder.EntitySet<rMatHangNguyenLieu>("rMatHangNguyenLieus").EntityType.HasKey(p => p.Ma);
            builder.EntitySet<rNuoc>("rNuocs").EntityType.HasKey(p => p.Ma);
            builder.EntitySet<rNguyenLieu>("rNguyenLieus").EntityType.HasKey(p => p.Ma);
            builder.EntitySet<rNhaCungCap>("rNhaCungCaps").EntityType.HasKey(p => p.Ma);
            builder.EntitySet<rNhanVien>("rNhanViens").EntityType.HasKey(p => p.Ma);
            builder.EntitySet<rPhuongTien>("rPhuongTiens").EntityType.HasKey(p => p.Ma);
            builder.EntitySet<tCongNoKhachHang>("tCongNoKhachHangs").EntityType.HasKey(p => p.Ma);
            builder.EntitySet<tChiPhi>("tChiPhis").EntityType.HasKey(p => p.Ma);
            builder.EntitySet<tChiTietChuyenHangDonHang>("tChiTietChuyenHangDonHangs").EntityType.HasKey(p => p.Ma);
            builder.EntitySet<tChiTietChuyenKho>("tChiTietChuyenKhos").EntityType.HasKey(p => p.Ma);
            builder.EntitySet<tChiTietDonHang>("tChiTietDonHangs").EntityType.HasKey(p => p.Ma);
            builder.EntitySet<tChiTietNhapHang>("tChiTietNhapHangs").EntityType.HasKey(p => p.Ma);
            builder.EntitySet<tChiTietToaHang>("tChiTietToaHangs").EntityType.HasKey(p => p.Ma);
            builder.EntitySet<tChuyenHang>("tChuyenHangs").EntityType.HasKey(p => p.Ma);
            builder.EntitySet<tChuyenHangDonHang>("tChuyenHangDonHangs").EntityType.HasKey(p => p.Ma);
            builder.EntitySet<tChuyenKho>("tChuyenKhos").EntityType.HasKey(p => p.Ma);
            builder.EntitySet<tDonHang>("tDonHangs").EntityType.HasKey(p => p.Ma);
            builder.EntitySet<tGiamTruKhachHang>("tGiamTruKhachHangs").EntityType.HasKey(p => p.Ma);
            builder.EntitySet<tMatHang>("tMatHangs").EntityType.HasKey(p => p.Ma);
            builder.EntitySet<tNhanTienKhachHang>("tNhanTienKhachHangs").EntityType.HasKey(p => p.Ma);
            builder.EntitySet<tNhapHang>("tNhapHangs").EntityType.HasKey(p => p.Ma);
            builder.EntitySet<tNhapNguyenLieu>("tNhapNguyenLieus").EntityType.HasKey(p => p.Ma);
            builder.EntitySet<tPhuThuKhachHang>("tPhuThuKhachHangs").EntityType.HasKey(p => p.Ma);
            builder.EntitySet<tToaHang>("tToaHangs").EntityType.HasKey(p => p.Ma);
            builder.EntitySet<tTonKho>("tTonKhos").EntityType.HasKey(p => p.Ma);
            
            _edmModel = builder.GetEdmModel();
            return _edmModel;
        }
    }
}
