using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using PhuDinhData.Filter;
using System;
using PhuDinhCommon;
using CustomControl.DataGridColumnHeaderFilterModel;
using System.Linq;

namespace PhuDinhData.ViewModel
{
    public class NhapHangViewModel : BaseViewModel<tNhapHang>
    {
        private List<rNhanVien> _rNhanViens;
        private List<rNhaCungCap> _rNhaCungCaps;
        private List<rKhoHang> _rKhoHangs;

        public HeaderTextFilterModel Header_NhanVien { get; set; }
        public HeaderComboBoxFilterModel Header_NhaCungCap { get; set; }
        public HeaderComboBoxFilterModel Header_KhoHang { get; set; }

        public HeaderDateFilterModel Header_Ngay { get; set; }

        public NhapHangViewModel()
        {
            Entity = new ObservableCollection<tNhapHang>();

            MainFilter = new Filter_tNhapHang();

            SetDefaultValue(Constant.ColumnName_MaKhoHang, 1);
            SetDefaultValue(Constant.ColumnName_MaNhaCungCap, 6);
            
            SetReferenceFilter<rNhanVien>(Constant.ColumnName_NhanVien, (p => true));
            SetReferenceFilter<rNhaCungCap>(Constant.ColumnName_NhanCungCap, (p => true));
            SetReferenceFilter<rKhoHang>(Constant.ColumnName_KhoHang, (p => true));

            Header_NhanVien = new HeaderTextFilterModel(Constant.ColumnName_NhanVien);
            Header_NhaCungCap = new HeaderComboBoxFilterModel(Constant.ColumnName_NhanCungCap);
            Header_KhoHang = new HeaderComboBoxFilterModel(Constant.ColumnName_KhoHang);

            Header_Ngay = new HeaderDateFilterModel(Constant.ColumnName_Ngay);
        }

        public override void Load()
        {
            _isLoading = true;

            Header_KhoHang.SelectedValue = GetDefaultValue(Constant.ColumnName_MaKhoHang);
            Header_NhaCungCap.SelectedValue = GetDefaultValue(Constant.ColumnName_MaNhaCungCap);

            Entity.CollectionChanged += Entity_CollectionChanged;

            Header_Ngay.PropertyChanged += Header_Ngay_PropertyChanged;
            Header_NhanVien.PropertyChanged += Header_NhanVien_PropertyChanged;
            Header_NhaCungCap.PropertyChanged += Header_NhaCungCap_PropertyChanged;
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
        }

        void Header_NhanVien_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            MainFilter.SetFilter(Filter_tNhapHang.TenNhanVien, Header_NhanVien.Text);

            OnHeaderFilterChanged();
        }

        void Header_NhaCungCap_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (Header_NhaCungCap.IsUsed)
            {
                MainFilter.SetFilter(Filter_tNhapHang.MaNhaCungCap, Header_NhaCungCap.SelectedValue);
            }
            else
            {
                MainFilter.SetFilter(Filter_tNhapHang.MaNhaCungCap, null);
            }

            if (_isLoading == false)
            {
                SetDefaultValue(Constant.ColumnName_MaNhaCungCap, Header_NhaCungCap.SelectedValue);
            }

            OnHeaderFilterChanged();
        }

        void Header_KhoHang_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (Header_KhoHang.IsUsed)
            {
                MainFilter.SetFilter(Filter_tNhapHang.MaKhoHang, Header_KhoHang.SelectedValue);
            }
            else
            {
                MainFilter.SetFilter(Filter_tNhapHang.MaKhoHang, null);
            }

            if (_isLoading == false)
            {
                SetDefaultValue(Constant.ColumnName_MaKhoHang, Header_KhoHang.SelectedValue);
            }

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

            Header_NhaCungCap.ItemSource = _rNhaCungCaps.ToDictionary(p => p.Ma, p => p.TenNhaCungCap);
            Header_NhaCungCap.SelectedValue = GetDefaultValue(Constant.ColumnName_MaNhaCungCap);
        }

        private void UpdateKhoHangReferenceData()
        {
            UpdateReferenceData(out _rKhoHangs
                , GetReferenceFilter<rKhoHang>(Constant.ColumnName_KhoHang)
                , (p => p.rKhoHangList = _rKhoHangs));

            Header_KhoHang.ItemSource = _rKhoHangs.ToDictionary(p => p.Ma, p => p.TenKho);
            Header_KhoHang.SelectedValue = GetDefaultValue(Constant.ColumnName_MaKhoHang);
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
