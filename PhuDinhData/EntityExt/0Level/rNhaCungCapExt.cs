namespace PhuDinhData
{
    [global::System.Data.Services.Common.EntitySetAttribute("rNhaCungCaps")]
    [global::System.Data.Services.Common.DataServiceKeyAttribute("Ma")]
    public partial class rNhaCungCap
    {
        public override string ToString()
        {
            return string.Format("[Ma {0}] [TenNhaCungCap {1}]", Ma, TenNhaCungCap);
        }
    }
}
