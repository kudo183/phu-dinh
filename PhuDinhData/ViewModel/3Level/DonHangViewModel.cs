using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using PhuDinhCommon;
using PhuDinhData.Filter;
using PhuDinhData.ViewModel.DataGridColumnHeaderFilterModel;

namespace PhuDinhData.ViewModel
{
    public class DonHangViewModel : BaseViewModel<tDonHang>
    {
        private List<rKhachHang> _rKhachHangs;
        private List<rChanh> _rChanhs;

        private bool _isUsedDateFilter = true;
        private DateTime _filterDate = DateTime.Now.Date;

        private string _filterKhachHang = string.Empty;

        private string _filterChanh = string.Empty;

        public static HeaderDateFilterModel Header_Ngay = new HeaderDateFilterModel(Constant.ColumnName_Ngay);
        public static HeaderTextFilterModel Header_KhachHang = new HeaderTextFilterModel(Constant.ColumnName_KhachHang);
        public static HeaderTextFilterModel Header_Chanh = new HeaderTextFilterModel(Constant.ColumnName_Chanh);

        public DonHangViewModel()
        {
            Entity = new ObservableCollection<tDonHang>();

            MainFilter = new Filter_tDonHang();

            SetReferenceFilter<rKhachHang>(Constant.ColumnName_KhachHang, (p => true));
            SetReferenceFilter<rChanh>(Constant.ColumnName_Chanh, (p => true));
        }

        public override void Load()
        {
            _isLoading = true;

            Entity.CollectionChanged += Entity_CollectionChanged;

            Header_Ngay.PropertyChanged += Header_Ngay_PropertyChanged;
            Header_KhachHang.PropertyChanged += Header_KhachHang_PropertyChanged;
            Header_Chanh.PropertyChanged += Header_Chanh_PropertyChanged;

            DonHangViewModel.Header_Ngay.Date = _filterDate;
            DonHangViewModel.Header_Ngay.IsUsed = _isUsedDateFilter;

            DonHangViewModel.Header_KhachHang.Text = _filterKhachHang;

            DonHangViewModel.Header_Chanh.Text = _filterChanh;

            _isLoading = false;
        }

        public override void Unload()
        {
            Entity.CollectionChanged -= Entity_CollectionChanged;

            Header_Ngay.PropertyChanged -= Header_Ngay_PropertyChanged;
            Header_KhachHang.PropertyChanged -= Header_KhachHang_PropertyChanged;
            Header_Chanh.PropertyChanged -= Header_Chanh_PropertyChanged;

            _filterDate = DonHangViewModel.Header_Ngay.Date;
            _isUsedDateFilter = DonHangViewModel.Header_Ngay.IsUsed;

            _filterKhachHang = DonHangViewModel.Header_KhachHang.Text;

            _filterChanh = DonHangViewModel.Header_Chanh.Text;
        }

        void Header_Ngay_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (DonHangViewModel.Header_Ngay.IsUsed)
            {
                MainFilter.SetFilter(Filter_tDonHang.Ngay, DonHangViewModel.Header_Ngay.Date);
            }
            else
            {
                MainFilter.SetFilter(Filter_tDonHang.Ngay, null);
            }

            OnHeaderFilterChanged();
        }

        void Header_KhachHang_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            MainFilter.SetFilter(Filter_tDonHang.TenKhachHang, DonHangViewModel.Header_KhachHang.Text);

            OnHeaderFilterChanged();
        }

        void Header_Chanh_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            MainFilter.SetFilter(Filter_tDonHang.TenChanh, DonHangViewModel.Header_Chanh.Text);

            OnHeaderFilterChanged();
        }

        void Entity_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var tDonHang = e.NewItems[0] as tDonHang;

                tDonHang.Ngay = (DonHangViewModel.Header_Ngay.IsUsed)
                    ? DonHangViewModel.Header_Ngay.Date : DateTime.Now;

                tDonHang.rChanhList = _rChanhs;
                tDonHang.rKhachHangList = _rKhachHangs;

                if (GetDefaultValue(Constant.ColumnName_MaKhachHang) != null)
                {
                    tDonHang.MaKhachHang = Convert.ToInt32(GetDefaultValue(Constant.ColumnName_MaKhachHang));
                }
            }
        }

        private void UpdateKhachHangReferenceData()
        {
            UpdateReferenceData(out _rKhachHangs
                , GetReferenceFilter<rKhachHang>(Constant.ColumnName_KhachHang)
                , (p => p.rKhachHangList = _rKhachHangs));
        }

        private void UpdateChanhReferenceData()
        {
            UpdateReferenceData(out _rChanhs, 
                GetReferenceFilter<rChanh>(Constant.ColumnName_Chanh), 
                (p => p.rChanhList = _rChanhs));
        }

        protected override void UpdateAllReferenceData()
        {
            UpdateKhachHangReferenceData();
            UpdateChanhReferenceData();
        }

        public override void UpdateReferenceData(string columnName)
        {
            switch (columnName)
            {
                case Constant.ColumnName_KhachHang:
                    UpdateKhachHangReferenceData();
                    break;
                case Constant.ColumnName_Chanh:
                    UpdateChanhReferenceData();
                    break;
            }
        }
    }
}
