﻿using PhuDinhDataEntity;
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
    public class ChiTietChuyenHangDonHangViewModel : BaseViewModel<tChiTietChuyenHangDonHang>
    {
        private List<tChiTietDonHang> _tChiTietDonHangs;

        public HeaderTextFilterModel Header_ChuyenHangDonHang { get; set; }
        public HeaderTextFilterModel Header_ChiTietDonHang { get; set; }

        public ChiTietChuyenHangDonHangViewModel()
        {
            Entity = new ObservableCollection<tChiTietChuyenHangDonHang>();

            MainFilter = new Filter_tChiTietChuyenHangDonHang();

            Header_ChuyenHangDonHang = new HeaderTextFilterModel(Constant.ColumnName_ChuyenHangDonHang);
            Header_ChiTietDonHang = new HeaderTextFilterModel(Constant.ColumnName_ChiTietDonHang);
        }

        public override void Load()
        {
            _isLoading = true;

            Entity.CollectionChanged += Entity_CollectionChanged;

            Header_ChuyenHangDonHang.PropertyChanged += Header_ChuyenHangDonHang_PropertyChanged;
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
        }

        void Header_ChuyenHangDonHang_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            MainFilter.SetFilter(Filter_tChiTietChuyenHangDonHang.TenChuyenHangDonHang, Header_ChuyenHangDonHang.GetFilterValue());

            OnHeaderFilterChanged();
        }

        void Header_ChiTietDonHang_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            MainFilter.SetFilter(Filter_tChiTietChuyenHangDonHang.TenChiTietDonHang, Header_ChiTietDonHang.GetFilterValue());

            OnHeaderFilterChanged();
        }

        void Entity_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var tChiTietChuyenHangDonHang = e.NewItems[0] as tChiTietChuyenHangDonHang;

                tChiTietChuyenHangDonHang.tChiTietDonHangList = _tChiTietDonHangs;

                if (GetDefaultValue(Constant.ColumnName_MaChuyenHangDonHang) != null)
                {
                    tChiTietChuyenHangDonHang.MaChuyenHangDonHang = Convert.ToInt32(GetDefaultValue(Constant.ColumnName_MaChuyenHangDonHang));
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
