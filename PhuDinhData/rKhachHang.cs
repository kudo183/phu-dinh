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
    
    public partial class rKhachHang : BindableObject
    {
        public rKhachHang()
        {
            this.tDonHangs = new HashSet<tDonHang>();
            this.rKhachHangChanhs = new HashSet<rKhachHangChanh>();
            this.tToaHangs = new HashSet<tToaHang>();
            Init();
        }
    
        private int _ma;
        public int Ma { get { return _ma; } set { SetPropertyAndValidate(ref _ma, value); } }
    
        private int _madiadiem;
        public int MaDiaDiem { get { return _madiadiem; } set { SetPropertyAndValidate(ref _madiadiem, value); } }
    
        private string _tenkhachhang;
        public string TenKhachHang { get { return _tenkhachhang; } set { SetPropertyAndValidate(ref _tenkhachhang, value); } }
    
        private bool _khachrieng;
        public bool KhachRieng { get { return _khachrieng; } set { SetPropertyAndValidate(ref _khachrieng, value); } }
    
    
        public virtual rDiaDiem rDiaDiem { get; set; }
        public virtual ICollection<tDonHang> tDonHangs { get; set; }
        public virtual ICollection<rKhachHangChanh> rKhachHangChanhs { get; set; }
        public virtual ICollection<tToaHang> tToaHangs { get; set; }
    }
}
