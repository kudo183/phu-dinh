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
            this.tChiTietDonHangs = new HashSet<tChiTietDonHang>();
            this.rMatHangNguyenLieux = new HashSet<rMatHangNguyenLieu>();
            this.tNhapMatHangs = new HashSet<tNhapMatHang>();
        }
    
        private int _ma;
        public int Ma { get { return _ma; } set { if(_ma == value) return; _ma = value; base.RaisePropertyChanged("Ma");} }
    
        private int _maloai;
        public int MaLoai { get { return _maloai; } set { if(_maloai == value) return; _maloai = value; base.RaisePropertyChanged("MaLoai");} }
    
        private string _tenmathang;
        public string TenMatHang { get { return _tenmathang; } set { if(_tenmathang == value) return; _tenmathang = value; base.RaisePropertyChanged("TenMatHang");} }
    
        private int _soky;
        public int SoKy { get { return _soky; } set { if(_soky == value) return; _soky = value; base.RaisePropertyChanged("SoKy");} }
    
        private int _somet;
        public int SoMet { get { return _somet; } set { if(_somet == value) return; _somet = value; base.RaisePropertyChanged("SoMet");} }
    
        private string _tenmathangdaydu;
        public string TenMatHangDayDu { get { return _tenmathangdaydu; } set { if(_tenmathangdaydu == value) return; _tenmathangdaydu = value; base.RaisePropertyChanged("TenMatHangDayDu");} }
    
    
        public virtual rLoaiHang rLoaiHang { get; set; }
        public virtual ICollection<tChiTietDonHang> tChiTietDonHangs { get; set; }
        public virtual ICollection<rMatHangNguyenLieu> rMatHangNguyenLieux { get; set; }
        public virtual ICollection<tNhapMatHang> tNhapMatHangs { get; set; }
    }
}
