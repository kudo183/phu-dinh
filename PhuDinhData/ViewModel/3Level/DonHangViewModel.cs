using PhuDinhDataEntity;
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
        private List<rKhachHangChanh> _khachHangChanhs;
        private Dictionary<int, List<rChanh>> _dicKhachHangChanhs = new Dictionary<int, List<rChanh>>();

        public HeaderTextFilterModel Header_KhachHang { get; set; }
        public HeaderTextFilterModel Header_Chanh { get; set; }
        public HeaderComboBoxFilterModel Header_KhoHang { get; set; }

        public HeaderDateFilterModel Header_Ngay { get; set; }

        public DonHangViewModel()
        {
            Entity = new ObservableCollection<tDonHang>();

            MainFilter = new Filter_tDonHang();

            SetDefaultValue(Constant.ColumnName_MaKhoHang, 1);
            SetDefaultValue(Constant.ColumnName_MaKhachHang, 4);

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
            foreach (var donHang in Entity)
            {
                donHang.PropertyChanged += donHang_PropertyChanged;
            }

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

        void donHang_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "MaKhachHang")
            {
                var donHang = sender as tDonHang;
                if (donHang.rKhachHang != null && _dicKhachHangChanhs.ContainsKey(donHang.rKhachHang.Ma) == true)
                {
                    donHang.rChanhList = _dicKhachHangChanhs[donHang.rKhachHang.Ma];

                    donHang.MaChanh = donHang.rChanhList[0].Ma;
                }
                else
                {
                    donHang.MaChanh = null;
                    donHang.rChanhList = _rChanhs;
                }
            }
        }

        public override void Unload()
        {
            Entity.CollectionChanged -= Entity_CollectionChanged;
            foreach (var donHang in Entity)
            {
                donHang.PropertyChanged -= donHang_PropertyChanged;
            }

            Header_Ngay.PropertyChanged -= Header_Ngay_PropertyChanged;
            Header_KhachHang.PropertyChanged -= Header_KhachHang_PropertyChanged;
            Header_Chanh.PropertyChanged -= Header_Chanh_PropertyChanged;
            Header_KhoHang.PropertyChanged -= Header_KhoHang_PropertyChanged;
        }

        void Header_Ngay_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            MainFilter.SetFilter(Filter_tDonHang.Ngay, Header_Ngay.GetFilterValue());

            OnHeaderFilterChanged();
        }

        void Header_KhachHang_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            MainFilter.SetFilter(Filter_tDonHang.TenKhachHang, Header_KhachHang.GetFilterValue());

            OnHeaderFilterChanged();
        }

        void Header_Chanh_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            MainFilter.SetFilter(Filter_tDonHang.TenChanh, Header_Chanh.GetFilterValue());

            OnHeaderFilterChanged();
        }

        void Header_KhoHang_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            MainFilter.SetFilter(Filter_tDonHang.MaKhoHang, Header_KhoHang.GetFilterValue());

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
                tDonHang.PropertyChanged += donHang_PropertyChanged;

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
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                var tDonHang = e.OldItems[0] as tDonHang;
                tDonHang.PropertyChanged -= donHang_PropertyChanged;
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
            //UpdateReferenceData(out _rChanhs,
            //    GetReferenceFilter<rChanh>(Constant.ColumnName_Chanh),
            //    (p => p.rChanhList = _rChanhs));

            _rChanhs = _contextManager.GetData<rChanh>(
                null, EntityHelper.GetReferenceDataRelatedTables("rChanh"));

            _khachHangChanhs = _contextManager.GetData<rKhachHangChanh>(
                null, EntityHelper.GetReferenceDataRelatedTables("rKhachHangChanh"));

            _dicKhachHangChanhs.Clear();
            foreach (var khachHangChanh in _khachHangChanhs)
            {
                if (_dicKhachHangChanhs.ContainsKey(khachHangChanh.MaKhachHang) == false)
                {
                    _dicKhachHangChanhs.Add(khachHangChanh.MaKhachHang, new List<rChanh>());
                }

                if (khachHangChanh.LaMacDinh)
                {
                    _dicKhachHangChanhs[khachHangChanh.MaKhachHang].Insert(0, khachHangChanh.rChanh);
                }
                else
                {
                    _dicKhachHangChanhs[khachHangChanh.MaKhachHang].Add(khachHangChanh.rChanh);
                }
            }

            foreach (var donHang in Entity)
            {
                if (_dicKhachHangChanhs.ContainsKey(donHang.rKhachHang.Ma) == true)
                {
                    donHang.rChanhList = _dicKhachHangChanhs[donHang.rKhachHang.Ma];
                }
                else
                {
                    donHang.rChanhList = _rChanhs;
                }
            }
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
