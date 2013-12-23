using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq.Expressions;
using PhuDinhCommon;
using PhuDinhData.Filter;
using PhuDinhData.ViewModel.DataGridColumnHeaderFilterModel;

namespace PhuDinhData.ViewModel
{
    public class ChuyenHangDonHangViewModel : BaseViewModel<tChuyenHangDonHang>
    {
        private List<tChuyenHang> _tChuyenHangs;
        private List<tDonHang> _tDonHangs;

        private string _filterChuyenHang = string.Empty;

        private string _filterDonHang = string.Empty;

        public static HeaderTextFilterModel Header_ChuyenHang = new HeaderTextFilterModel(Constant.ColumnName_ChuyenHang);
        public static HeaderTextFilterModel Header_DonHang = new HeaderTextFilterModel(Constant.ColumnName_DonHang);

        public ChuyenHangDonHangViewModel()
        {
            Entity = new ObservableCollection<tChuyenHangDonHang>();

            MainFilter = new Filter_tChuyenHangDonHang();

            SetReferenceFilter<tChuyenHang>(Constant.ColumnName_ChuyenHang, (p => true));
            SetReferenceFilter<tDonHang>(Constant.ColumnName_DonHang, (p => true));
        }

        public override void Load()
        {
            _isLoading = true;

            Entity.CollectionChanged += Entity_CollectionChanged;

            Header_ChuyenHang.PropertyChanged += Header_ChuyenHang_PropertyChanged;
            Header_DonHang.PropertyChanged += Header_DonHang_PropertyChanged;

            ChuyenHangDonHangViewModel.Header_ChuyenHang.Text = _filterChuyenHang;

            ChuyenHangDonHangViewModel.Header_DonHang.Text = _filterDonHang;

            _isLoading = false;
        }

        public override void Unload()
        {
            Entity.CollectionChanged -= Entity_CollectionChanged;

            Header_ChuyenHang.PropertyChanged -= Header_ChuyenHang_PropertyChanged;
            Header_DonHang.PropertyChanged -= Header_DonHang_PropertyChanged;

            _filterChuyenHang = ChuyenHangDonHangViewModel.Header_ChuyenHang.Text;

            _filterDonHang = ChuyenHangDonHangViewModel.Header_DonHang.Text;
        }

        void Header_ChuyenHang_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            MainFilter.SetFilter(Filter_tChuyenHangDonHang.TenChuyenHang, ChuyenHangDonHangViewModel.Header_ChuyenHang.Text);

            OnHeaderFilterChanged();
        }

        void Header_DonHang_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            MainFilter.SetFilter(Filter_tChuyenHangDonHang.TenDonHang, ChuyenHangDonHangViewModel.Header_DonHang.Text);

            OnHeaderFilterChanged();
        }

        void Entity_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var tChuyenHangDonHang = e.NewItems[0] as tChuyenHangDonHang;
                tChuyenHangDonHang.tChuyenHangList = _tChuyenHangs;
                tChuyenHangDonHang.tDonHangList = _tDonHangs;

                if (GetDefaultValue(Constant.ColumnName_MaChuyenHang) != null)
                {
                    tChuyenHangDonHang.MaChuyenHang =
                        Convert.ToInt32(GetDefaultValue(Constant.ColumnName_MaChuyenHang));
                }

                if (GetDefaultValue(Constant.ColumnName_MaDonHang) != null)
                {
                    tChuyenHangDonHang.MaDonHang =
                        Convert.ToInt32(GetDefaultValue(Constant.ColumnName_MaDonHang));
                }
            }
        }

        private void UpdateChuyenHangReferenceData()
        {
            UpdateReferenceData(out _tChuyenHangs
                , GetReferenceFilter<tChuyenHang>(Constant.ColumnName_ChuyenHang)
                , (p => p.tChuyenHangList = _tChuyenHangs));
        }

        private void UpdateDonHangReferenceData()
        {
            UpdateReferenceData(out _tDonHangs
                , GetReferenceFilter<tDonHang>(Constant.ColumnName_DonHang)
                , (p => p.tDonHangList = _tDonHangs));
        }

        protected override void UpdateAllReferenceData()
        {
            UpdateChuyenHangReferenceData();
            UpdateDonHangReferenceData();
        }

        public override void UpdateReferenceData(string columnName)
        {
            switch (columnName)
            {
                case Constant.ColumnName_ChuyenHang:
                    UpdateChuyenHangReferenceData();
                    break;
                case Constant.ColumnName_DonHang:
                    UpdateDonHangReferenceData();
                    break;
            }
        }
    }
}
