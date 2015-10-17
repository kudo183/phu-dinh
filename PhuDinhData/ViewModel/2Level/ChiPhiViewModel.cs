using System.Linq;
using PhuDinhDataEntity;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using PhuDinhData.Filter;
using System;
using PhuDinhCommon;
using CustomControl.DataGridColumnHeaderFilterModel;

namespace PhuDinhData.ViewModel
{
    public class ChiPhiViewModel : BaseViewModel<tChiPhi>
    {
        private List<rLoaiChiPhi> _rLoaiChiPhis;
        private List<rNhanVien> _rNhanViens;

        public HeaderComboBoxFilterModel Header_LoaiChiPhi { get; set; }
        public HeaderComboBoxFilterModel Header_NhanVien { get; set; }

        private HeaderDateFilterModel _header_Ngay = new HeaderDateFilterModel(Constant.ColumnName_Ngay);
        public HeaderDateFilterModel Header_Ngay
        {
            get { return _header_Ngay; }
            set { _header_Ngay = value; }
        }

        public ChiPhiViewModel()
        {
            Entity = new ObservableCollection<tChiPhi>();

            MainFilter = new Filter_tChiPhi();

            SetDefaultValue(Constant.ColumnName_MaLoaiChiPhi, 2);
            SetDefaultValue(Constant.ColumnName_MaNhanVien, 1);

            Header_LoaiChiPhi = new HeaderComboBoxFilterModel(Constant.ColumnName_LoaiChiPhi);
            Header_NhanVien = new HeaderComboBoxFilterModel(Constant.ColumnName_NhanVien);

            Header_LoaiChiPhi.IsUsed = false;
            Header_NhanVien.IsUsed = false;
        }

        public override void Load()
        {
            _isLoading = true;

            Header_LoaiChiPhi.SelectedValue = GetDefaultValue(Constant.ColumnName_MaLoaiChiPhi);
            Header_NhanVien.SelectedValue = GetDefaultValue(Constant.ColumnName_MaNhanVien);

            Entity.CollectionChanged += Entity_CollectionChanged;

            Header_Ngay.PropertyChanged += Header_Ngay_PropertyChanged;

            Header_LoaiChiPhi.PropertyChanged += Header_LoaiChiPhi_PropertyChanged;
            Header_NhanVien.PropertyChanged += Header_NhanVien_PropertyChanged;

            Header_Ngay_PropertyChanged(null, null);
            Header_LoaiChiPhi_PropertyChanged(null, null);
            Header_NhanVien_PropertyChanged(null, null);

            _isLoading = false;
        }

        public override void Unload()
        {
            Entity.CollectionChanged -= Entity_CollectionChanged;

            Header_Ngay.PropertyChanged -= Header_Ngay_PropertyChanged;
            Header_LoaiChiPhi.PropertyChanged -= Header_LoaiChiPhi_PropertyChanged;
            Header_NhanVien.PropertyChanged -= Header_NhanVien_PropertyChanged;
        }

        void Header_LoaiChiPhi_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            MainFilter.SetFilter(Filter_tChiPhi.MaLoaiChiPhi, Header_LoaiChiPhi.GetFilterValue());

            if (_isLoading == false)
            {
                SetDefaultValue(Constant.ColumnName_MaLoaiChiPhi, Header_LoaiChiPhi.SelectedValue);
            }

            OnHeaderFilterChanged();
        }

        void Header_NhanVien_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            MainFilter.SetFilter(Filter_tChiPhi.MaNhanVien, Header_NhanVien.GetFilterValue());

            if (_isLoading == false)
            {
                SetDefaultValue(Constant.ColumnName_MaNhanVien, Header_NhanVien.SelectedValue);
            }

            OnHeaderFilterChanged();
        }

        void Header_Ngay_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            MainFilter.SetFilter(Filter_tChiPhi.Ngay, Header_Ngay.GetFilterValue());

            OnHeaderFilterChanged();
        }

        void Entity_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var tChiPhi = e.NewItems[0] as tChiPhi;
                tChiPhi.Ngay = (Header_Ngay.IsUsed) ? Header_Ngay.Date : DateTime.Now;
                tChiPhi.rLoaiChiPhiList = _rLoaiChiPhis;
                tChiPhi.rNhanVienList = _rNhanViens;

                if (GetDefaultValue(Constant.ColumnName_MaLoaiChiPhi) != null)
                {
                    tChiPhi.MaLoaiChiPhi = Convert.ToInt32(GetDefaultValue(Constant.ColumnName_MaLoaiChiPhi));
                }

                if (GetDefaultValue(Constant.ColumnName_MaNhanVien) != null)
                {
                    tChiPhi.MaNhanVienGiaoHang = Convert.ToInt32(GetDefaultValue(Constant.ColumnName_MaNhanVien));
                }
            }
        }

        private void UpdateLoaiChiPhiReferenceData()
        {
            UpdateReferenceData(out _rLoaiChiPhis
                , GetReferenceFilter<rLoaiChiPhi>(Constant.ColumnName_LoaiChiPhi)
                , (p => p.rLoaiChiPhiList = _rLoaiChiPhis));

            Header_LoaiChiPhi.ItemSource = _rLoaiChiPhis.ToDictionary(p => p.Ma, p => p.TenLoaiChiPhi);
            Header_LoaiChiPhi.SelectedValue = GetDefaultValue(Constant.ColumnName_MaLoaiChiPhi);
        }

        private void UpdateNhanVienReferenceData()
        {
            UpdateReferenceData(out _rNhanViens
                , GetReferenceFilter<rNhanVien>(Constant.ColumnName_NhanVien)
                , (p => p.rNhanVienList = _rNhanViens));

            Header_NhanVien.ItemSource = _rNhanViens.ToDictionary(p => p.Ma, p => p.TenNhanVien);
            Header_NhanVien.SelectedValue = GetDefaultValue(Constant.ColumnName_MaNhanVien);
        }

        protected override void UpdateAllReferenceData()
        {
            UpdateLoaiChiPhiReferenceData();
            UpdateNhanVienReferenceData();
        }

        public override void UpdateReferenceData(string columnName)
        {
            switch (columnName)
            {
                case Constant.ColumnName_LoaiChiPhi:
                    UpdateLoaiChiPhiReferenceData();
                    break;
                case Constant.ColumnName_NhanVien:
                    UpdateNhanVienReferenceData();
                    break;
            }
        }

        public override void RefreshData()
        {
            base.RefreshData();

            Message = string.Format("Tong so tien: {0:N0}", Entity.Sum(p => p.SoTien));
        }
    }
}
