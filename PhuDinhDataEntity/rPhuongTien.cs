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
    
    public partial class rPhuongTien : BindableObject
    {
        public rPhuongTien()
        {
            this.rNhanViens = new HashSet<rNhanVien>();
            Init();
        }
    
        private int _ma;
        public int Ma { get { return _ma; } set { SetPropertyAndValidate(ref _ma, value); } }
    
        private string _tenphuongtien;
        public string TenPhuongTien { get { return _tenphuongtien; } set { SetPropertyAndValidate(ref _tenphuongtien, value); } }
    
    
        public virtual ICollection<rNhanVien> rNhanViens { get; set; }
    }
}
