using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using PhuDinhCommon;
using PhuDinhData.Filter;
using CustomControl.DataGridColumnHeaderFilterModel;

namespace PhuDinhData.ViewModel
{
    public class ChuyenHangDonHangViewModel : BaseViewModel<tChuyenHangDonHang>
    {
        private List<tChuyenHang> _tChuyenHangs;
        private List<tDonHang> _tDonHangs;

        public HeaderTextFilterModel Header_ChuyenHang { get; set; }
        public HeaderTextFilterModel Header_DonHang { get; set; }

        public ChuyenHangDonHangViewModel()
        {
            Entity = new ObservableCollection<tChuyenHangDonHang>();

            MainFilter = new Filter_tChuyenHangDonHang();

            SetReferenceFilter<tChuyenHang>(Constant.ColumnName_ChuyenHang, (p => true));
            SetReferenceFilter<tDonHang>(Constant.ColumnName_DonHang, (p => true));

            Header_ChuyenHang = new HeaderTextFilterModel(Constant.ColumnName_ChuyenHang);
            Header_DonHang = new HeaderTextFilterModel(Constant.ColumnName_DonHang);
        }

        public override void Load()
        {
            _isLoading = true;

            Entity.CollectionChanged += Entity_CollectionChanged;

            Header_ChuyenHang.PropertyChanged += Header_ChuyenHang_PropertyChanged;
            Header_DonHang.PropertyChanged += Header_DonHang_PropertyChanged;

            Header_DonHang_PropertyChanged(null, null);
            Header_ChuyenHang_PropertyChanged(null, null);

            _isLoading = false;
        }

        public override void Unload()
        {
            Entity.CollectionChanged -= Entity_CollectionChanged;

            Header_ChuyenHang.PropertyChanged -= Header_ChuyenHang_PropertyChanged;
            Header_DonHang.PropertyChanged -= Header_DonHang_PropertyChanged;
        }

        void Header_ChuyenHang_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            MainFilter.SetFilter(Filter_tChuyenHangDonHang.TenChuyenHang, Header_ChuyenHang.Text);

            OnHeaderFilterChanged();
        }

        void Header_DonHang_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            MainFilter.SetFilter(Filter_tChuyenHangDonHang.TenDonHang, Header_DonHang.Text);

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
