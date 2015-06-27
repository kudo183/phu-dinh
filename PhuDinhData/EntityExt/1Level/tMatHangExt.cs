using System.Collections.Generic;

namespace PhuDinhData
{
    [global::System.Data.Services.Common.EntitySetAttribute("tMatHangs")]
    [global::System.Data.Services.Common.DataServiceKeyAttribute("Ma")]
    [global::System.Data.Services.IgnoreProperties("TenMatHangLoaiHang")]
    public partial class tMatHang
    {
        public string TenMatHangLoaiHang
        {
            get
            {
                return string.Format("{0}", TenMatHang);
            }
        }

        public List<rLoaiHang> rLoaiHangList { get; set; }

        public override string ToString()
        {
            return string.Format("[Ma {0}] [LoaiHang {1}] [TenMatHang {2}] [TenMatHangDayDu {3}] [SoMet {4}] [SoKy {5}]", Ma
                , rLoaiHang == null ? "" : rLoaiHang.TenLoai
                , TenMatHang, TenMatHangDayDu, SoMet, SoKy);
        }

        protected override void Init()
        {
            base.Init();

            _validators.Add("MaLoai", () => Common.MyValidators.Validate_NumberRange(this, "MaLoai", 1, double.MaxValue));
            _validators.Add("TenMatHang", () => Common.MyValidators.Validate_RequiredText(this, "TenMatHang"));
            _validators.Add("TenMatHangDayDu", () => Common.MyValidators.Validate_RequiredText(this, "TenMatHangDayDu"));
            _validators.Add("TenMatHangIn", () => Common.MyValidators.Validate_RequiredText(this, "TenMatHangIn"));
            _validators.Add("SoKy", () => Common.MyValidators.Validate_NumberRange(this, "SoKy", 0, double.MaxValue));
            _validators.Add("SoMet", () => Common.MyValidators.Validate_NumberRange(this, "SoMet", 0, double.MaxValue));
            
            Validate();
        }
    }
}
