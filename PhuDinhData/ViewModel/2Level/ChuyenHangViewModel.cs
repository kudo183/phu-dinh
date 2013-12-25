using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using PhuDinhData.Filter;
using PhuDinhData.ViewModel.DataGridColumnHeaderFilterModel;
using System;
using PhuDinhCommon;

namespace PhuDinhData.ViewModel
{
    public class ChuyenHangViewModel : BaseViewModel<tChuyenHang>
    {
        private List<rNhanVien> _rNhanViens;

        private bool _isUsedDateFilter = true;
        private DateTime _filterDate = DateTime.Now.Date;

        private string _filterNhanVien = string.Empty;

        public static HeaderDateFilterModel Header_Ngay = new HeaderDateFilterModel(Constant.ColumnName_Ngay);
        public static HeaderTextFilterModel Header_NhanVien = new HeaderTextFilterModel(Constant.ColumnName_NhanVien);

        public ChuyenHangViewModel()
        {
            Entity = new ObservableCollection<tChuyenHang>();

            MainFilter = new Filter_tChuyenHang();

            SetReferenceFilter<rNhanVien>(Constant.ColumnName_NhanVien, (p => true));
        }

        public override void Load()
        {
            _isLoading = true;

            Entity.CollectionChanged += Entity_CollectionChanged;
            Header_Ngay.PropertyChanged += Header_Ngay_PropertyChanged;
            Header_NhanVien.PropertyChanged += Header_NhanVien_PropertyChanged;

            Header_Ngay.IsUsed = _isUsedDateFilter;
            Header_Ngay.Date = _filterDate;

            Header_NhanVien.Text = _filterNhanVien;

            _isLoading = false;
        }

        public override void Unload()
        {
            Entity.CollectionChanged -= Entity_CollectionChanged;

            Header_Ngay.PropertyChanged -= Header_Ngay_PropertyChanged;
            Header_NhanVien.PropertyChanged -= Header_NhanVien_PropertyChanged;

            _filterDate = Header_Ngay.Date;
            _isUsedDateFilter = Header_Ngay.IsUsed;

            _filterNhanVien = Header_NhanVien.Text;
        }

        void Header_NhanVien_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            MainFilter.SetFilter(Filter_tChuyenHang.TenNhanVien, Header_NhanVien.Text);

            OnHeaderFilterChanged();
        }

        void Header_Ngay_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (Header_Ngay.IsUsed)
            {
                MainFilter.SetFilter(Filter_tChuyenHang.Ngay, Header_Ngay.Date);
            }
            else
            {
                MainFilter.SetFilter(Filter_tChuyenHang.Ngay, null);
            }

            OnHeaderFilterChanged();
        }

        void Entity_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var tChuyenHang = e.NewItems[0] as tChuyenHang;
                tChuyenHang.Ngay = (Header_Ngay.IsUsed) ? Header_Ngay.Date : DateTime.Now;
                tChuyenHang.rNhanVienList = _rNhanViens;

                if (GetDefaultValue(Constant.ColumnName_MaNhanVien) != null)
                {
                    tChuyenHang.MaNhanVienGiaoHang = Convert.ToInt32(GetDefaultValue(Constant.ColumnName_MaNhanVien));
                }
            }
        }

        private void UpdateNhanVienReferenceData()
        {
            UpdateReferenceData(out _rNhanViens
                , GetReferenceFilter<rNhanVien>(Constant.ColumnName_NhanVien)
                , (p => p.rNhanVienList = _rNhanViens));
        }

        protected override void UpdateAllReferenceData()
        {
            UpdateNhanVienReferenceData();
        }

        public override void UpdateReferenceData(string columnName)
        {
            switch (columnName)
            {
                case Constant.ColumnName_NhanVien:
                    UpdateNhanVienReferenceData();
                    break;
            }
        }
    }
}
