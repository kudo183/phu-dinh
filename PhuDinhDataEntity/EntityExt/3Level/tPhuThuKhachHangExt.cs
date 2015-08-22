using System.Collections.Generic;

namespace PhuDinhDataEntity
{
    [global::System.Data.Services.Common.EntitySetAttribute("tPhuThuKhachHangs")]
    [global::System.Data.Services.Common.DataServiceKeyAttribute("Ma")]
    public partial class tPhuThuKhachHang
    {
        public List<rKhachHang> rKhachHangList { get; set; }
    }
}
