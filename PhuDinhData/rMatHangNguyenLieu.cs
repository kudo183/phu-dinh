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
    
    public partial class rMatHangNguyenLieu : BindableObject
    {
        private int _ma;
        public int Ma { get { return _ma; } set { if(IsValid(value, "Ma") == false || _ma == value) return; _ma = value; base.RaisePropertyChanged("Ma");} }
    
        private int _mamathang;
        public int MaMatHang { get { return _mamathang; } set { if(IsValid(value, "MaMatHang") == false || _mamathang == value) return; _mamathang = value; base.RaisePropertyChanged("MaMatHang");} }
    
        private int _manguyenlieu;
        public int MaNguyenLieu { get { return _manguyenlieu; } set { if(IsValid(value, "MaNguyenLieu") == false || _manguyenlieu == value) return; _manguyenlieu = value; base.RaisePropertyChanged("MaNguyenLieu");} }
    
    
        public virtual rNguyenLieu rNguyenLieu { get; set; }
        public virtual tMatHang tMatHang { get; set; }
    }
}
