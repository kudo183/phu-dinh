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
    public class PhuThuKhachHangViewModel : BaseViewModel<tPhuThuKhachHang>
    {
        private List<rKhachHang> _rKhachHangs;

        public HeaderTextFilterModel Header_KhachHang { get; set; }

        public HeaderDateFilterModel Header_Ngay { get; set; }

        public PhuThuKhachHangViewModel()
        {
            Entity = new ObservableCollection<tPhuThuKhachHang>();

            MainFilter = new Filter_tPhuThuKhachHang();

            Header_KhachHang = new HeaderTextFilterModel(Constant.ColumnName_KhachHang);

            Header_Ngay = new HeaderDateFilterModel(Constant.ColumnName_Ngay);
        }

        public override void Load()
        {
            _isLoading = true;

            Entity.CollectionChanged += Entity_CollectionChanged;

            Header_Ngay.PropertyChanged += Header_Ngay_PropertyChanged;

            Header_KhachHang.PropertyChanged += Header_KhachHang_PropertyChanged;

            Header_Ngay_PropertyChanged(null, null);
            Header_KhachHang_PropertyChanged(null, null);

            _isLoading = false;
        }

        public override void Unload()
        {
            Entity.CollectionChanged -= Entity_CollectionChanged;

            Header_Ngay.PropertyChanged -= Header_Ngay_PropertyChanged;
            Header_KhachHang.PropertyChanged -= Header_KhachHang_PropertyChanged;
        }

        void Header_Ngay_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            MainFilter.SetFilter(Filter_tPhuThuKhachHang.Ngay, Header_Ngay.GetFilterValue());

            OnHeaderFilterChanged();
        }

        void Header_KhachHang_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            MainFilter.SetFilter(Filter_tPhuThuKhachHang.TenKhachHang, Header_KhachHang.GetFilterValue());

            OnHeaderFilterChanged();
        }

        void Entity_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var tPhuThuKhachHang = e.NewItems[0] as tPhuThuKhachHang;

                tPhuThuKhachHang.Ngay = (Header_Ngay.IsUsed)
                    ? Header_Ngay.Date : DateTime.Now;

                tPhuThuKhachHang.rKhachHangList = _rKhachHangs;

                if (GetDefaultValue(Constant.ColumnName_MaKhachHang) != null)
                {
                    tPhuThuKhachHang.MaKhachHang = Convert.ToInt32(GetDefaultValue(Constant.ColumnName_MaKhachHang));
                }
            }
        }

        private void UpdateKhachHangReferenceData()
        {
            UpdateReferenceData(out _rKhachHangs
                , GetReferenceFilter<rKhachHang>(Constant.ColumnName_KhachHang)
                , (p => p.rKhachHangList = _rKhachHangs));
        }

        protected override void UpdateAllReferenceData()
        {
            UpdateKhachHangReferenceData();
        }

        public override void UpdateReferenceData(string columnName)
        {
            switch (columnName)
            {
                case Constant.ColumnName_KhachHang:
                    UpdateKhachHangReferenceData();
                    break;
            }
        }
    }
}
