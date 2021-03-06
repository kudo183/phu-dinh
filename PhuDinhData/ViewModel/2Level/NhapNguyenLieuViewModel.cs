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
    public class NhapNguyenLieuViewModel : BaseViewModel<tNhapNguyenLieu>
    {
        private List<rNguyenLieu> _rNguyenLieus;
        private List<rNhaCungCap> _rNhaCungCaps;

        public HeaderTextFilterModel Header_NguyenLieu { get; set; }
        public HeaderTextFilterModel Header_NhaCungCap { get; set; }

        public HeaderDateFilterModel Header_Ngay { get; set; }

        public NhapNguyenLieuViewModel()
        {
            Entity = new ObservableCollection<tNhapNguyenLieu>();

            MainFilter = new Filter_tNhapNguyenLieu();

            Header_NguyenLieu = new HeaderTextFilterModel(Constant.ColumnName_NguyenLieu);
            Header_NhaCungCap = new HeaderTextFilterModel(Constant.ColumnName_NhanCungCap);

            Header_Ngay = new HeaderDateFilterModel(Constant.ColumnName_Ngay);
        }

        public override void Load()
        {
            _isLoading = true;

            Entity.CollectionChanged += Entity_CollectionChanged;

            Header_Ngay.PropertyChanged += Header_Ngay_PropertyChanged;
            Header_NguyenLieu.PropertyChanged += Header_NguyenLieu_PropertyChanged;
            Header_NhaCungCap.PropertyChanged += Header_NhaCungCap_PropertyChanged;

            Header_Ngay_PropertyChanged(null, null);
            Header_NguyenLieu_PropertyChanged(null, null);
            Header_NhaCungCap_PropertyChanged(null, null);

            _isLoading = false;
        }

        public override void Unload()
        {
            Entity.CollectionChanged -= Entity_CollectionChanged;

            Header_Ngay.PropertyChanged -= Header_Ngay_PropertyChanged;
            Header_NguyenLieu.PropertyChanged -= Header_NguyenLieu_PropertyChanged;
            Header_NhaCungCap.PropertyChanged -= Header_NhaCungCap_PropertyChanged;
        }

        void Header_NguyenLieu_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            MainFilter.SetFilter(Filter_tNhapNguyenLieu.TenNguyenLieu, Header_NguyenLieu.GetFilterValue());

            OnHeaderFilterChanged();
        }

        void Header_NhaCungCap_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            MainFilter.SetFilter(Filter_tNhapNguyenLieu.TenNhaCungCap, Header_NhaCungCap.GetFilterValue());

            OnHeaderFilterChanged();
        }

        void Header_Ngay_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            MainFilter.SetFilter(Filter_tNhapNguyenLieu.Ngay, Header_Ngay.GetFilterValue());

            OnHeaderFilterChanged();
        }

        void Entity_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var tNhapNguyenLieu = e.NewItems[0] as tNhapNguyenLieu;
                tNhapNguyenLieu.Ngay = (Header_Ngay.IsUsed) ? Header_Ngay.Date : DateTime.Now;
                tNhapNguyenLieu.rNguyenLieuList = _rNguyenLieus;
                tNhapNguyenLieu.rNhaCungCapList = _rNhaCungCaps;

                if (GetDefaultValue(Constant.ColumnName_MaNguyenLieu) != null)
                {
                    tNhapNguyenLieu.MaNguyenLieu = Convert.ToInt32(GetDefaultValue(Constant.ColumnName_MaNguyenLieu));
                }

                if (GetDefaultValue(Constant.ColumnName_MaNhaCungCap) != null)
                {
                    tNhapNguyenLieu.MaNhaCungCap = Convert.ToInt32(GetDefaultValue(Constant.ColumnName_MaNhaCungCap));
                }
            }
        }

        private void UpdateNguyenLieuReferenceData()
        {
            UpdateReferenceData(out _rNguyenLieus
                , GetReferenceFilter<rNguyenLieu>(Constant.ColumnName_NguyenLieu)
                , (p => p.rNguyenLieuList = _rNguyenLieus));
        }

        private void UpdateNhaCungCapReferenceData()
        {
            UpdateReferenceData(out _rNhaCungCaps
                , GetReferenceFilter<rNhaCungCap>(Constant.ColumnName_NhanCungCap)
                , (p => p.rNhaCungCapList = _rNhaCungCaps));
        }

        protected override void UpdateAllReferenceData()
        {
            UpdateNguyenLieuReferenceData();
            UpdateNhaCungCapReferenceData();
        }

        public override void UpdateReferenceData(string columnName)
        {
            switch (columnName)
            {
                case Constant.ColumnName_NguyenLieu:
                    UpdateNguyenLieuReferenceData();
                    break;
                case Constant.ColumnName_NhanCungCap:
                    UpdateNhaCungCapReferenceData();
                    break;
            }
        }
    }
}
