using System;
using System.Linq.Expressions;
using PhuDinhCommon;

namespace PhuDinhData.Filter
{
    public class Filter_rDiaDiem
    {
        private Expression<Func<rDiaDiem, bool>> _filterMaNuoc;
        private Expression<Func<rDiaDiem, bool>> _filterTenNuoc;
        private Expression<Func<rDiaDiem, bool>> _filterTenTinh;

        public bool IsClearAllData { get; set; }

        public Filter_rDiaDiem()
        {
            IsClearAllData = false;

            _filterMaNuoc = (p => true);
            _filterTenNuoc = (p => true);
            _filterTenTinh = (p => true);

            UpdateMainFilter();
        }

        public void SetFilterMaNuoc(int? maNuoc, bool setFalse = false)
        {
            IsClearAllData = false;

            if (setFalse == true)
            {
                _filterMaNuoc = (p => false);
            }
            else
            {
                if (maNuoc == null)
                {
                    _filterMaNuoc = (p => true);
                }
                else
                {
                    _filterMaNuoc = (p => p.MaNuoc == maNuoc);
                }
            }

            UpdateMainFilter();
        }

        public void SetFilterTenNuoc(string tenNuoc, bool setFalse = false)
        {
            IsClearAllData = false;

            if (setFalse == true)
            {
                _filterTenNuoc = (p => false);
            }
            else
            {
                if (string.IsNullOrEmpty(tenNuoc))
                {
                    _filterTenNuoc = (p => true);
                }
                else
                {
                    _filterTenNuoc = (p => p.rNuoc.TenNuoc.Contains(tenNuoc));
                }
            }

            UpdateMainFilter();
        }

        public void SetFilterTenTinh(string tenTinh, bool setFalse = false)
        {
            IsClearAllData = false;

            if (setFalse == true)
            {
                _filterTenTinh = (p => false);
            }
            else
            {
                if (string.IsNullOrEmpty(tenTinh))
                {
                    _filterTenTinh = (p => true);
                }
                else
                {
                    _filterTenTinh = (p => p.Tinh.Contains(tenTinh));
                }
            }

            UpdateMainFilter();
        }

        private void UpdateMainFilter()
        {
            FilterDiaDiem = _filterTenTinh.And(_filterMaNuoc).And(_filterTenNuoc);
        }

        public Expression<Func<rDiaDiem, bool>> FilterDiaDiem { get; private set; }
    }
}
