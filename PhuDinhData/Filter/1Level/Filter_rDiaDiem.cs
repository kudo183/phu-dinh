﻿using PhuDinhDataEntity;

namespace PhuDinhData.Filter
{
    public class Filter_rDiaDiem : FilterBase<rDiaDiem>
    {
        public const string MaNuoc = "MaNuoc";
        public const string TenNuoc = "TenNuoc";
        public const string TenTinh = "TenTinh";

        public Filter_rDiaDiem()
        {
            _filters[MaNuoc] = (p => true);
            _filters[TenNuoc] = (p => true);
            _filters[TenTinh] = (p => true);

            UpdateMainFilter();
        }

        public override void SetFilter(string key, object value, bool setFalse = false)
        {
            switch (key)
            {
                case MaNuoc:
                    SetFilterMaNuoc(value as int?, setFalse);
                    break;
                case TenNuoc:
                    SetFilterTenNuoc(value as string, setFalse);
                    break;
                case TenTinh:
                    SetFilterTenTinh(value as string, setFalse);
                    break;
            }
        }

        private void SetFilterMaNuoc(int? maNuoc, bool setFalse = false)
        {
            _filters[MaNuoc] = FilterNullable(maNuoc, setFalse, "MaNuoc");

            UpdateMainFilter();
        }

        private void SetFilterTenNuoc(string tenNuoc, bool setFalse = false)
        {
            _filters[TenNuoc] = FilterText(tenNuoc, setFalse, "rNuoc.TenNuoc");

            UpdateMainFilter();
        }

        private void SetFilterTenTinh(string tenTinh, bool setFalse = false)
        {
            _filters[TenTinh] = FilterText(tenTinh, setFalse, "Tinh");

            UpdateMainFilter();
        }
    }
}
