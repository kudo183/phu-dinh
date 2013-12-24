using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using PhuDinhData.Filter;
using PhuDinhData.ViewModel.DataGridColumnHeaderFilterModel;
using System;
using PhuDinhCommon;

namespace PhuDinhData.ViewModel
{
    public class ChiPhiViewModel : BaseViewModel<tChiPhi>
    {
        private List<rLoaiChiPhi> _rLoaiChiPhis;
        private List<rNhanVien> _rNhanViens;

        private bool _isUsedDateFilter = true;
        private DateTime _filterDate = DateTime.Now.Date;

        private string _filterLoaiChiPhi = string.Empty;
        private string _filterNhanVien = string.Empty;

        public static HeaderDateFilterModel Header_Ngay = new HeaderDateFilterModel(Constant.ColumnName_Ngay);
        public static HeaderTextFilterModel Header_LoaiChiPhi = new HeaderTextFilterModel(Constant.ColumnName_LoaiChiPhi);
        public static HeaderTextFilterModel Header_NhanVien = new HeaderTextFilterModel(Constant.ColumnName_NhanVien);

        public ChiPhiViewModel()
        {
            Entity = new ObservableCollection<tChiPhi>();

            MainFilter = new Filter_tChiPhi();

            SetReferenceFilter<rLoaiChiPhi>(Constant.ColumnName_LoaiChiPhi, (p => true));
            SetReferenceFilter<rNhanVien>(Constant.ColumnName_NhanVien, (p => true));
        }

        public override void Load()
        {
            _isLoading = true;

            Entity.CollectionChanged += Entity_CollectionChanged;
            Header_Ngay.PropertyChanged += Header_Ngay_PropertyChanged;
            Header_LoaiChiPhi.PropertyChanged += Header_LoaiChiPhi_PropertyChanged;
            Header_NhanVien.PropertyChanged += Header_NhanVien_PropertyChanged;

            Header_Ngay.IsUsed = _isUsedDateFilter;
            Header_Ngay.Date = _filterDate;

            Header_LoaiChiPhi.Text = _filterLoaiChiPhi;

            Header_NhanVien.Text = _filterNhanVien;

            _isLoading = false;
        }

        public override void Unload()
        {
            Entity.CollectionChanged -= Entity_CollectionChanged;

            Header_Ngay.PropertyChanged -= Header_Ngay_PropertyChanged;
            Header_LoaiChiPhi.PropertyChanged -= Header_LoaiChiPhi_PropertyChanged;
            Header_NhanVien.PropertyChanged -= Header_NhanVien_PropertyChanged;

            _filterDate = Header_Ngay.Date;
            _isUsedDateFilter = Header_Ngay.IsUsed;

            _filterLoaiChiPhi = Header_LoaiChiPhi.Text;

            _filterNhanVien = Header_NhanVien.Text;
        }

        void Header_LoaiChiPhi_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            MainFilter.SetFilter(Filter_tChiPhi.TenLoaiChiPhi, Header_LoaiChiPhi.Text);

            OnHeaderFilterChanged();
        }

        void Header_NhanVien_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            MainFilter.SetFilter(Filter_tChiPhi.TenNhanVien, Header_NhanVien.Text);

            OnHeaderFilterChanged();
        }

        void Header_Ngay_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (Header_Ngay.IsUsed)
            {
                MainFilter.SetFilter(Filter_tChiPhi.Ngay, Header_Ngay.Date);
            }
            else
            {
                MainFilter.SetFilter(Filter_tChiPhi.Ngay, null);
            }

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
        }

        private void UpdateNhanVienReferenceData()
        {
            UpdateReferenceData(out _rNhanViens
                , GetReferenceFilter<rNhanVien>(Constant.ColumnName_NhanVien)
                , (p => p.rNhanVienList = _rNhanViens));
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
    }
}
