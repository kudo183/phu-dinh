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
    
    public partial class rPhuongTien : BindableObject
    {
        public rPhuongTien()
        {
            this.rNhanViens = new HashSet<rNhanVien>();
        }
    
        private int _ma;
        public int Ma { get { return _ma; } set { if(_ma == value) return; _ma = value; base.RaisePropertyChanged("Ma");} }
    
        private string _tenphuongtien;
        public string TenPhuongTien { get { return _tenphuongtien; } set { if(_tenphuongtien == value) return; _tenphuongtien = value; base.RaisePropertyChanged("TenPhuongTien");} }
    
    
        public virtual ICollection<rNhanVien> rNhanViens { get; set; }
    }
}
