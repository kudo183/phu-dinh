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
    
    public partial class tMatHang : BindableObject
    {
        public tMatHang()
        {
            this.tChiTietChuyenHangDonHangs = new HashSet<tChiTietChuyenHangDonHang>();
            this.tChiTietDonHangs = new HashSet<tChiTietDonHang>();
        }
    
        private int _ma;
        public int Ma { get { return _ma; } set { if(_ma == value) return; _ma = value; base.RaisePropertyChanged("Ma");} }
    
        private int _maloai;
        public int MaLoai { get { return _maloai; } set { if(_maloai == value) return; _maloai = value; base.RaisePropertyChanged("MaLoai");} }
    
        private string _tenmathang;
        public string TenMatHang { get { return _tenmathang; } set { if(_tenmathang == value) return; _tenmathang = value; base.RaisePropertyChanged("TenMatHang");} }
    
    
        public virtual rLoaiHang rLoaiHang { get; set; }
        public virtual ICollection<tChiTietChuyenHangDonHang> tChiTietChuyenHangDonHangs { get; set; }
        public virtual ICollection<tChiTietDonHang> tChiTietDonHangs { get; set; }
    }
}
