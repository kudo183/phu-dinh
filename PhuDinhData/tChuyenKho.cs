//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    using Common;
    
    public partial class tChuyenKho : BindableObject
    {
        public tChuyenKho()
        {
            this.tChiTietChuyenKhoes = new HashSet<tChiTietChuyenKho>();
            Init();
        }
    
        private int _ma;
        public int Ma { get { return _ma; } set { if(IsValid(value, "Ma") == false || _ma == value) return; _ma = value; base.RaisePropertyChanged("Ma");} }
    
        private int _manhanvien;
        public int MaNhanVien { get { return _manhanvien; } set { if(IsValid(value, "MaNhanVien") == false || _manhanvien == value) return; _manhanvien = value; base.RaisePropertyChanged("MaNhanVien");} }
    
        private int _makhohangxuat;
        public int MaKhoHangXuat { get { return _makhohangxuat; } set { if(IsValid(value, "MaKhoHangXuat") == false || _makhohangxuat == value) return; _makhohangxuat = value; base.RaisePropertyChanged("MaKhoHangXuat");} }
    
        private int _makhohangnhap;
        public int MaKhoHangNhap { get { return _makhohangnhap; } set { if(IsValid(value, "MaKhoHangNhap") == false || _makhohangnhap == value) return; _makhohangnhap = value; base.RaisePropertyChanged("MaKhoHangNhap");} }
    
        private System.DateTime _ngay;
        public System.DateTime Ngay { get { return _ngay; } set { if(IsValid(value, "Ngay") == false || _ngay == value) return; _ngay = value; base.RaisePropertyChanged("Ngay");} }
    
    
        public virtual rKhoHang rKhoHangNhap { get; set; }
        public virtual rKhoHang rKhoHangXuat { get; set; }
        public virtual rNhanVien rNhanVien { get; set; }
        public virtual ICollection<tChiTietChuyenKho> tChiTietChuyenKhoes { get; set; }
    }
}
