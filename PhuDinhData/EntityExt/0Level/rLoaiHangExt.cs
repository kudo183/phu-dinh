namespace PhuDinhData
{
    [global::System.Data.Services.Common.EntitySetAttribute("rLoaiHangs")]
    [global::System.Data.Services.Common.DataServiceKeyAttribute("Ma")]
    public partial class rLoaiHang
    {
        public override string ToString()
        {
            return string.Format("[Ma {0}] [TenLoai {1}]", Ma, TenLoai);
        }
    }
}
