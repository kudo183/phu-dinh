namespace PhuDinhData
{
    [global::System.Data.Services.Common.EntitySetAttribute("rLoaiNguyenLieus")]
    [global::System.Data.Services.Common.DataServiceKeyAttribute("Ma")]
    public partial class rLoaiNguyenLieu
    {
        public override string ToString()
        {
            return string.Format("[Ma {0}] [TenLoai {1}]", Ma, TenLoai);
        }
    }
}
