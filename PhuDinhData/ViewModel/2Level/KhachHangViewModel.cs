using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using PhuDinhData.Filter;
using PhuDinhData.ViewModel.DataGridColumnHeaderFilterModel;
using System;
using PhuDinhCommon;

namespace PhuDinhData.ViewModel
{
    public class KhachHangViewModel : BaseViewModel<rKhachHang>
    {
        private List<rDiaDiem> _rDiaDiems;
        private string _filterDiaDiem = string.Empty;

        public static HeaderTextFilterModel Header_DiaDiem = new HeaderTextFilterModel(Constant.ColumnName_DiaDiem);

        public KhachHangViewModel()
        {
            Entity = new ObservableCollection<rKhachHang>();

            MainFilter = new Filter_rKhachHang();

            SetReferenceFilter<rDiaDiem>(Constant.ColumnName_DiaDiem, (p => true));
        }

        public override void Load()
        {
            Entity.CollectionChanged += Entity_CollectionChanged;

            Header_DiaDiem.Text = _filterDiaDiem;
            Header_DiaDiem.PropertyChanged += Header_DiaDiem_PropertyChanged;
        }

        public override void Unload()
        {
            Entity.CollectionChanged -= Entity_CollectionChanged;

            _filterDiaDiem = Header_DiaDiem.Text;
            Header_DiaDiem.PropertyChanged -= Header_DiaDiem_PropertyChanged;
        }

        void Header_DiaDiem_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            MainFilter.SetFilter(Filter_rKhachHang.Tinh, Header_DiaDiem.Text);

            OnHeaderFilterChanged();
        }

        void Entity_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var khachHang = e.NewItems[0] as rKhachHang;
                khachHang.rDiaDiemList = _rDiaDiems;

                if (GetDefaultValue(Constant.ColumnName_MaDiaDiem) != null)
                {
                    khachHang.MaDiaDiem = Convert.ToInt32(GetDefaultValue(Constant.ColumnName_MaDiaDiem));
                }
            }
        }

        private void UpdateDiaDiemReferenceData()
        {
            UpdateReferenceData(out _rDiaDiems
                , GetReferenceFilter<rDiaDiem>(Constant.ColumnName_DiaDiem)
                , (p => p.rDiaDiemList = _rDiaDiems));
        }

        protected override void UpdateAllReferenceData()
        {
            UpdateDiaDiemReferenceData();
        }

        public override void UpdateReferenceData(string columnName)
        {
            switch (columnName)
            {
                case Constant.ColumnName_DiaDiem:
                    UpdateDiaDiemReferenceData();
                    break;
            }
        }
    }
}
