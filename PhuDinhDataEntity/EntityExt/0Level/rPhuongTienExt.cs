namespace PhuDinhDataEntity
{
    [global::System.Data.Services.Common.EntitySetAttribute("rPhuongTiens")]
    [global::System.Data.Services.Common.DataServiceKeyAttribute("Ma")]
    public partial class rPhuongTien
    {
        public override string ToString()
        {
            return string.Format("[Ma {0}] [TenPhuongTien {1}]", Ma, TenPhuongTien);
        }
    }
}
