using System.Collections.Generic;

namespace PhuDinhDataEntity
{
    [global::System.Data.Services.Common.EntitySetAttribute("tGiamTruKhachHangs")]
    [global::System.Data.Services.Common.DataServiceKeyAttribute("Ma")]
    public partial class tGiamTruKhachHang
    {
        public List<rKhachHang> rKhachHangList { get; set; }
    }
}
