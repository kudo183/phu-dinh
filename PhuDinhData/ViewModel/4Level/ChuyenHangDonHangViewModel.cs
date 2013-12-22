using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq.Expressions;
using PhuDinhCommon;
using PhuDinhData.Filter;
using PhuDinhData.ViewModel.DataGridColumnHeaderFilterModel;

namespace PhuDinhData.ViewModel
{
    public class ChuyenHangDonHangViewModel : BaseViewModel<tChuyenHangDonHang>
    {
        private List<tChuyenHang> _tChuyenHangs;
        private List<tDonHang> _tDonHangs;        

        private string _filterChuyenHang = string.Empty;

        private string _filterDonHang = string.Empty;

        public Expression<Func<tChuyenHang, bool>> RFilter_ChuyenHang { get; set; }
        public Expression<Func<tDonHang, bool>> RFilter_DonHang { get; set; }

        public tChuyenHang tChuyenHangDefault { get; set; }
        public tDonHang tDonHangDefault { get; set; }
        
        public static HeaderTextFilterModel Header_ChuyenHang = new HeaderTextFilterModel(Constant.ColumnName_ChuyenHang);
        public static HeaderTextFilterModel Header_DonHang = new HeaderTextFilterModel(Constant.ColumnName_DonHang);

        public ChuyenHangDonHangViewModel()
        {
            Entity = new ObservableCollection<tChuyenHangDonHang>();

            MainFilter = new Filter_tChuyenHangDonHang();
            RFilter_ChuyenHang = (p => true);
            RFilter_DonHang = (p => true);
        }

        public override void Load()
        {
            _isLoading = true;

            Entity.CollectionChanged += Entity_CollectionChanged;

            Header_ChuyenHang.PropertyChanged += Header_ChuyenHang_PropertyChanged;
            Header_DonHang.PropertyChanged += Header_DonHang_PropertyChanged;

            DonHangViewModel.Header_KhachHang.Text = _filterChuyenHang;

            DonHangViewModel.Header_KhachHangChanh.Text = _filterDonHang;

            _isLoading = false;
        }

        public override void Unload()
        {
            Entity.CollectionChanged -= Entity_CollectionChanged;

            Header_ChuyenHang.PropertyChanged -= Header_ChuyenHang_PropertyChanged;
            Header_DonHang.PropertyChanged -= Header_DonHang_PropertyChanged;

            _filterChuyenHang = DonHangViewModel.Header_KhachHang.Text;

            _filterDonHang = DonHangViewModel.Header_KhachHangChanh.Text;
        }

        void Header_ChuyenHang_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            MainFilter.SetFilter(Filter_tChuyenHangDonHang.TenChuyenHang, ChuyenHangDonHangViewModel.Header_ChuyenHang.Text);

            OnHeaderFilterChanged();
        }

        void Header_DonHang_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            MainFilter.SetFilter(Filter_tChuyenHangDonHang.TenDonHang, ChuyenHangDonHangViewModel.Header_DonHang.Text);

            OnHeaderFilterChanged();
        }

        void Entity_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var tChuyenHangDonHang = e.NewItems[0] as tChuyenHangDonHang;
                tChuyenHangDonHang.tChuyenHangList = _tChuyenHangs;
                tChuyenHangDonHang.tDonHangList = _tDonHangs;

                if (tChuyenHangDefault != null)
                {
                    tChuyenHangDonHang.MaChuyenHang = tChuyenHangDefault.Ma;
                }

                if (tDonHangDefault != null)
                {
                    tChuyenHangDonHang.MaDonHang = tDonHangDefault.Ma;
                }
            }
        }

        public void UpdateChuyenHangReferenceData()
        {
            UpdateReferenceData(out _tChuyenHangs, RFilter_ChuyenHang, (p => p.tChuyenHangList = _tChuyenHangs));
        }

        public void UpdateDonHangReferenceData()
        {
            UpdateReferenceData(out _tDonHangs, RFilter_DonHang, (p => p.tDonHangList = _tDonHangs));
        }

        protected override void UpdateAllReferenceData()
        {
            UpdateChuyenHangReferenceData();
            UpdateDonHangReferenceData();
        }
    }
}
