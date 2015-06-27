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
    public class ChuyenHangViewModel : BaseViewModel<tChuyenHang>
    {
        private List<rNhanVien> _rNhanViens;

        public HeaderTextFilterModel Header_NhanVien { get; set; }
        public HeaderDateFilterModel Header_Ngay { get; set; }

        public ChuyenHangViewModel()
        {
            Entity = new ObservableCollection<tChuyenHang>();

            MainFilter = new Filter_tChuyenHang();

            SetReferenceFilter<rNhanVien>(Constant.ColumnName_NhanVien, (p => true));

            Header_Ngay = new HeaderDateFilterModel(Constant.ColumnName_Ngay);
            Header_NhanVien = new HeaderTextFilterModel(Constant.ColumnName_NhanVien);
        }

        public override void Load()
        {
            _isLoading = true;

            Entity.CollectionChanged += Entity_CollectionChanged;

            Header_Ngay.PropertyChanged += Header_Ngay_PropertyChanged;
            Header_NhanVien.PropertyChanged += Header_NhanVien_PropertyChanged;

            Header_Ngay_PropertyChanged(null, null);
            Header_NhanVien_PropertyChanged(null, null);

            _isLoading = false;
        }

        public override void Unload()
        {
            Entity.CollectionChanged -= Entity_CollectionChanged;

            Header_Ngay.PropertyChanged -= Header_Ngay_PropertyChanged;
            Header_NhanVien.PropertyChanged -= Header_NhanVien_PropertyChanged;
        }

        void Header_NhanVien_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            MainFilter.SetFilter(Filter_tChuyenHang.TenNhanVien, Header_NhanVien.Text);

            OnHeaderFilterChanged();
        }

        void Header_Ngay_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (Header_Ngay.IsUsed)
            {
                MainFilter.SetFilter(Filter_tChuyenHang.Ngay, Header_Ngay.Date);
            }
            else
            {
                MainFilter.SetFilter(Filter_tChuyenHang.Ngay, null);
            }

            OnHeaderFilterChanged();
        }

        void Entity_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var tChuyenHang = e.NewItems[0] as tChuyenHang;
                tChuyenHang.Ngay = (Header_Ngay.IsUsed) ? Header_Ngay.Date : DateTime.Now;
                tChuyenHang.rNhanVienList = _rNhanViens;

                if (GetDefaultValue(Constant.ColumnName_MaNhanVien) != null)
                {
                    tChuyenHang.MaNhanVienGiaoHang = Convert.ToInt32(GetDefaultValue(Constant.ColumnName_MaNhanVien));
                }
            }
        }

        private void UpdateNhanVienReferenceData()
        {
            UpdateReferenceData(out _rNhanViens
                , GetReferenceFilter<rNhanVien>(Constant.ColumnName_NhanVien)
                , (p => p.rNhanVienList = _rNhanViens));
        }

        protected override void UpdateAllReferenceData()
        {
            UpdateNhanVienReferenceData();
        }

        public override void UpdateReferenceData(string columnName)
        {
            switch (columnName)
            {
                case Constant.ColumnName_NhanVien:
                    UpdateNhanVienReferenceData();
                    break;
            }
        }
    }
}
