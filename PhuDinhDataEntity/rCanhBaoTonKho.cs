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
    
    public partial class rCanhBaoTonKho : BindableObject
    {
        private int _ma;
        public int Ma { get { return _ma; } set { SetPropertyAndValidate(ref _ma, value); } }
    
        private int _mamathang;
        public int MaMatHang { get { return _mamathang; } set { SetPropertyAndValidate(ref _mamathang, value); } }
    
        private int _makhohang;
        public int MaKhoHang { get { return _makhohang; } set { SetPropertyAndValidate(ref _makhohang, value); } }
    
        private int _toncaonhat;
        public int TonCaoNhat { get { return _toncaonhat; } set { SetPropertyAndValidate(ref _toncaonhat, value); } }
    
        private int _tonthapnhat;
        public int TonThapNhat { get { return _tonthapnhat; } set { SetPropertyAndValidate(ref _tonthapnhat, value); } }
    
    
        public virtual rKhoHang rKhoHang { get; set; }
        public virtual tMatHang tMatHang { get; set; }
    }
}
