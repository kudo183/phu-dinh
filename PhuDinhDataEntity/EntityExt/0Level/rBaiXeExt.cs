namespace PhuDinhDataEntity
{
    [global::System.Data.Services.Common.EntitySetAttribute("rBaiXes")]
    [global::System.Data.Services.Common.DataServiceKeyAttribute("Ma")]
    public partial class rBaiXe
    {
        public override string ToString()
        {
            return string.Format("[Ma {0}] [DiaDiemBaiXe {1}]", Ma, DiaDiemBaiXe);
        }
    }
}
