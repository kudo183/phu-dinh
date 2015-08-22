using System.Collections.Generic;

namespace PhuDinhDataEntity
{
    [global::System.Data.Services.Common.EntitySetAttribute("tNhanTienKhachHangs")]
    [global::System.Data.Services.Common.DataServiceKeyAttribute("Ma")]
    public partial class tNhanTienKhachHang
    {
        public List<rKhachHang> rKhachHangList { get; set; }
    }
}
