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
    
    public partial class rNhanVien : BindableObject
    {
        public rNhanVien()
        {
            this.tChiPhis = new HashSet<tChiPhi>();
            this.tChuyenHangs = new HashSet<tChuyenHang>();
        }
    
        private int _ma;
        public int Ma { get { return _ma; } set { if(_ma == value) return; _ma = value; base.RaisePropertyChanged("Ma");} }
    
        private int _maphuongtien;
        public int MaPhuongTien { get { return _maphuongtien; } set { if(_maphuongtien == value) return; _maphuongtien = value; base.RaisePropertyChanged("MaPhuongTien");} }
    
        private string _tennhanvien;
        public string TenNhanVien { get { return _tennhanvien; } set { if(_tennhanvien == value) return; _tennhanvien = value; base.RaisePropertyChanged("TenNhanVien");} }
    
    
        public virtual rPhuongTien rPhuongTien { get; set; }
        public virtual ICollection<tChiPhi> tChiPhis { get; set; }
        public virtual ICollection<tChuyenHang> tChuyenHangs { get; set; }
    }
}
