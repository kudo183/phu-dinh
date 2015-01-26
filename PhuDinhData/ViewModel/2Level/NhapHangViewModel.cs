using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using PhuDinhData.Filter;
using PhuDinhData.ViewModel.DataGridColumnHeaderFilterModel;
using System;
using PhuDinhCommon;

namespace PhuDinhData.ViewModel
{
    public class NhapHangViewModel : BaseViewModel<tNhapHang>
    {
        private List<rNhanVien> _rNhanViens;
        private List<rNhaCungCap> _rNhaCungCaps;
        private List<rKhoHang> _rKhoHangs;

        private bool _isUsedDateFilter = true;
        private DateTime _filterDate = DateTime.Now.Date;

        private string _filterNhanVien = string.Empty;
        private string _filterNhaCungCap = string.Empty;
        private string _filterKhoHang = string.Empty;

        public static HeaderTextFilterModel Header_NhanVien = new HeaderTextFilterModel(Constant.ColumnName_NhanVien);
        public static HeaderTextFilterModel Header_NhaCungCap = new HeaderTextFilterModel(Constant.ColumnName_NhanCungCap);
        public static HeaderTextFilterModel Header_KhoHang = new HeaderTextFilterModel(Constant.ColumnName_KhoHang);

        private HeaderDateFilterModel _header_Ngay = new HeaderDateFilterModel(Constant.ColumnName_Ngay);
        public HeaderDateFilterModel Header_Ngay
        {
            get { return _header_Ngay; }
            set { _header_Ngay = value; }
        }

        public NhapHangViewModel()
        {
            Entity = new ObservableCollection<tNhapHang>();

            MainFilter = new Filter_tNhapHang();

            SetReferenceFilter<rNhanVien>(Constant.ColumnName_NhanVien, (p => true));
            SetReferenceFilter<rNhaCungCap>(Constant.ColumnName_NhanCungCap, (p => true));
            SetReferenceFilter<rKhoHang>(Constant.ColumnName_KhoHang, (p => true));
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

            Header_NhaCungCap.Text = _filterNhaCungCap;
            Header_NhaCungCap.PropertyChanged += Header_NhaCungCap_PropertyChanged;

            Header_KhoHang.Text = _filterKhoHang;
            Header_KhoHang.PropertyChanged += Header_KhoHang_PropertyChanged;

            Header_Ngay_PropertyChanged(null, null);
            Header_NhanVien_PropertyChanged(null, null);
            Header_NhaCungCap_PropertyChanged(null, null);
            Header_KhoHang_PropertyChanged(null, null);

            _isLoading = false;
        }

        public override void Unload()
        {
            Entity.CollectionChanged -= Entity_CollectionChanged;

            Header_Ngay.PropertyChanged -= Header_Ngay_PropertyChanged;
            Header_NhanVien.PropertyChanged -= Header_NhanVien_PropertyChanged;
            Header_NhaCungCap.PropertyChanged -= Header_NhaCungCap_PropertyChanged;
            Header_KhoHang.PropertyChanged -= Header_KhoHang_PropertyChanged;

            _filterDate = Header_Ngay.Date;
            _isUsedDateFilter = Header_Ngay.IsUsed;

            _filterNhanVien = Header_NhanVien.Text;

            _filterNhaCungCap = Header_NhaCungCap.Text;

            _filterKhoHang = Header_KhoHang.Text;
        }

        void Header_NhanVien_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            MainFilter.SetFilter(Filter_tNhapHang.TenNhanVien, Header_NhanVien.Text);

            OnHeaderFilterChanged();
        }

        void Header_NhaCungCap_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            MainFilter.SetFilter(Filter_tNhapHang.TenNhaCungCap, Header_NhaCungCap.Text);

            OnHeaderFilterChanged();
        }

        void Header_KhoHang_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            MainFilter.SetFilter(Filter_tNhapHang.TenKhoHang, Header_KhoHang.Text);

            OnHeaderFilterChanged();
        }

        void Header_Ngay_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (Header_Ngay.IsUsed)
            {
                MainFilter.SetFilter(Filter_tNhapHang.Ngay, Header_Ngay.Date);
            }
            else
            {
                MainFilter.SetFilter(Filter_tNhapHang.Ngay, null);
            }

            OnHeaderFilterChanged();
        }

        void Entity_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var tNhapHang = e.NewItems[0] as tNhapHang;
                tNhapHang.Ngay = (Header_Ngay.IsUsed) ? Header_Ngay.Date : DateTime.Now;
                tNhapHang.rNhanVienList = _rNhanViens;
                tNhapHang.rNhaCungCapList = _rNhaCungCaps;
                tNhapHang.rKhoHangList = _rKhoHangs;

                if (GetDefaultValue(Constant.ColumnName_MaNhanVien) != null)
                {
                    tNhapHang.MaNhanVien = Convert.ToInt32(GetDefaultValue(Constant.ColumnName_MaNhanVien));
                }

                if (GetDefaultValue(Constant.ColumnName_MaNhaCungCap) != null)
                {
                    tNhapHang.MaNhaCungCap = Convert.ToInt32(GetDefaultValue(Constant.ColumnName_MaNhaCungCap));
                }

                if (GetDefaultValue(Constant.ColumnName_MaKhoHang) != null)
                {
                    tNhapHang.MaKhoHang = Convert.ToInt32(GetDefaultValue(Constant.ColumnName_MaKhoHang));
                }
            }
        }

        private void UpdateNhanVienReferenceData()
        {
            UpdateReferenceData(out _rNhanViens
                , GetReferenceFilter<rNhanVien>(Constant.ColumnName_NhanVien)
                , (p => p.rNhanVienList = _rNhanViens));
        }

        private void UpdateNhaCungCapReferenceData()
        {
            UpdateReferenceData(out _rNhaCungCaps
                , GetReferenceFilter<rNhaCungCap>(Constant.ColumnName_NhanCungCap)
                , (p => p.rNhaCungCapList = _rNhaCungCaps));
        }

        private void UpdateKhoHangReferenceData()
        {
            UpdateReferenceData(out _rKhoHangs
                , GetReferenceFilter<rKhoHang>(Constant.ColumnName_KhoHang)
                , (p => p.rKhoHangList = _rKhoHangs));
        }

        protected override void UpdateAllReferenceData()
        {
            UpdateNhanVienReferenceData();
            UpdateNhaCungCapReferenceData();
            UpdateKhoHangReferenceData();
        }

        public override void UpdateReferenceData(string columnName)
        {
            switch (columnName)
            {
                case Constant.ColumnName_NhanVien:
                    UpdateNhanVienReferenceData();
                    break;
                case Constant.ColumnName_NhanCungCap:
                    UpdateNhaCungCapReferenceData();
                    break;
                case Constant.ColumnName_KhoHang:
                    UpdateKhoHangReferenceData();
                    break;
            }
        }
    }
}
