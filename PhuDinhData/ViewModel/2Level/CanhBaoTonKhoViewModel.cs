using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using PhuDinhData.Filter;
using PhuDinhData.ViewModel.DataGridColumnHeaderFilterModel;
using System;
using PhuDinhCommon;

namespace PhuDinhData.ViewModel
{
    public class CanhBaoTonKhoViewModel : BaseViewModel<rCanhBaoTonKho>
    {
        private List<tMatHang> _tMatHangs;
        private List<rKhoHang> _rKhoHangs;

        public HeaderTextFilterModel Header_MatHang { get; set; }
        public HeaderTextFilterModel Header_KhoHang { get; set; }

        public CanhBaoTonKhoViewModel()
        {
            Entity = new ObservableCollection<rCanhBaoTonKho>();

            MainFilter = new Filter_rCanhBaoTonKho();

            SetReferenceFilter<tMatHang>(Constant.ColumnName_MatHang, (p => true));
            SetReferenceFilter<rKhoHang>(Constant.ColumnName_KhoHang, (p => true));

            Header_MatHang = new HeaderTextFilterModel(Constant.ColumnName_MatHang);
            Header_KhoHang = new HeaderTextFilterModel(Constant.ColumnName_KhoHang);
        }

        public override void Load()
        {
            _isLoading = true;

            Entity.CollectionChanged += Entity_CollectionChanged;

            Header_MatHang.PropertyChanged += Header_MatHang_PropertyChanged;
            Header_KhoHang.PropertyChanged += Header_KhoHang_PropertyChanged;

            Header_MatHang_PropertyChanged(null, null);
            Header_KhoHang_PropertyChanged(null, null);

            _isLoading = false;
        }

        public override void Unload()
        {
            Entity.CollectionChanged -= Entity_CollectionChanged;

            Header_MatHang.PropertyChanged -= Header_MatHang_PropertyChanged;
            Header_KhoHang.PropertyChanged -= Header_KhoHang_PropertyChanged;
        }

        void Header_MatHang_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            MainFilter.SetFilter(Filter_rCanhBaoTonKho.TenMatHang, Header_MatHang.Text);

            OnHeaderFilterChanged();
        }

        void Header_KhoHang_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            MainFilter.SetFilter(Filter_rCanhBaoTonKho.TenKhoHang, Header_KhoHang.Text);

            OnHeaderFilterChanged();
        }

        void Entity_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var rCanhBaoTonKho = e.NewItems[0] as rCanhBaoTonKho;

                rCanhBaoTonKho.tMatHangList = _tMatHangs;
                rCanhBaoTonKho.rKhoHangList = _rKhoHangs;

                if (GetDefaultValue(Constant.ColumnName_MaMatHang) != null)
                {
                    rCanhBaoTonKho.MaMatHang = Convert.ToInt32(GetDefaultValue(Constant.ColumnName_MaMatHang));
                }

                if (GetDefaultValue(Constant.ColumnName_MaKhoHang) != null)
                {
                    rCanhBaoTonKho.MaKhoHang = Convert.ToInt32(GetDefaultValue(Constant.ColumnName_MaKhoHang));
                }
            }
        }

        private void UpdateMatHangReferenceData()
        {
            UpdateReferenceData(out _tMatHangs
                , GetReferenceFilter<tMatHang>(Constant.ColumnName_MatHang)
                , (p => p.tMatHangList = _tMatHangs));
        }

        private void UpdateKhoHangReferenceData()
        {
            UpdateReferenceData(out _rKhoHangs
                , GetReferenceFilter<rKhoHang>(Constant.ColumnName_KhoHang)
                , (p => p.rKhoHangList = _rKhoHangs));
        }

        protected override void UpdateAllReferenceData()
        {
            UpdateMatHangReferenceData();
            UpdateKhoHangReferenceData();
        }

        public override void UpdateReferenceData(string columnName)
        {
            switch (columnName)
            {
                case Constant.ColumnName_MatHang:
                    UpdateMatHangReferenceData();
                    break;
                case Constant.ColumnName_KhoHang:
                    UpdateKhoHangReferenceData();
                    break;
            }
        }
    }
}
