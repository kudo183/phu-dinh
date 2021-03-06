﻿using System.Collections.Generic;

namespace PhuDinhDataEntity
{
    [global::System.Data.Services.Common.EntitySetAttribute("tChiTietDonHangs")]
    [global::System.Data.Services.Common.DataServiceKeyAttribute("Ma")]
    [global::System.Data.Services.IgnoreProperties("TenChiTietDonHang")]
    public partial class tChiTietDonHang
    {
        public string TenChiTietDonHang
        {
            get
            {
                return string.Format("{0}_{1}"
                    , tDonHang == null ? "" : tDonHang.TenDonHang
                    , tMatHang == null ? "" : tMatHang.TenMatHangLoaiHang);
            }
        }

        public List<tDonHang> tDonHangList { get; set; }
        public List<tMatHang> tMatHangList { get; set; }

        public override string ToString()
        {
            return string.Format("[Ma {0}] [DonHang {1}] [MatHang {2}] [SoLuong {3}] [Xong {4}]", Ma
                , tDonHang == null ? "" : tDonHang.TenDonHang
                , tMatHang == null ? "" : tMatHang.TenMatHang
                , SoLuong, Xong);
        }
    }
}
