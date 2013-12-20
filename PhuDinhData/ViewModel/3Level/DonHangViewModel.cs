using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using PhuDinhData.Filter;
using PhuDinhData.Repository;
using PhuDinhData.ViewModel.DataGridColumnHeaderFilterModel;

namespace PhuDinhData.ViewModel
{
    public class DonHangViewModel : BaseViewModel<tDonHang>
    {
        private List<rKhachHang> _rKhachHangs;
        private List<rChanh> _rChanhs;

        private bool _isUsedDateFilter = true;
        private DateTime _filterDate = DateTime.Now.Date;

        private string _filterKhachHang = string.Empty;

        private string _filterChanh = string.Empty;

        public Filter_tDonHang MainFilter { get; private set; }
        public Expression<Func<rKhachHang, bool>> Reference_FilterKhachHang { get; set; }
        public Expression<Func<rChanh, bool>> Reference_FilterChanh { get; set; }
        public rKhachHang rKhachHangDefault { get; set; }

        public static HeaderDateFilter Header_Ngay = new HeaderDateFilter("Ngày");
        public static HeaderTextFilter Header_KhachHang = new HeaderTextFilter("Khách Hàng");
        public static HeaderTextFilter Header_KhachHangChanh = new HeaderTextFilter("Khách Hàng Chành");

        public DonHangViewModel()
        {
            Entity = new ObservableCollection<tDonHang>();

            MainFilter = new Filter_tDonHang();
            Reference_FilterKhachHang = (p => true);
            Reference_FilterChanh = (p => true);
        }

        public void Load()
        {
            _isLoading = true;

            Entity.CollectionChanged += Entity_CollectionChanged;

            Header_Ngay.PropertyChanged += Header_Ngay_PropertyChanged;
            Header_KhachHang.PropertyChanged += Header_KhachHang_PropertyChanged;
            Header_KhachHangChanh.PropertyChanged += Header_KhachHangChanh_PropertyChanged;

            DonHangViewModel.Header_Ngay.Date = _filterDate;
            DonHangViewModel.Header_Ngay.IsUsed = _isUsedDateFilter;

            DonHangViewModel.Header_KhachHang.Text = _filterKhachHang;

            DonHangViewModel.Header_KhachHangChanh.Text = _filterChanh;

            _isLoading = false;
        }

        public void Unload()
        {
            Entity.CollectionChanged -= Entity_CollectionChanged;

            Header_Ngay.PropertyChanged -= Header_Ngay_PropertyChanged;
            Header_KhachHang.PropertyChanged -= Header_KhachHang_PropertyChanged;
            Header_KhachHangChanh.PropertyChanged -= Header_KhachHangChanh_PropertyChanged;

            _filterDate = DonHangViewModel.Header_Ngay.Date;
            _isUsedDateFilter = DonHangViewModel.Header_Ngay.IsUsed;

            _filterKhachHang = DonHangViewModel.Header_KhachHang.Text;

            _filterChanh = DonHangViewModel.Header_KhachHangChanh.Text;
        }

        void Header_Ngay_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (DonHangViewModel.Header_Ngay.IsUsed)
            {
                MainFilter.SetFilterNgay(DonHangViewModel.Header_Ngay.Date);
            }
            else
            {
                MainFilter.SetFilterNgay(null);
            }

            OnHeaderFilterChanged();
        }

        void Header_KhachHang_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            MainFilter.SetFilterTenKhachHang(DonHangViewModel.Header_KhachHang.Text);

            OnHeaderFilterChanged();
        }

        void Header_KhachHangChanh_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            MainFilter.SetFilterTenChanh(DonHangViewModel.Header_KhachHangChanh.Text);

            OnHeaderFilterChanged();
        }


        void Entity_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var tDonHang = e.NewItems[0] as tDonHang;

                tDonHang.Ngay = (DonHangViewModel.Header_Ngay.IsUsed)
                    ? DonHangViewModel.Header_Ngay.Date : DateTime.Now;

                tDonHang.rChanhList = _rChanhs;
                tDonHang.rKhachHangList = _rKhachHangs;

                if (rKhachHangDefault != null)
                {
                    tDonHang.MaKhachHang = rKhachHangDefault.Ma;
                }
            }
        }

        public override void RefreshData()
        {
            if (MainFilter.FilterDonHang == null)
            {
                return;
            }

            _context = ContextFactory.CreateContext();

            ItemCount = tDonHangRepository.GetDataCount(_context, MainFilter.FilterDonHang);
            _origData = tDonHangRepository.GetData(_context, MainFilter.FilterDonHang, PageSize, CurrentPageIndex, ItemCount);

            Unload();

            Entity.Clear();

            foreach (var tDonHang in _origData)
            {
                Entity.Add(tDonHang);
            }

            UpdateChanhReferenceData();
            UpdateKhachHangReferenceData();

            Load();
        }

        public void UpdateChanhReferenceData()
        {
            _rChanhs = rChanhRepository.GetData(_context, Reference_FilterChanh);

            if (Entity == null)
            {
                return;
            }

            foreach (var tDonHang in Entity)
            {
                tDonHang.rChanhList = _rChanhs;
            }
        }

        public void UpdateKhachHangReferenceData()
        {
            _rKhachHangs = rKhachHangRepository.GetData(_context, Reference_FilterKhachHang);

            if (Entity == null)
            {
                return;
            }

            foreach (var tDonHang in Entity)
            {
                tDonHang.rKhachHangList = _rKhachHangs;
            }
        }

        public List<Repository<tDonHang>.ChangedItemData> Save()
        {
            try
            {
                return tDonHangRepository.Save(_context, Entity.ToList(), _origData);
            }
            catch (Exception)
            {
                PhuDinhCommon.EntityFrameworkUtils.UndoContextChange(_context, EntityState.Modified);
                throw;
            }
        }
    }
}
