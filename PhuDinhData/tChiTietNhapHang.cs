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
    
    public partial class tChiTietNhapHang : BindableObject
    {
        private int _ma;
        public int Ma { get { return _ma; } set { if(_ma == value) return; _ma = value; base.RaisePropertyChanged("Ma");} }
    
        private int _manhaphang;
        public int MaNhapHang { get { return _manhaphang; } set { if(_manhaphang == value) return; _manhaphang = value; base.RaisePropertyChanged("MaNhapHang");} }
    
        private int _mamathang;
        public int MaMatHang { get { return _mamathang; } set { if(_mamathang == value) return; _mamathang = value; base.RaisePropertyChanged("MaMatHang");} }
    
        private int _soluong;
        public int SoLuong { get { return _soluong; } set { if(_soluong == value) return; _soluong = value; base.RaisePropertyChanged("SoLuong");} }
    
    
        public virtual tNhapHang tNhapHang { get; set; }
        public virtual tMatHang tMatHang { get; set; }
    }
}
