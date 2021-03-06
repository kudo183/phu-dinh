﻿using System.Linq;
using PhuDinhDataEntity;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using PhuDinhData.Filter;
using System;
using PhuDinhCommon;
using CustomControl.DataGridColumnHeaderFilterModel;

namespace PhuDinhData.ViewModel
{
    public class TonKhoViewModel : BaseViewModel<tTonKho>
    {
        private List<tMatHang> _tMatHangs;
        private List<rKhoHang> _rKhoHangs;

        public HeaderTextFilterModel Header_MatHang { get; set; }
        public HeaderTextFilterModel Header_KhoHang { get; set; }

        public HeaderDateFilterModel Header_Ngay { get; set; }

        public TonKhoViewModel()
        {
            Entity = new ObservableCollection<tTonKho>();

            MainFilter = new Filter_tTonKho();

            Header_MatHang = new HeaderTextFilterModel(Constant.ColumnName_MatHang);
            Header_KhoHang = new HeaderTextFilterModel(Constant.ColumnName_KhoHang);

            Header_Ngay = new HeaderDateFilterModel(Constant.ColumnName_Ngay);
        }

        public override void Load()
        {
            _isLoading = true;

            Entity.CollectionChanged += Entity_CollectionChanged;

            Header_Ngay.PropertyChanged += Header_Ngay_PropertyChanged;
            Header_MatHang.PropertyChanged += Header_MatHang_PropertyChanged;
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
            Header_KhoHang.PropertyChanged -= Header_KhoHang_PropertyChanged;
        }

        void Header_MatHang_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            MainFilter.SetFilter(Filter_tTonKho.TenMatHang, Header_MatHang.GetFilterValue());

            OnHeaderFilterChanged();
        }

        void Header_KhoHang_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            MainFilter.SetFilter(Filter_tTonKho.TenKhoHang, Header_KhoHang.GetFilterValue());

            OnHeaderFilterChanged();
        }

        void Header_Ngay_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            MainFilter.SetFilter(Filter_tTonKho.Ngay, Header_Ngay.GetFilterValue());

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
            
            Message = string.Format("Tong cong: {0} cuon", Entity.Sum(p => p.SoLuong));
        }
    }
}
