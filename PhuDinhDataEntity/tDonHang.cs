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
    
    public partial class tDonHang : BindableObject
    {
        public tDonHang()
        {
            this.tChiTietDonHangs = new HashSet<tChiTietDonHang>();
            this.tChuyenHangDonHangs = new HashSet<tChuyenHangDonHang>();
            Init();
        }
    
        private int _ma;
        public int Ma { get { return _ma; } set { SetPropertyAndValidate(ref _ma, value); } }
    
        private int _makhachhang;
        public int MaKhachHang { get { return _makhachhang; } set { SetPropertyAndValidate(ref _makhachhang, value); } }
    
        private Nullable<int> _machanh;
        public Nullable<int> MaChanh { get { return _machanh; } set { SetPropertyAndValidate(ref _machanh, value); } }
    
        private System.DateTime _ngay;
        public System.DateTime Ngay { get { return _ngay; } set { SetPropertyAndValidate(ref _ngay, value); } }
    
        private bool _xong;
        public bool Xong { get { return _xong; } set { SetPropertyAndValidate(ref _xong, value); } }
    
        private int _makhohang;
        public int MaKhoHang { get { return _makhohang; } set { SetPropertyAndValidate(ref _makhohang, value); } }
    
    
        public virtual rChanh rChanh { get; set; }
        public virtual rKhachHang rKhachHang { get; set; }
        public virtual ICollection<tChiTietDonHang> tChiTietDonHangs { get; set; }
        public virtual ICollection<tChuyenHangDonHang> tChuyenHangDonHangs { get; set; }
        public virtual rKhoHang rKhoHang { get; set; }
    }
}
