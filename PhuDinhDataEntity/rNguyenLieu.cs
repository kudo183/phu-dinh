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
    
    public partial class rNguyenLieu : BindableObject
    {
        public rNguyenLieu()
        {
            this.rMatHangNguyenLieux = new HashSet<rMatHangNguyenLieu>();
            this.tNhapNguyenLieux = new HashSet<tNhapNguyenLieu>();
            Init();
        }
    
        private int _ma;
        public int Ma { get { return _ma; } set { SetPropertyAndValidate(ref _ma, value); } }
    
        private int _maloainguyenlieu;
        public int MaLoaiNguyenLieu { get { return _maloainguyenlieu; } set { SetPropertyAndValidate(ref _maloainguyenlieu, value); } }
    
        private int _duongkinh;
        public int DuongKinh { get { return _duongkinh; } set { SetPropertyAndValidate(ref _duongkinh, value); } }
    
    
        public virtual rLoaiNguyenLieu rLoaiNguyenLieu { get; set; }
        public virtual ICollection<rMatHangNguyenLieu> rMatHangNguyenLieux { get; set; }
        public virtual ICollection<tNhapNguyenLieu> tNhapNguyenLieux { get; set; }
    }
}
