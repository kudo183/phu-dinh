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
    
    public partial class rLoaiHang : BindableObject
    {
        public rLoaiHang()
        {
            this.tMatHangs = new HashSet<tMatHang>();
            Init();
        }
    
        private int _ma;
        public int Ma { get { return _ma; } set { SetPropertyAndValidate(ref _ma, value); } }
    
        private string _tenloai;
        public string TenLoai { get { return _tenloai; } set { SetPropertyAndValidate(ref _tenloai, value); } }
    
    
        public virtual ICollection<tMatHang> tMatHangs { get; set; }
    }
}
