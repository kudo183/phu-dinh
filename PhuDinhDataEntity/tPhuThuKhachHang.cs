//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PhuDinhDataEntity
{
    using System;
    using System.Collections.Generic;
    using Common;
    
    public partial class tPhuThuKhachHang : BindableObject
    {
        private int _ma;
        public int Ma { get { return _ma; } set { SetPropertyAndValidate(ref _ma, value); } }
    
        private int _makhachhang;
        public int MaKhachHang { get { return _makhachhang; } set { SetPropertyAndValidate(ref _makhachhang, value); } }
    
        private System.DateTime _ngay;
        public System.DateTime Ngay { get { return _ngay; } set { SetPropertyAndValidate(ref _ngay, value); } }
    
        private int _sotien;
        public int SoTien { get { return _sotien; } set { SetPropertyAndValidate(ref _sotien, value); } }
    
        private string _ghichu;
        public string GhiChu { get { return _ghichu; } set { SetPropertyAndValidate(ref _ghichu, value); } }
    
    
        public virtual rKhachHang rKhachHang { get; set; }
    }
}