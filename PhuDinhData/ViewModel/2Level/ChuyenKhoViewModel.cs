﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using PhuDinhData.Filter;
using PhuDinhData.ViewModel.DataGridColumnHeaderFilterModel;
using System;
using PhuDinhCommon;

namespace PhuDinhData.ViewModel
{
    public class ChuyenKhoViewModel : BaseViewModel<tChuyenKho>
    {
        private List<rNhanVien> _rNhanViens;
        private List<rKhoHang> _rKhoHangs;

        private bool _isUsedDateFilter = true;
        private DateTime _filterDate = DateTime.Now.Date;

        private string _filterNhanVien = string.Empty;
        private string _filterKhoHangNhap = string.Empty;
        private string _filterKhoHangXuat = string.Empty;

        public static HeaderTextFilterModel Header_NhanVien = new HeaderTextFilterModel(Constant.ColumnName_NhanVien);
        public static HeaderTextFilterModel Header_KhoHangNhap = new HeaderTextFilterModel(Constant.ColumnName_KhoHangNhap);
        public static HeaderTextFilterModel Header_KhoHangXuat = new HeaderTextFilterModel(Constant.ColumnName_KhoHangXuat);

        private HeaderDateFilterModel _header_Ngay = new HeaderDateFilterModel(Constant.ColumnName_Ngay);
        public HeaderDateFilterModel Header_Ngay
        {
            get { return _header_Ngay; }
            set { _header_Ngay = value; }
        }

        public ChuyenKhoViewModel()
        {
            Entity = new ObservableCollection<tChuyenKho>();

            MainFilter = new Filter_tChuyenKho();

            SetReferenceFilter<rNhanVien>(Constant.ColumnName_NhanVien, (p => true));
            SetReferenceFilter<rNhaCungCap>(Constant.ColumnName_NhanCungCap, (p => true));
            SetReferenceFilter<rKhoHang>(Constant.ColumnName_KhoHangNhap, (p => true));
            SetReferenceFilter<rKhoHang>(Constant.ColumnName_KhoHangXuat, (p => true));
        }

        public override void Load()
        {
            _isLoading = true;

            Entity.CollectionChanged += Entity_CollectionChanged;

            Header_Ngay.IsUsed = _isUsedDateFilter;
            Header_Ngay.Date = _filterDate;
            Header_Ngay.PropertyChanged += Header_Ngay_PropertyChanged;

            Header_NhanVien.Text = _filterNhanVien;
            Header_NhanVien.PropertyChanged += Header_NhanVien_PropertyChanged;

            Header_KhoHangNhap.Text = _filterKhoHangNhap;
            Header_KhoHangNhap.PropertyChanged += Header_KhoHangNhap_PropertyChanged;

            Header_KhoHangXuat.Text = _filterKhoHangXuat;
            Header_KhoHangXuat.PropertyChanged += Header_KhoHangXuat_PropertyChanged;

            Header_Ngay_PropertyChanged(null, null);
            Header_NhanVien_PropertyChanged(null, null);
            Header_KhoHangNhap_PropertyChanged(null, null);
            Header_KhoHangXuat_PropertyChanged(null, null);

            _isLoading = false;
        }

        public override void Unload()
        {
            Entity.CollectionChanged -= Entity_CollectionChanged;

            Header_Ngay.PropertyChanged -= Header_Ngay_PropertyChanged;
            Header_NhanVien.PropertyChanged -= Header_NhanVien_PropertyChanged;
            Header_KhoHangNhap.PropertyChanged -= Header_KhoHangNhap_PropertyChanged;

            _filterDate = Header_Ngay.Date;
            _isUsedDateFilter = Header_Ngay.IsUsed;

            _filterNhanVien = Header_NhanVien.Text;

            _filterKhoHangNhap = Header_KhoHangNhap.Text;

            _filterKhoHangXuat = Header_KhoHangXuat.Text;
        }

        void Header_NhanVien_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            MainFilter.SetFilter(Filter_tChuyenKho.TenNhanVien, Header_NhanVien.Text);

            OnHeaderFilterChanged();
        }

        void Header_KhoHangNhap_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            MainFilter.SetFilter(Filter_tChuyenKho.TenKhoHangNhap, Header_KhoHangNhap.Text);

            OnHeaderFilterChanged();
        }

        void Header_KhoHangXuat_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            MainFilter.SetFilter(Filter_tChuyenKho.TenKhoHangXuat, Header_KhoHangXuat.Text);

            OnHeaderFilterChanged();
        }

        void Header_Ngay_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (Header_Ngay.IsUsed)
            {
                MainFilter.SetFilter(Filter_tChuyenKho.Ngay, Header_Ngay.Date);
            }
            else
            {
                MainFilter.SetFilter(Filter_tChuyenKho.Ngay, null);
            }

            OnHeaderFilterChanged();
        }

        void Entity_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var tChuyenKho = e.NewItems[0] as tChuyenKho;
                tChuyenKho.Ngay = (Header_Ngay.IsUsed) ? Header_Ngay.Date : DateTime.Now;
                tChuyenKho.rNhanVienList = _rNhanViens;
                tChuyenKho.rKhoHangList = _rKhoHangs;

                if (GetDefaultValue(Constant.ColumnName_MaNhanVien) != null)
                {
                    tChuyenKho.MaNhanVien = Convert.ToInt32(GetDefaultValue(Constant.ColumnName_MaNhanVien));
                }

                if (GetDefaultValue(Constant.ColumnName_MaKhoHangNhap) != null)
                {
                    tChuyenKho.MaKhoHangNhap = Convert.ToInt32(GetDefaultValue(Constant.ColumnName_MaKhoHangNhap));
                }

                if (GetDefaultValue(Constant.ColumnName_MaKhoHangXuat) != null)
                {
                    tChuyenKho.MaKhoHangXuat = Convert.ToInt32(GetDefaultValue(Constant.ColumnName_MaKhoHangXuat));
                }
            }
        }

        private void UpdateNhanVienReferenceData()
        {
            UpdateReferenceData(out _rNhanViens
                , GetReferenceFilter<rNhanVien>(Constant.ColumnName_NhanVien)
                , (p => p.rNhanVienList = _rNhanViens));
        }

        private void UpdateKhoHangNhapReferenceData()
        {
            UpdateReferenceData(out _rKhoHangs
                , GetReferenceFilter<rKhoHang>(Constant.ColumnName_KhoHangNhap)
                , (p => p.rKhoHangList = _rKhoHangs));
        }

        private void UpdateKhoHangXuatReferenceData()
        {
            UpdateReferenceData(out _rKhoHangs
                , GetReferenceFilter<rKhoHang>(Constant.ColumnName_KhoHangXuat)
                , (p => p.rKhoHangList = _rKhoHangs));
        }

        protected override void UpdateAllReferenceData()
        {
            UpdateNhanVienReferenceData();
            UpdateKhoHangNhapReferenceData();
            UpdateKhoHangXuatReferenceData();
        }

        public override void UpdateReferenceData(string columnName)
        {
            switch (columnName)
            {
                case Constant.ColumnName_NhanVien:
                    UpdateNhanVienReferenceData();
                    break;
                case Constant.ColumnName_KhoHangNhap:
                    UpdateKhoHangNhapReferenceData();
                    break;
                case Constant.ColumnName_KhoHangXuat:
                    UpdateKhoHangXuatReferenceData();
                    break;
            }
        }
    }
}
