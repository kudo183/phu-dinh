namespace PhuDinhData
{
    [global::System.Data.Services.Common.EntitySetAttribute("rNuocs")]
    [global::System.Data.Services.Common.DataServiceKeyAttribute("Ma")]
    public partial class rNuoc
    {
        public override string ToString()
        {
            return string.Format("[Ma {0}] [TenNuoc {1}]", Ma, TenNuoc);
        }
    }
}
