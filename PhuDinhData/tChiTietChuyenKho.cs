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
    
    public partial class tChiTietChuyenKho : BindableObject
    {
        private int _ma;
        public int Ma { get { return _ma; } set { if(IsValid(value, "Ma") == false || _ma == value) return; _ma = value; base.RaisePropertyChanged("Ma");} }
    
        private int _machuyenkho;
        public int MaChuyenKho { get { return _machuyenkho; } set { if(IsValid(value, "MaChuyenKho") == false || _machuyenkho == value) return; _machuyenkho = value; base.RaisePropertyChanged("MaChuyenKho");} }
    
        private int _mamathang;
        public int MaMatHang { get { return _mamathang; } set { if(IsValid(value, "MaMatHang") == false || _mamathang == value) return; _mamathang = value; base.RaisePropertyChanged("MaMatHang");} }
    
        private int _soluong;
        public int SoLuong { get { return _soluong; } set { if(IsValid(value, "SoLuong") == false || _soluong == value) return; _soluong = value; base.RaisePropertyChanged("SoLuong");} }
    
    
        public virtual tChuyenKho tChuyenKho { get; set; }
        public virtual tMatHang tMatHang { get; set; }
    }
}
