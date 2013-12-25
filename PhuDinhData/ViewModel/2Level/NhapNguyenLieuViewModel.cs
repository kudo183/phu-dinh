﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using PhuDinhData.Filter;
using PhuDinhData.ViewModel.DataGridColumnHeaderFilterModel;
using System;
using PhuDinhCommon;

namespace PhuDinhData.ViewModel
{
    public class NhapNguyenLieuViewModel : BaseViewModel<tNhapNguyenLieu>
    {
        private List<rNguyenLieu> _rNguyenLieus;
        private List<rNhaCungCap> _rNhaCungCaps;

        private bool _isUsedDateFilter = true;
        private DateTime _filterDate = DateTime.Now.Date;

        private string _filterLoaiChiPhi = string.Empty;
        private string _filterNhanVien = string.Empty;

        public static HeaderDateFilterModel Header_Ngay = new HeaderDateFilterModel(Constant.ColumnName_Ngay);
        public static HeaderTextFilterModel Header_NguyenLieu = new HeaderTextFilterModel(Constant.ColumnName_NguyenLieu);
        public static HeaderTextFilterModel Header_NhaCungCap = new HeaderTextFilterModel(Constant.ColumnName_NhanCungCap);

        public NhapNguyenLieuViewModel()
        {
            Entity = new ObservableCollection<tNhapNguyenLieu>();

            MainFilter = new Filter_tNhapNguyenLieu();

            SetReferenceFilter<rNguyenLieu>(Constant.ColumnName_NguyenLieu, (p => true));
            SetReferenceFilter<rNhaCungCap>(Constant.ColumnName_NhanCungCap, (p => true));
        }

        public override void Load()
        {
            _isLoading = true;

            Entity.CollectionChanged += Entity_CollectionChanged;
            Header_Ngay.PropertyChanged += Header_Ngay_PropertyChanged;
            Header_NguyenLieu.PropertyChanged += Header_NguyenLieu_PropertyChanged;
            Header_NhaCungCap.PropertyChanged += Header_NhaCungCap_PropertyChanged;

            Header_Ngay.IsUsed = _isUsedDateFilter;
            Header_Ngay.Date = _filterDate;

            Header_NguyenLieu.Text = _filterLoaiChiPhi;

            Header_NhaCungCap.Text = _filterNhanVien;

            _isLoading = false;
        }

        public override void Unload()
        {
            Entity.CollectionChanged -= Entity_CollectionChanged;

            Header_Ngay.PropertyChanged -= Header_Ngay_PropertyChanged;
            Header_NguyenLieu.PropertyChanged -= Header_NguyenLieu_PropertyChanged;
            Header_NhaCungCap.PropertyChanged -= Header_NhaCungCap_PropertyChanged;

            _filterDate = Header_Ngay.Date;
            _isUsedDateFilter = Header_Ngay.IsUsed;

            _filterLoaiChiPhi = Header_NguyenLieu.Text;

            _filterNhanVien = Header_NhaCungCap.Text;
        }

        void Header_NguyenLieu_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            MainFilter.SetFilter(Filter_tNhapNguyenLieu.TenNguyenLieu, Header_NguyenLieu.Text);

            OnHeaderFilterChanged();
        }

        void Header_NhaCungCap_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            MainFilter.SetFilter(Filter_tNhapNguyenLieu.TenNhaCungCap, Header_NhaCungCap.Text);

            OnHeaderFilterChanged();
        }

        void Header_Ngay_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (Header_Ngay.IsUsed)
            {
                MainFilter.SetFilter(Filter_tNhapNguyenLieu.Ngay, Header_Ngay.Date);
            }
            else
            {
                MainFilter.SetFilter(Filter_tNhapNguyenLieu.Ngay, null);
            }

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