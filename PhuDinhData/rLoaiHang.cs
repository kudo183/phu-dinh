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
    
    public partial class rLoaiHang : BindableObject
    {
        public rLoaiHang()
        {
            this.tMatHangs = new HashSet<tMatHang>();
            Init();
        }
    
        private int _ma;
        public int Ma { get { return _ma; } set { if(IsValid(value, "Ma") == false || _ma == value) return; _ma = value; base.RaisePropertyChanged("Ma");} }
    
        private string _tenloai;
        public string TenLoai { get { return _tenloai; } set { if(IsValid(value, "TenLoai") == false || _tenloai == value) return; _tenloai = value; base.RaisePropertyChanged("TenLoai");} }
    
    
        public virtual ICollection<tMatHang> tMatHangs { get; set; }
    }
}
