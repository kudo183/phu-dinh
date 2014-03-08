using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using PhuDinhData.Filter;
using PhuDinhData.ViewModel.DataGridColumnHeaderFilterModel;
using System;
using PhuDinhCommon;

namespace PhuDinhData.ViewModel
{
    public class TonKhoViewModel : BaseViewModel<tTonKho>
    {
        private List<tMatHang> _tMatHangs;
        private List<rKhoHang> _rKhoHangs;

        private bool _isUsedDateFilter = true;
        private DateTime _filterDate = DateTime.Now.Date;

        private string _filterMatHang = string.Empty;
        private string _filterKhoHang = string.Empty;

        public static HeaderDateFilterModel Header_Ngay = new HeaderDateFilterModel(Constant.ColumnName_Ngay);
        public static HeaderTextFilterModel Header_MatHang = new HeaderTextFilterModel(Constant.ColumnName_MatHang);
        public static HeaderTextFilterModel Header_KhoHang = new HeaderTextFilterModel(Constant.ColumnName_KhoHang);

        public TonKhoViewModel()
        {
            Entity = new ObservableCollection<tTonKho>();

            MainFilter = new Filter_tTonKho();

            SetReferenceFilter<tMatHang>(Constant.ColumnName_MatHang, (p => true));
            SetReferenceFilter<rKhoHang>(Constant.ColumnName_KhoHang, (p => true));
        }

        public override void Load()
        {
            _isLoading = true;

            Entity.CollectionChanged += Entity_CollectionChanged;

            Header_Ngay.IsUsed = _isUsedDateFilter;
            Header_Ngay.Date = _filterDate;
            Header_Ngay.PropertyChanged += Header_Ngay_PropertyChanged;

            Header_MatHang.Text = _filterMatHang;
            Header_MatHang.PropertyChanged += Header_MatHang_PropertyChanged;

            Header_KhoHang.Text = _filterKhoHang;
            Header_KhoHang.PropertyChanged += Header_KhoHang_PropertyChanged;

            Header_Ngay_PropertyChanged(null, null);
            Header_MatHang_PropertyChanged(null, null);
            Header_KhoHang_PropertyChanged(null, null);

            _isLoading = false;
        }

        public override void Unload()
        {
            Entity.CollectionChanged -= Entity_CollectionChanged;

            Header_Ngay.PropertyChanged -= Header_Ngay_PropertyChanged;
            Header_MatHang.PropertyChanged -= Header_MatHang_PropertyChanged;

            _filterDate = Header_Ngay.Date;
            _isUsedDateFilter = Header_Ngay.IsUsed;

            _filterMatHang = Header_MatHang.Text;

            _filterKhoHang = Header_KhoHang.Text;
        }

        void Header_MatHang_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            MainFilter.SetFilter(Filter_tTonKho.TenMatHang, Header_MatHang.Text);

            OnHeaderFilterChanged();
        }

        void Header_KhoHang_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            MainFilter.SetFilter(Filter_tTonKho.TenKhoHang, Header_KhoHang.Text);

            OnHeaderFilterChanged();
        }

        void Header_Ngay_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (Header_Ngay.IsUsed)
            {
                MainFilter.SetFilter(Filter_tTonKho.Ngay, Header_Ngay.Date);
            }
            else
            {
                MainFilter.SetFilter(Filter_tTonKho.Ngay, null);
            }

            OnHeaderFilterChanged();
        }

        void Entity_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var tTonKho = e.NewItems[0] as tTonKho;
                tTonKho.Ngay = (Header_Ngay.IsUsed) ? Header_Ngay.Date : DateTime.Now;
                tTonKho.tMatHangList = _tMatHangs;
                tTonKho.rKhoHangList = _rKhoHangs;

                if (GetDefaultValue(Constant.ColumnName_MaMatHang) != null)
                {
                    tTonKho.MaMatHang = Convert.ToInt32(GetDefaultValue(Constant.ColumnName_MaMatHang));
                }

                if (GetDefaultValue(Constant.ColumnName_MaKhoHang) != null)
                {
                    tTonKho.MaKhoHang = Convert.ToInt32(GetDefaultValue(Constant.ColumnName_MaKhoHang));
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

        public override void RefreshData()
        {
            base.RefreshData();

            Message = string.Format("Tong cong: {0} cuon", Repository.tTonKhoRepository.SumSoLuong(_context, MainFilter.Filter));
        }
    }
}
