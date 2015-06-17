using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using PhuDinhCommon;
using PhuDinhData.Filter;
using CustomControl.DataGridColumnHeaderFilterModel;
using System.Linq;

namespace PhuDinhData.ViewModel
{
    public class DonHangViewModel : BaseViewModel<tDonHang>
    {
        private List<rKhachHang> _rKhachHangs;
        private List<rChanh> _rChanhs;
        private List<rKhoHang> _rKhoHangs;

        public HeaderTextFilterModel Header_KhachHang { get; set; }
        public HeaderTextFilterModel Header_Chanh { get; set; }
        public HeaderComboBoxFilterModel Header_KhoHang { get; set; }

        public HeaderDateFilterModel Header_Ngay { get; set; }

        public DonHangViewModel()
        {
            Entity = new ObservableCollection<tDonHang>();

            MainFilter = new Filter_tDonHang();

            SetDefaultValue(Constant.ColumnName_MaKhoHang, 1);

            SetReferenceFilter<rKhachHang>(Constant.ColumnName_KhachHang, (p => true));
            SetReferenceFilter<rChanh>(Constant.ColumnName_Chanh, (p => true));
            SetReferenceFilter<rKhoHang>(Constant.ColumnName_KhoHang, (p => true));

            Header_KhachHang = new HeaderTextFilterModel(Constant.ColumnName_KhachHang);
            Header_Chanh = new HeaderTextFilterModel(Constant.ColumnName_Chanh);
            Header_KhoHang = new HeaderComboBoxFilterModel(Constant.ColumnName_KhoHang);

            Header_Ngay = new HeaderDateFilterModel(Constant.ColumnName_Ngay);
        }

        public override void Load()
        {
            _isLoading = true;

            Header_KhoHang.SelectedValue = GetDefaultValue(Constant.ColumnName_MaKhoHang);

            Entity.CollectionChanged += Entity_CollectionChanged;

            Header_Ngay.PropertyChanged += Header_Ngay_PropertyChanged;

            Header_KhachHang.PropertyChanged += Header_KhachHang_PropertyChanged;
            Header_Chanh.PropertyChanged += Header_Chanh_PropertyChanged;
            Header_KhoHang.PropertyChanged += Header_KhoHang_PropertyChanged;

            Header_Ngay_PropertyChanged(null, null);
            Header_KhachHang_PropertyChanged(null, null);
            Header_Chanh_PropertyChanged(null, null);
            Header_KhoHang_PropertyChanged(null, null);

            _isLoading = false;
        }

        public override void Unload()
        {
            Entity.CollectionChanged -= Entity_CollectionChanged;

            Header_Ngay.PropertyChanged -= Header_Ngay_PropertyChanged;
            Header_KhachHang.PropertyChanged -= Header_KhachHang_PropertyChanged;
            Header_Chanh.PropertyChanged -= Header_Chanh_PropertyChanged;
            Header_KhoHang.PropertyChanged -= Header_KhoHang_PropertyChanged;
        }

        void Header_Ngay_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (Header_Ngay.IsUsed)
            {
                MainFilter.SetFilter(Filter_tDonHang.Ngay, Header_Ngay.Date);
            }
            else
            {
                MainFilter.SetFilter(Filter_tDonHang.Ngay, null);
            }

            OnHeaderFilterChanged();
        }

        void Header_KhachHang_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            MainFilter.SetFilter(Filter_tDonHang.TenKhachHang, Header_KhachHang.Text);

            OnHeaderFilterChanged();
        }

        void Header_Chanh_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            MainFilter.SetFilter(Filter_tDonHang.TenChanh, Header_Chanh.Text);

            OnHeaderFilterChanged();
        }

        void Header_KhoHang_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (Header_KhoHang.IsUsed)
            {
                MainFilter.SetFilter(Filter_tDonHang.MaKhoHang, Header_KhoHang.SelectedValue);
            }
            else
            {
                MainFilter.SetFilter(Filter_tDonHang.MaKhoHang, null);
            }

            if (_isLoading == false)
            {
                SetDefaultValue(Constant.ColumnName_MaKhoHang, Header_KhoHang.SelectedValue);
            }

            OnHeaderFilterChanged();
        }

        void Entity_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var tDonHang = e.NewItems[0] as tDonHang;

                tDonHang.Ngay = (Header_Ngay.IsUsed)
                    ? Header_Ngay.Date : DateTime.Now;

                tDonHang.rChanhList = _rChanhs;
                tDonHang.rKhachHangList = _rKhachHangs;
                tDonHang.rKhoHangList = _rKhoHangs;

                if (GetDefaultValue(Constant.ColumnName_MaKhachHang) != null)
                {
                    tDonHang.MaKhachHang = Convert.ToInt32(GetDefaultValue(Constant.ColumnName_MaKhachHang));
                }

                if (GetDefaultValue(Constant.ColumnName_MaKhoHang) != null)
                {
                    tDonHang.MaKhoHang = Convert.ToInt32(GetDefaultValue(Constant.ColumnName_MaKhoHang));
                }
            }
        }

        private void UpdateKhachHangReferenceData()
        {
            UpdateReferenceData(out _rKhachHangs
                , GetReferenceFilter<rKhachHang>(Constant.ColumnName_KhachHang)
                , (p => p.rKhachHangList = _rKhachHangs));
        }

        private void UpdateChanhReferenceData()
        {
            UpdateReferenceData(out _rChanhs,
                GetReferenceFilter<rChanh>(Constant.ColumnName_Chanh),
                (p => p.rChanhList = _rChanhs));
        }

        private void UpdateKhoHangReferenceData()
        {
            UpdateReferenceData(out _rKhoHangs,
                GetReferenceFilter<rKhoHang>(Constant.ColumnName_KhoHang),
                (p => p.rKhoHangList = _rKhoHangs));

            Header_KhoHang.ItemSource = _rKhoHangs.ToDictionary(p => p.Ma, p => p.TenKho);
            Header_KhoHang.SelectedValue = GetDefaultValue(Constant.ColumnName_MaKhoHang);
        }

        protected override void UpdateAllReferenceData()
        {
            UpdateKhachHangReferenceData();
            UpdateChanhReferenceData();
            UpdateKhoHangReferenceData();
        }

        public override void UpdateReferenceData(string columnName)
        {
            switch (columnName)
            {
                case Constant.ColumnName_KhachHang:
                    UpdateKhachHangReferenceData();
                    break;
                case Constant.ColumnName_Chanh:
                    UpdateChanhReferenceData();
                    break;
                case Constant.ColumnName_KhoHang:
                    UpdateKhoHangReferenceData();
                    break;
            }
        }
    }
}
