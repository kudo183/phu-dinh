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
    
    public partial class rKhoHang : BindableObject
    {
        public rKhoHang()
        {
            this.tDonHangs = new HashSet<tDonHang>();
            this.tTonKhoes = new HashSet<tTonKho>();
            this.tNhapHangs = new HashSet<tNhapHang>();
            this.tChuyenKhoes = new HashSet<tChuyenKho>();
            this.tChuyenKhoes1 = new HashSet<tChuyenKho>();
        }
    
        private int _ma;
        public int Ma { get { return _ma; } set { if(_ma == value) return; _ma = value; base.RaisePropertyChanged("Ma");} }
    
        private string _tenkho;
        public string TenKho { get { return _tenkho; } set { if(_tenkho == value) return; _tenkho = value; base.RaisePropertyChanged("TenKho");} }
    
        private bool _trangthai;
        public bool TrangThai { get { return _trangthai; } set { if(_trangthai == value) return; _trangthai = value; base.RaisePropertyChanged("TrangThai");} }
    
    
        public virtual ICollection<tDonHang> tDonHangs { get; set; }
        public virtual ICollection<tTonKho> tTonKhoes { get; set; }
        public virtual ICollection<tNhapHang> tNhapHangs { get; set; }
        public virtual ICollection<tChuyenKho> tChuyenKhoes { get; set; }
        public virtual ICollection<tChuyenKho> tChuyenKhoes1 { get; set; }
    }
}
