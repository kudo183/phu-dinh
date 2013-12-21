using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq.Expressions;
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

        public Expression<Func<rLoaiHang, bool>> RFilter_LoaiHang { get; set; }

        public rLoaiHang rLoaiHangDefault { get; set; }

        public static HeaderTextFilterModel Header_LoaiHang = new HeaderTextFilterModel(Constant.ColumnName_LoaiHang);

        public MatHangViewModel()
        {
            Entity = new ObservableCollection<tMatHang>();

            RFilter_LoaiHang = (p => true);
            MainFilter = new Filter_tMatHang();
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

                if (rLoaiHangDefault != null)
                {
                    matHang.MaLoai = rLoaiHangDefault.Ma;
                }
            }
        }

        public void UpdateLoaiHangReferenceData()
        {
            UpdateReferenceData(out _rLoaiHangs, RFilter_LoaiHang, (p => p.rLoaiHangList = _rLoaiHangs));
        }

        protected override void UpdateAllReferenceData()
        {
            UpdateLoaiHangReferenceData();
        }
    }
}
