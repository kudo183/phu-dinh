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
    
    public partial class tChiTietToaHang : BindableObject
    {
        private int _ma;
        public int Ma { get { return _ma; } set { SetPropertyAndValidate(ref _ma, value); } }
    
        private int _matoahang;
        public int MaToaHang { get { return _matoahang; } set { SetPropertyAndValidate(ref _matoahang, value); } }
    
        private int _machitietdonhang;
        public int MaChiTietDonHang { get { return _machitietdonhang; } set { SetPropertyAndValidate(ref _machitietdonhang, value); } }
    
        private int _giatien;
        public int GiaTien { get { return _giatien; } set { SetPropertyAndValidate(ref _giatien, value); } }
    
    
        public virtual tChiTietDonHang tChiTietDonHang { get; set; }
        public virtual tToaHang tToaHang { get; set; }
    }
}
