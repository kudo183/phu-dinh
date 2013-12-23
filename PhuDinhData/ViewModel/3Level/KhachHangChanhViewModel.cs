using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using PhuDinhCommon;
using PhuDinhData.Filter;
using PhuDinhData.ViewModel.DataGridColumnHeaderFilterModel;

namespace PhuDinhData.ViewModel
{
    public class KhachHangChanhViewModel : BaseViewModel<rKhachHangChanh>
    {
        private List<rKhachHang> _rKhachHangs;
        private List<rChanh> _rChanhs;

        private string _filterKhachHang = string.Empty;

        private string _filterChanh = string.Empty;

        public static HeaderTextFilterModel Header_KhachHang = new HeaderTextFilterModel(Constant.ColumnName_KhachHang);
        public static HeaderTextFilterModel Header_Chanh = new HeaderTextFilterModel(Constant.ColumnName_Chanh);

        public KhachHangChanhViewModel()
        {
            Entity = new ObservableCollection<rKhachHangChanh>();

            MainFilter = new Filter_rKhachHangChanh();

            SetReferenceFilter<rKhachHang>(Constant.ColumnName_KhachHang, (p => true));
            SetReferenceFilter<rChanh>(Constant.ColumnName_Chanh, (p => true));
        }

        public override void Load()
        {
            _isLoading = true;

            Entity.CollectionChanged += Entity_CollectionChanged;

            Header_KhachHang.PropertyChanged += Header_KhachHang_PropertyChanged;
            Header_Chanh.PropertyChanged += Header_Chanh_PropertyChanged;

            Header_KhachHang.Text = _filterKhachHang;

            Header_Chanh.Text = _filterChanh;

            _isLoading = false;
        }

        public override void Unload()
        {
            Entity.CollectionChanged -= Entity_CollectionChanged;

            Header_KhachHang.PropertyChanged -= Header_KhachHang_PropertyChanged;
            Header_Chanh.PropertyChanged -= Header_Chanh_PropertyChanged;

            _filterKhachHang = Header_KhachHang.Text;

            _filterChanh = Header_Chanh.Text;
        }

        void Header_KhachHang_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            MainFilter.SetFilter(Filter_rKhachHangChanh.TenKhachHang, Header_KhachHang.Text);

            OnHeaderFilterChanged();
        }

        void Header_Chanh_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            MainFilter.SetFilter(Filter_rKhachHangChanh.TenChanh, Header_Chanh.Text);

            OnHeaderFilterChanged();
        }

        void Entity_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var rKhachHangChanh = e.NewItems[0] as rKhachHangChanh;
                rKhachHangChanh.rChanhList = _rChanhs;
                rKhachHangChanh.rKhachHangList = _rKhachHangs;

                if (GetDefaultValue(Constant.ColumnName_MaKhachHang) != null)
                {
                    rKhachHangChanh.MaKhachHang = Convert.ToInt32(GetDefaultValue(Constant.ColumnName_MaKhachHang));
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

        protected override void UpdateAllReferenceData()
        {
            UpdateKhachHangReferenceData();
            UpdateChanhReferenceData();
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
            }
        }
    }
}
