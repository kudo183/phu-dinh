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
    
    public partial class tChiTietDonHang : BindableObject
    {
        public tChiTietDonHang()
        {
            this.tChiTietChuyenHangDonHangs = new HashSet<tChiTietChuyenHangDonHang>();
            Init();
        }
    
        private int _ma;
        public int Ma { get { return _ma; } set { SetPropertyAndValidate(ref _ma, value); } }
    
        private int _madonhang;
        public int MaDonHang { get { return _madonhang; } set { SetPropertyAndValidate(ref _madonhang, value); } }
    
        private int _mamathang;
        public int MaMatHang { get { return _mamathang; } set { SetPropertyAndValidate(ref _mamathang, value); } }
    
        private int _soluong;
        public int SoLuong { get { return _soluong; } set { SetPropertyAndValidate(ref _soluong, value); } }
    
        private bool _xong;
        public bool Xong { get { return _xong; } set { SetPropertyAndValidate(ref _xong, value); } }
    
    
        public virtual ICollection<tChiTietChuyenHangDonHang> tChiTietChuyenHangDonHangs { get; set; }
        public virtual tDonHang tDonHang { get; set; }
        public virtual tMatHang tMatHang { get; set; }
    }
}
