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
    
    public partial class tNhapNguyenLieu : BindableObject
    {
        private int _ma;
        public int Ma { get { return _ma; } set { SetPropertyAndValidate(ref _ma, value); } }
    
        private System.DateTime _ngay;
        public System.DateTime Ngay { get { return _ngay; } set { SetPropertyAndValidate(ref _ngay, value); } }
    
        private int _manguyenlieu;
        public int MaNguyenLieu { get { return _manguyenlieu; } set { SetPropertyAndValidate(ref _manguyenlieu, value); } }
    
        private int _manhacungcap;
        public int MaNhaCungCap { get { return _manhacungcap; } set { SetPropertyAndValidate(ref _manhacungcap, value); } }
    
        private int _soluong;
        public int SoLuong { get { return _soluong; } set { SetPropertyAndValidate(ref _soluong, value); } }
    
    
        public virtual rNguyenLieu rNguyenLieu { get; set; }
        public virtual rNhaCungCap rNhaCungCap { get; set; }
    }
}
