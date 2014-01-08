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
    public class ChiTietChuyenHangDonHangViewModel : BaseViewModel<tChiTietChuyenHangDonHang>
    {
        private List<tChuyenHangDonHang> _tChuyenHangDonHangs;
        private List<tChiTietDonHang> _tChiTietDonHangs;

        private string _filterChuyenHangDonHang = string.Empty;

        private string _filterChiTietDonHang = string.Empty;

        public static HeaderTextFilterModel Header_ChuyenHangDonHang = new HeaderTextFilterModel(Constant.ColumnName_ChuyenHangDonHang);
        public static HeaderTextFilterModel Header_ChiTietDonHang = new HeaderTextFilterModel(Constant.ColumnName_ChiTietDonHang);

        public ChiTietChuyenHangDonHangViewModel()
        {
            Entity = new ObservableCollection<tChiTietChuyenHangDonHang>();

            MainFilter = new Filter_tChiTietChuyenHangDonHang();

            SetReferenceFilter<tChuyenHangDonHang>(Constant.ColumnName_ChuyenHangDonHang, (p => true));
            SetReferenceFilter<tChiTietDonHang>(Constant.ColumnName_ChiTietDonHang, (p => true));
        }

        public override void Load()
        {
            _isLoading = true;

            Entity.CollectionChanged += Entity_CollectionChanged;

            Header_ChuyenHangDonHang.Text = _filterChuyenHangDonHang;
            Header_ChuyenHangDonHang.PropertyChanged += Header_ChuyenHangDonHang_PropertyChanged;

            Header_ChiTietDonHang.Text = _filterChiTietDonHang;
            Header_ChiTietDonHang.PropertyChanged += Header_ChiTietDonHang_PropertyChanged;

            Header_ChiTietDonHang_PropertyChanged(null, null);
            Header_ChuyenHangDonHang_PropertyChanged(null, null);

            _isLoading = false;
        }

        public override void Unload()
        {
            Entity.CollectionChanged -= Entity_CollectionChanged;

            Header_ChuyenHangDonHang.PropertyChanged -= Header_ChuyenHangDonHang_PropertyChanged;
            Header_ChiTietDonHang.PropertyChanged -= Header_ChiTietDonHang_PropertyChanged;

            _filterChuyenHangDonHang = Header_ChuyenHangDonHang.Text;

            _filterChiTietDonHang = Header_ChiTietDonHang.Text;
        }

        void Header_ChuyenHangDonHang_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            MainFilter.SetFilter(Filter_tChiTietChuyenHangDonHang.TenChuyenHangDonHang, Header_ChuyenHangDonHang.Text);

            OnHeaderFilterChanged();
        }

        void Header_ChiTietDonHang_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            MainFilter.SetFilter(Filter_tChiTietChuyenHangDonHang.TenChiTietDonHang, Header_ChiTietDonHang.Text);

            OnHeaderFilterChanged();
        }

        void Entity_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var tChiTietChuyenHangDonHang = e.NewItems[0] as tChiTietChuyenHangDonHang;

                tChiTietChuyenHangDonHang.tChuyenHangDonHangList = _tChuyenHangDonHangs;
                tChiTietChuyenHangDonHang.tChiTietDonHangList = _tChiTietDonHangs;

                if (GetDefaultValue(Constant.ColumnName_MaChuyenHangDonHang) != null)
                {
                    tChiTietChuyenHangDonHang.MaChuyenHangDonHang = Convert.ToInt32(GetDefaultValue(Constant.ColumnName_MaChuyenHangDonHang));
                }
            }
        }

        private void UpdateChuyenHangDonHangReferenceData()
        {
            UpdateReferenceData(out _tChuyenHangDonHangs
                , GetReferenceFilter<tChuyenHangDonHang>(Constant.ColumnName_ChuyenHangDonHang)
                , (p => p.tChuyenHangDonHangList = _tChuyenHangDonHangs));
        }

        private void UpdateChiTietDonHangReferenceData()
        {
            UpdateReferenceData(out _tChiTietDonHangs
                , GetReferenceFilter<tChiTietDonHang>(Constant.ColumnName_ChiTietDonHang)
                , (p => p.tChiTietDonHangList = _tChiTietDonHangs));
        }

        protected override void UpdateAllReferenceData()
        {
            UpdateChuyenHangDonHangReferenceData();
            UpdateChiTietDonHangReferenceData();
        }

        public override void UpdateReferenceData(string columnName)
        {
            switch (columnName)
            {
                case Constant.ColumnName_ChuyenHangDonHang:
                    UpdateChuyenHangDonHangReferenceData();
                    break;
                case Constant.ColumnName_ChiTietDonHang:
                    UpdateChiTietDonHangReferenceData();
                    break;
            }
        }
    }
}
