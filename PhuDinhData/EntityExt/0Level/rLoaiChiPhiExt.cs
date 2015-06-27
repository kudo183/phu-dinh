namespace PhuDinhData
{
    [global::System.Data.Services.Common.EntitySetAttribute("rLoaiChiPhis")]
    [global::System.Data.Services.Common.DataServiceKeyAttribute("Ma")]
    public partial class rLoaiChiPhi
    {
        public override string ToString()
        {
            return string.Format("[Ma {0}] [TenLoaiChiPhi {1}]", Ma, TenLoaiChiPhi);
        }
    }
}
