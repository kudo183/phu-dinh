using PhuDinhDataEntity;
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
    public class KhachHangChanhViewModel : BaseViewModel<rKhachHangChanh>
    {
        private List<rKhachHang> _rKhachHangs;
        private List<rChanh> _rChanhs;

        public HeaderTextFilterModel Header_KhachHang { get; set; }
        public HeaderTextFilterModel Header_Chanh { get; set; }

        public KhachHangChanhViewModel()
        {
            Entity = new ObservableCollection<rKhachHangChanh>();

            MainFilter = new Filter_rKhachHangChanh();

            Header_KhachHang = new HeaderTextFilterModel(Constant.ColumnName_KhachHang);
            Header_Chanh = new HeaderTextFilterModel(Constant.ColumnName_Chanh);
        }

        public override void Load()
        {
            _isLoading = true;

            Entity.CollectionChanged += Entity_CollectionChanged;

            Header_KhachHang.PropertyChanged += Header_KhachHang_PropertyChanged;
            Header_Chanh.PropertyChanged += Header_Chanh_PropertyChanged;

            Header_KhachHang_PropertyChanged(null, null);
            Header_Chanh_PropertyChanged(null, null);

            _isLoading = false;
        }

        public override void Unload()
        {
            Entity.CollectionChanged -= Entity_CollectionChanged;

            Header_KhachHang.PropertyChanged -= Header_KhachHang_PropertyChanged;
            Header_Chanh.PropertyChanged -= Header_Chanh_PropertyChanged;
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
