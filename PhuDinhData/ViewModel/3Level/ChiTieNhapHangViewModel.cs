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
    public class ChiTietNhapHangViewModel : BaseViewModel<tChiTietNhapHang>
    {
        private List<tNhapHang> _tNhapHangs;
        private List<tMatHang> _tMatHangs;

        private string _filterMatHang = string.Empty;

        private string _filterNhapHang = string.Empty;

        public static HeaderTextFilterModel Header_MatHang = new HeaderTextFilterModel(Constant.ColumnName_MatHang);
        public static HeaderTextFilterModel Header_NhapHang = new HeaderTextFilterModel(Constant.ColumnName_NhapHang);

        public ChiTietNhapHangViewModel()
        {
            Entity = new ObservableCollection<tChiTietNhapHang>();

            MainFilter = new Filter_tChiTietNhapHang();

            SetReferenceFilter<tMatHang>(Constant.ColumnName_MatHang, (p => true));
            SetReferenceFilter<tNhapHang>(Constant.ColumnName_NhapHang, (p => true));
        }

        public override void Load()
        {
            _isLoading = true;

            Entity.CollectionChanged += Entity_CollectionChanged;

            Header_MatHang.Text = _filterMatHang;
            Header_MatHang.PropertyChanged += Header_MatHang_PropertyChanged;

            Header_NhapHang.Text = _filterNhapHang;
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

            _filterMatHang = Header_MatHang.Text;

            _filterNhapHang = Header_NhapHang.Text;
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

        private void UpdateNhapHangReferenceData()
        {
            UpdateReferenceData(out _tNhapHangs
                , GetReferenceFilter<tNhapHang>(Constant.ColumnName_NhapHang)
                , (p => p.tNhapHangList = _tNhapHangs));
        }

        protected override void UpdateAllReferenceData()
        {
            UpdateMatHangReferenceData();
            UpdateNhapHangReferenceData();
        }

        public override void UpdateReferenceData(string columnName)
        {
            switch (columnName)
            {
                case Constant.ColumnName_MatHang:
                    UpdateMatHangReferenceData();
                    break;
                case Constant.ColumnName_NhapHang:
                    UpdateNhapHangReferenceData();
                    break;
            }
        }
    }
}
