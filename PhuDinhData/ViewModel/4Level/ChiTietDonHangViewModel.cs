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
    public class ChiTietDonHangViewModel : BaseViewModel<tChiTietDonHang>
    {
        private List<tDonHang> _tDonHangs;
        private List<tMatHang> _tMatHangs;

        private string _filterMatHang = string.Empty;

        private string _filterDonHang = string.Empty;

        public static HeaderTextFilterModel Header_MatHang = new HeaderTextFilterModel(Constant.ColumnName_MatHang);
        public static HeaderTextFilterModel Header_DonHang = new HeaderTextFilterModel(Constant.ColumnName_DonHang);

        public ChiTietDonHangViewModel()
        {
            Entity = new ObservableCollection<tChiTietDonHang>();

            MainFilter = new Filter_tChiTietDonHang();

            SetReferenceFilter<tMatHang>(Constant.ColumnName_MatHang, (p => true));
            SetReferenceFilter<tDonHang>(Constant.ColumnName_DonHang, (p => true));
        }

        public override void Load()
        {
            _isLoading = true;

            Entity.CollectionChanged += Entity_CollectionChanged;

            Header_MatHang.PropertyChanged += Header_MatHang_PropertyChanged;
            Header_DonHang.PropertyChanged += Header_DonHang_PropertyChanged;

            Header_MatHang.Text = _filterMatHang;

            Header_DonHang.Text = _filterDonHang;

            _isLoading = false;
        }

        public override void Unload()
        {
            Entity.CollectionChanged -= Entity_CollectionChanged;

            Header_MatHang.PropertyChanged -= Header_MatHang_PropertyChanged;
            Header_DonHang.PropertyChanged -= Header_DonHang_PropertyChanged;

            _filterMatHang = Header_MatHang.Text;

            _filterDonHang = Header_DonHang.Text;
        }

        void Header_MatHang_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            MainFilter.SetFilter(Filter_tChiTietDonHang.TenMatHang, Header_MatHang.Text);

            OnHeaderFilterChanged();
        }

        void Header_DonHang_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            MainFilter.SetFilter(Filter_tChiTietDonHang.TenDonHang, Header_DonHang.Text);

            OnHeaderFilterChanged();
        }

        void Entity_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var tChiTietDonHang = e.NewItems[0] as tChiTietDonHang;
                tChiTietDonHang.tDonHangList = _tDonHangs;
                tChiTietDonHang.tMatHangList = _tMatHangs;
                
                if (GetDefaultValue(Constant.ColumnName_MaDonHang) != null)
                {
                    tChiTietDonHang.MaDonHang =
                        Convert.ToInt32(GetDefaultValue(Constant.ColumnName_MaDonHang));
                }
            }
        }

        private void UpdateMatHangReferenceData()
        {
            UpdateReferenceData(out _tMatHangs
                , GetReferenceFilter<tMatHang>(Constant.ColumnName_MatHang)
                , (p => p.tMatHangList = _tMatHangs));
        }

        private void UpdateDonHangReferenceData()
        {
            UpdateReferenceData(out _tDonHangs
                , GetReferenceFilter<tDonHang>(Constant.ColumnName_DonHang)
                , (p => p.tDonHangList = _tDonHangs));
        }

        protected override void UpdateAllReferenceData()
        {
            UpdateMatHangReferenceData();
            UpdateDonHangReferenceData();
        }

        public override void UpdateReferenceData(string columnName)
        {
            switch (columnName)
            {
                case Constant.ColumnName_MatHang:
                    UpdateMatHangReferenceData();
                    break;
                case Constant.ColumnName_DonHang:
                    UpdateDonHangReferenceData();
                    break;
            }
        }
    }
}
