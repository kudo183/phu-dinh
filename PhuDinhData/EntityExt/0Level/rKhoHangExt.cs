namespace PhuDinhData
{
    [global::System.Data.Services.Common.EntitySetAttribute("rKhoHangs")]
    [global::System.Data.Services.Common.DataServiceKeyAttribute("Ma")]
    public partial class rKhoHang
    {
        public override string ToString()
        {
            return string.Format("[Ma {0}] [TenKho {1}] [TrangThai {2}]", Ma, TenKho, TrangThai);
        }
    }
}
