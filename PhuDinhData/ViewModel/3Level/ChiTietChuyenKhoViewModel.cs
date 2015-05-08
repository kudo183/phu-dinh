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
    public class ChiTietChuyenKhoViewModel : BaseViewModel<tChiTietChuyenKho>
    {
        private List<tChuyenKho> _tChuyenKhos;
        private List<tMatHang> _tMatHangs;

        public HeaderTextFilterModel Header_MatHang { get; set; }
        public HeaderTextFilterModel Header_ChuyenKho { get; set; }

        public ChiTietChuyenKhoViewModel()
        {
            Entity = new ObservableCollection<tChiTietChuyenKho>();

            MainFilter = new Filter_tChiTietChuyenKho();

            SetReferenceFilter<tMatHang>(Constant.ColumnName_MatHang, (p => true));
            SetReferenceFilter<tChuyenKho>(Constant.ColumnName_ChuyenKho, (p => true));

            Header_MatHang = new HeaderTextFilterModel(Constant.ColumnName_MatHang);
            Header_ChuyenKho = new HeaderTextFilterModel(Constant.ColumnName_ChuyenKho);
        }

        public override void Load()
        {
            _isLoading = true;

            Entity.CollectionChanged += Entity_CollectionChanged;

            Header_MatHang.PropertyChanged += Header_MatHang_PropertyChanged;
            Header_ChuyenKho.PropertyChanged += Header_ChuyenKho_PropertyChanged;

            Header_ChuyenKho_PropertyChanged(null, null);
            Header_MatHang_PropertyChanged(null, null);

            _isLoading = false;
        }

        public override void Unload()
        {
            Entity.CollectionChanged -= Entity_CollectionChanged;

            Header_MatHang.PropertyChanged -= Header_MatHang_PropertyChanged;
            Header_ChuyenKho.PropertyChanged -= Header_ChuyenKho_PropertyChanged;
        }

        void Header_MatHang_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            MainFilter.SetFilter(Filter_tChiTietChuyenKho.TenMatHang, Header_MatHang.Text);

            OnHeaderFilterChanged();
        }

        void Header_ChuyenKho_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            MainFilter.SetFilter(Filter_tChiTietChuyenKho.TenChuyenKho, Header_ChuyenKho.Text);

            OnHeaderFilterChanged();
        }

        void Entity_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var tChiTietChuyenKho = e.NewItems[0] as tChiTietChuyenKho;
                tChiTietChuyenKho.tChuyenKhoList = _tChuyenKhos;
                tChiTietChuyenKho.tMatHangList = _tMatHangs;

                if (GetDefaultValue(Constant.ColumnName_MaChuyenKho) != null)
                {
                    tChiTietChuyenKho.MaChuyenKho =
                        Convert.ToInt32(GetDefaultValue(Constant.ColumnName_MaChuyenKho));
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
