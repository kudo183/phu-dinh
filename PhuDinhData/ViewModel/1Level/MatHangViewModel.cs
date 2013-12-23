using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using PhuDinhData.Filter;
using PhuDinhData.ViewModel.DataGridColumnHeaderFilterModel;
using System;
using PhuDinhCommon;

namespace PhuDinhData.ViewModel
{
    public class MatHangViewModel : BaseViewModel<tMatHang>
    {
        private List<rLoaiHang> _rLoaiHangs;
        private string _filterLoaiHang = string.Empty;

        public static HeaderTextFilterModel Header_LoaiHang = new HeaderTextFilterModel(Constant.ColumnName_LoaiHang);

        public MatHangViewModel()
        {
            Entity = new ObservableCollection<tMatHang>();

            MainFilter = new Filter_tMatHang();

            SetReferenceFilter<rLoaiHang>(Constant.ColumnName_LoaiHang, (p => true));
        }

        public override void Load()
        {
            Entity.CollectionChanged += Entity_CollectionChanged;

            MatHangViewModel.Header_LoaiHang.Text = _filterLoaiHang;
            MatHangViewModel.Header_LoaiHang.PropertyChanged += Header_LoaiHang_PropertyChanged;
        }

        public override void Unload()
        {
            Entity.CollectionChanged -= Entity_CollectionChanged;

            _filterLoaiHang = MatHangViewModel.Header_LoaiHang.Text;
            MatHangViewModel.Header_LoaiHang.PropertyChanged -= Header_LoaiHang_PropertyChanged;
        }

        void Header_LoaiHang_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            MainFilter.SetFilter(Filter_tMatHang.TenLoaiHang, MatHangViewModel.Header_LoaiHang.Text);

            OnHeaderFilterChanged();
        }

        void Entity_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var matHang = e.NewItems[0] as tMatHang;
                matHang.rLoaiHangList = _rLoaiHangs;

                if (GetDefaultValue(Constant.ColumnName_MaLoaiHang) != null)
                {
                    matHang.MaLoai = Convert.ToInt32(GetDefaultValue(Constant.ColumnName_MaLoaiHang));
                }
            }
        }

        private void UpdateLoaiHangReferenceData()
        {
            UpdateReferenceData(out _rLoaiHangs
                , GetReferenceFilter<rLoaiHang>(Constant.ColumnName_LoaiHang)
                , (p => p.rLoaiHangList = _rLoaiHangs));
        }

        protected override void UpdateAllReferenceData()
        {
            UpdateLoaiHangReferenceData();
        }

        public override void UpdateReferenceData(string columnName)
        {
            switch (columnName)
            {
                case Constant.ColumnName_LoaiHang:
                    UpdateLoaiHangReferenceData();
                    break;
            }
        }
    }
}
