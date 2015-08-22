using System.Collections.Generic;

namespace PhuDinhDataEntity
{
    [global::System.Data.Services.Common.EntitySetAttribute("tCongNoKhachHangs")]
    [global::System.Data.Services.Common.DataServiceKeyAttribute("Ma")]
    public partial class tCongNoKhachHang
    {
        public List<rKhachHang> rKhachHangList { get; set; }
    }
}
