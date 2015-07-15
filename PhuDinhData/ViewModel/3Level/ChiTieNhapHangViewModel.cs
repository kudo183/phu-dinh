using PhuDinhDataEntity;
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
    public class ChiTietNhapHangViewModel : BaseViewModel<tChiTietNhapHang>
    {
        private List<tNhapHang> _tNhapHangs;
        private List<tMatHang> _tMatHangs;

        public HeaderTextFilterModel Header_MatHang { get; set; }
        public HeaderTextFilterModel Header_NhapHang { get; set; }

        public ChiTietNhapHangViewModel()
        {
            Entity = new ObservableCollection<tChiTietNhapHang>();

            MainFilter = new Filter_tChiTietNhapHang();

            Header_MatHang = new HeaderTextFilterModel(Constant.ColumnName_MatHang);
            Header_NhapHang = new HeaderTextFilterModel(Constant.ColumnName_NhapHang);
        }

        public override void Load()
        {
            _isLoading = true;

            Entity.CollectionChanged += Entity_CollectionChanged;

            Header_MatHang.PropertyChanged += Header_MatHang_PropertyChanged;
            Header_NhapHang.PropertyChanged += Header_NhapHang_PropertyChanged;

            Header_NhapHang_PropertyChanged(null, null);
            Header_MatHang_PropertyChanged(null, null);

            _isLoading = false;
        }

        public override void Unload()
        {
            Entity.CollectionChanged -= Entity_CollectionChanged;

            Header_MatHang.PropertyChanged -= Header_MatHang_PropertyChanged;
            Header_NhapHang.PropertyChanged -= Header_NhapHang_PropertyChanged;
        }

        void Header_MatHang_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            MainFilter.SetFilter(Filter_tChiTietNhapHang.TenMatHang, Header_MatHang.Text);

            OnHeaderFilterChanged();
        }

        void Header_NhapHang_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            MainFilter.SetFilter(Filter_tChiTietNhapHang.TenNhapHang, Header_NhapHang.Text);

            OnHeaderFilterChanged();
        }

        void Entity_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var tChiTietNhapHang = e.NewItems[0] as tChiTietNhapHang;
                tChiTietNhapHang.tNhapHangList = _tNhapHangs;
                tChiTietNhapHang.tMatHangList = _tMatHangs;

                if (GetDefaultValue(Constant.ColumnName_MaNhapHang) != null)
                {
                    tChiTietNhapHang.MaNhapHang =
                        Convert.ToInt32(GetDefaultValue(Constant.ColumnName_MaNhapHang));
                }
            }
        }

        private void UpdateMatHangReferenceData()
        {
            UpdateReferenceData(out _tMatHangs
                , GetReferenceFilter<tMatHang>(Constant.ColumnName_MatHang)
                , (p => p.tMatHangList = _tMatHangs));
        }

        protected override void UpdateAllReferenceData()
        {
            UpdateMatHangReferenceData();
        }

        public override void UpdateReferenceData(string columnName)
        {
            switch (columnName)
            {
                case Constant.ColumnName_MatHang:
                    UpdateMatHangReferenceData();
                    break;
            }
        }
    }
}
