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
    
    public partial class rMatHangNguyenLieu : BindableObject
    {
        private int _ma;
        public int Ma { get { return _ma; } set { SetPropertyAndValidate(ref _ma, value); } }
    
        private int _mamathang;
        public int MaMatHang { get { return _mamathang; } set { SetPropertyAndValidate(ref _mamathang, value); } }
    
        private int _manguyenlieu;
        public int MaNguyenLieu { get { return _manguyenlieu; } set { SetPropertyAndValidate(ref _manguyenlieu, value); } }
    
    
        public virtual rNguyenLieu rNguyenLieu { get; set; }
        public virtual tMatHang tMatHang { get; set; }
    }
}
