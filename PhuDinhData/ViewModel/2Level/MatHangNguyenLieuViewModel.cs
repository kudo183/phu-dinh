﻿using PhuDinhDataEntity;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using PhuDinhData.Filter;
using System;
using PhuDinhCommon;
using CustomControl.DataGridColumnHeaderFilterModel;

namespace PhuDinhData.ViewModel
{
    public class MatHangNguyenLieuViewModel : BaseViewModel<rMatHangNguyenLieu>
    {
        private List<tMatHang> _tMatHangs;
        private List<rNguyenLieu> _rNguyenLieus;

        public HeaderTextFilterModel Header_MatHang { get; set; }
        public HeaderTextFilterModel Header_NguyenLieu { get; set; }

        public MatHangNguyenLieuViewModel()
        {
            Entity = new ObservableCollection<rMatHangNguyenLieu>();

            MainFilter = new Filter_rMatHangNguyenLieu();

            Header_MatHang = new HeaderTextFilterModel(Constant.ColumnName_MatHang);
            Header_NguyenLieu = new HeaderTextFilterModel(Constant.ColumnName_NguyenLieu);
        }

        public override void Load()
        {
            _isLoading = true;

            Entity.CollectionChanged += Entity_CollectionChanged;

            Header_MatHang.PropertyChanged += Header_MatHang_PropertyChanged;
            Header_NguyenLieu.PropertyChanged += Header_NguyenLieu_PropertyChanged;

            Header_MatHang_PropertyChanged(null, null);
            Header_NguyenLieu_PropertyChanged(null, null);

            _isLoading = false;
        }

        public override void Unload()
        {
            Entity.CollectionChanged -= Entity_CollectionChanged;

            Header_MatHang.PropertyChanged -= Header_MatHang_PropertyChanged;
            Header_NguyenLieu.PropertyChanged -= Header_NguyenLieu_PropertyChanged;
        }

        void Header_MatHang_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            MainFilter.SetFilter(Filter_rMatHangNguyenLieu.TenMatHang, Header_MatHang.GetFilterValue());

            OnHeaderFilterChanged();
        }

        void Header_NguyenLieu_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            MainFilter.SetFilter(Filter_rMatHangNguyenLieu.TenNguyenLieu, Header_NguyenLieu.GetFilterValue());

            OnHeaderFilterChanged();
        }

        void Entity_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var rMatHangNguyenLieu = e.NewItems[0] as rMatHangNguyenLieu;
                rMatHangNguyenLieu.tMatHangList = _tMatHangs;
                rMatHangNguyenLieu.rNguyenLieuList = _rNguyenLieus;

                if (GetDefaultValue(Constant.ColumnName_MaMatHang) != null)
                {
                    rMatHangNguyenLieu.MaMatHang = Convert.ToInt32(GetDefaultValue(Constant.ColumnName_MaMatHang));
                }

                if (GetDefaultValue(Constant.ColumnName_MaNguyenLieu) != null)
                {
                    rMatHangNguyenLieu.MaNguyenLieu = Convert.ToInt32(GetDefaultValue(Constant.ColumnName_MaNguyenLieu));
                }
            }
        }

        private void UpdateMatHangReferenceData()
        {
            UpdateReferenceData(out _tMatHangs
                , GetReferenceFilter<tMatHang>(Constant.ColumnName_MatHang)
                , (p => p.tMatHangList = _tMatHangs));
        }

        private void UpdateNguyenLieuReferenceData()
        {
            UpdateReferenceData(out _rNguyenLieus
                , GetReferenceFilter<rNguyenLieu>(Constant.ColumnName_NguyenLieu)
                , (p => p.rNguyenLieuList = _rNguyenLieus));
        }

        protected override void UpdateAllReferenceData()
        {
            UpdateMatHangReferenceData();
            UpdateNguyenLieuReferenceData();
        }

        public override void UpdateReferenceData(string columnName)
        {
            switch (columnName)
            {
                case Constant.ColumnName_MatHang:
                    UpdateMatHangReferenceData();
                    break;
                case Constant.ColumnName_NguyenLieu:
                    UpdateNguyenLieuReferenceData();
                    break;
            }
        }
    }
}
