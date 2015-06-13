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
    public class ChiTietToaHangViewModel : BaseViewModel<tChiTietToaHang>
    {
        private List<tChiTietDonHang> _tChiTietDonHangs;

        public HeaderTextFilterModel Header_ToaHang { get; set; }
        public HeaderTextFilterModel Header_ChiTietDonHang { get; set; }

        public ChiTietToaHangViewModel()
        {
            Entity = new ObservableCollection<tChiTietToaHang>();

            MainFilter = new Filter_tChiTietToaHang();

            SetReferenceFilter<tToaHang>(Constant.ColumnName_ToaHang, (p => true));
            SetReferenceFilter<tChiTietDonHang>(Constant.ColumnName_ChiTietDonHang, (p => true));

            Header_ToaHang = new HeaderTextFilterModel(Constant.ColumnName_ToaHang);
            Header_ChiTietDonHang = new HeaderTextFilterModel(Constant.ColumnName_ChiTietDonHang);
        }

        public override void Load()
        {
            _isLoading = true;

            Entity.CollectionChanged += Entity_CollectionChanged;

            Header_ToaHang.PropertyChanged += Header_ToaHang_PropertyChanged;
            Header_ChiTietDonHang.PropertyChanged += Header_ChiTietDonHang_PropertyChanged;

            Header_ChiTietDonHang_PropertyChanged(null, null);
            Header_ToaHang_PropertyChanged(null, null);

            _isLoading = false;
        }

        public override void Unload()
        {
            Entity.CollectionChanged -= Entity_CollectionChanged;

            Header_ToaHang.PropertyChanged -= Header_ToaHang_PropertyChanged;
            Header_ChiTietDonHang.PropertyChanged -= Header_ChiTietDonHang_PropertyChanged;
        }

        void Header_ToaHang_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            MainFilter.SetFilter(Filter_tChiTietToaHang.TenToaHang, Header_ToaHang.Text);

            OnHeaderFilterChanged();
        }

        void Header_ChiTietDonHang_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            MainFilter.SetFilter(Filter_tChiTietToaHang.TenChiTietDonHang, Header_ChiTietDonHang.Text);

            OnHeaderFilterChanged();
        }

        void Entity_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var tChiTietToaHang = e.NewItems[0] as tChiTietToaHang;

                tChiTietToaHang.tChiTietDonHangList = _tChiTietDonHangs;

                if (GetDefaultValue(Constant.ColumnName_MaToaHang) != null)
                {
                    tChiTietToaHang.MaToaHang = Convert.ToInt32(GetDefaultValue(Constant.ColumnName_MaToaHang));
                }
            }
        }

        private void UpdateChiTietDonHangReferenceData()
        {
            UpdateReferenceData(out _tChiTietDonHangs
                , GetReferenceFilter<tChiTietDonHang>(Constant.ColumnName_ChiTietDonHang)
                , (p => p.tChiTietDonHangList = _tChiTietDonHangs));
        }

        protected override void UpdateAllReferenceData()
        {
            UpdateChiTietDonHangReferenceData();
        }

        public override void UpdateReferenceData(string columnName)
        {
            switch (columnName)
            {
                case Constant.ColumnName_ChiTietDonHang:
                    UpdateChiTietDonHangReferenceData();
                    break;
            }
        }
    }
}
