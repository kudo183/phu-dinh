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
    
    public partial class tTonKho : BindableObject
    {
        public tTonKho()
        {
            this.tTonKho1 = new HashSet<tTonKho>();
        }
    
        private int _ma;
        public int Ma { get { return _ma; } set { if(_ma == value) return; _ma = value; base.RaisePropertyChanged("Ma");} }
    
        private int _makhohang;
        public int MaKhoHang { get { return _makhohang; } set { if(_makhohang == value) return; _makhohang = value; base.RaisePropertyChanged("MaKhoHang");} }
    
        private int _mamathang;
        public int MaMatHang { get { return _mamathang; } set { if(_mamathang == value) return; _mamathang = value; base.RaisePropertyChanged("MaMatHang");} }
    
        private System.DateTime _ngay;
        public System.DateTime Ngay { get { return _ngay; } set { if(_ngay == value) return; _ngay = value; base.RaisePropertyChanged("Ngay");} }
    
        private int _soluong;
        public int SoLuong { get { return _soluong; } set { if(_soluong == value) return; _soluong = value; base.RaisePropertyChanged("SoLuong");} }
    
        private Nullable<int> _macon;
        public Nullable<int> MaCon { get { return _macon; } set { if(_macon == value) return; _macon = value; base.RaisePropertyChanged("MaCon");} }
    
    
        public virtual rKhoHang rKhoHang { get; set; }
        public virtual tMatHang tMatHang { get; set; }
        public virtual ICollection<tTonKho> tTonKho1 { get; set; }
        public virtual tTonKho tTonKho2 { get; set; }
    }
}
