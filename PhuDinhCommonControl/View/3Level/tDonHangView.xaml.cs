﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Controls.Primitives;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for tDonHangView.xaml
    /// </summary>
    public partial class tDonHangView : BaseView
    {
        public Filter_tDonHang FilterDonHang { get; private set; }
        public Expression<Func<PhuDinhData.rKhachHang, bool>> FilterKhachHang { get; set; }
        public Expression<Func<PhuDinhData.rChanh, bool>> FilterChanh { get; set; }
        public PhuDinhData.rKhachHang rKhachHangDefault { get; set; }

        private ObservableCollection<PhuDinhData.tDonHang> _tDonHangs;
        private List<PhuDinhData.rKhachHang> _rKhachHangs;
        private List<PhuDinhData.rChanh> _rChanhs;

        private PhuDinhData.PhuDinhEntities _context = ContextFactory.CreateContext();

        private bool _isUsedDateFilter = true;
        private DateTime _filterDate = DateTime.Now.Date;

        private string _filterKhachHang = string.Empty;

        private string _filterChanh = string.Empty;

        public tDonHangView()
        {
            InitializeComponent();

            FilterDonHang = new Filter_tDonHang();
            FilterKhachHang = (p => true);
            FilterChanh = (p => true);

            Loaded += tDonHangView_Loaded;
            Unloaded += tDonHangView_Unloaded;
        }

        void tDonHangView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            DataGridColumnHeaderDateFilter.DonHang.Date = _filterDate;
            DataGridColumnHeaderDateFilter.DonHang.IsUsed = _isUsedDateFilter;
            DataGridColumnHeaderDateFilter.DonHang.PropertyChanged += DonHang_PropertyChanged;
            FilterDonHang.FilterNgay = (p => p.Ngay == DataGridColumnHeaderDateFilter.DonHang.Date);

            DataGridColumnHeaderTextFilter.DonHang_KhachHang.Text = _filterKhachHang;
            DataGridColumnHeaderTextFilter.DonHang_KhachHang.PropertyChanged += DonHang_KhachHang_PropertyChanged;

            DataGridColumnHeaderTextFilter.DonHang_Chanh.Text = _filterChanh;
            DataGridColumnHeaderTextFilter.DonHang_Chanh.PropertyChanged += DonHang_Chanh_PropertyChanged;

            RefreshView();
        }

        void tDonHangView_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            DataGridColumnHeaderDateFilter.DonHang.PropertyChanged -= DonHang_PropertyChanged;
            _filterDate = DataGridColumnHeaderDateFilter.DonHang.Date;
            _isUsedDateFilter = DataGridColumnHeaderDateFilter.DonHang.IsUsed;

            DataGridColumnHeaderTextFilter.DonHang_Chanh.PropertyChanged -= DonHang_Chanh_PropertyChanged;
            _filterChanh = DataGridColumnHeaderTextFilter.DonHang_Chanh.Text;

            DataGridColumnHeaderTextFilter.DonHang_KhachHang.PropertyChanged -= DonHang_KhachHang_PropertyChanged;
            _filterKhachHang = DataGridColumnHeaderTextFilter.DonHang_KhachHang.Text;
        }

        void DonHang_KhachHang_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(DataGridColumnHeaderTextFilter.DonHang_KhachHang.Text) == false)
            {
                FilterDonHang.FilterKhachHang = (p => p.rKhachHang.TenKhachHang == DataGridColumnHeaderTextFilter.DonHang_KhachHang.Text);
            }
            else
            {
                FilterDonHang.FilterKhachHang = (p => true);
            }

            RefreshView();
        }

        void DonHang_Chanh_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(DataGridColumnHeaderTextFilter.DonHang_Chanh.Text) == false)
            {
                FilterDonHang.FilterChanh = (p => p.rChanh.TenChanh == DataGridColumnHeaderTextFilter.DonHang_Chanh.Text);
            }
            else
            {
                FilterDonHang.FilterChanh = (p => true);
            }

            RefreshView();
        }

        void DonHang_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (DataGridColumnHeaderDateFilter.DonHang.IsUsed)
            {
                FilterDonHang.FilterNgay = (p => p.Ngay == DataGridColumnHeaderDateFilter.DonHang.Date);
            }
            else
            {
                FilterDonHang.FilterNgay = (p => true);
            }

            RefreshView();
        }

        #region Override base view method
        public override void CommitEdit()
        {
            dgDonHang.CommitEdit();
            base.CommitEdit();
        }

        public override void Save()
        {
            CommitEdit();
            try
            {
                if (FilterDonHang == null)
                {
                    return;
                }

                var data = dgDonHang.DataContext as ObservableCollection<PhuDinhData.tDonHang>;
                PhuDinhData.Repository.tDonHangRepository.Save(_context, data.ToList(), FilterDonHang.FilterDonHang);
            }
            catch (Exception ex)
            {
                PhuDinhCommon.EntityFrameworkUtils.UndoContextChange(_context, EntityState.Modified);
            }

            base.Save();
        }

        public override void Cancel()
        {
            RefreshView();

            base.Cancel();
        }

        public override void RefreshView()
        {
            if (FilterDonHang.IsClearAllData == true)
            {
                dgDonHang.DataContext = null;
                return;
            }

            var index = dgDonHang.SelectedIndex;

            if (_tDonHangs != null)
            {
                _tDonHangs.CollectionChanged -= collection_CollectionChanged;
            }

            _context = ContextFactory.CreateContext();
            var tDonHangs = PhuDinhData.Repository.tDonHangRepository.GetData(_context, FilterDonHang.FilterDonHang);

            _tDonHangs = new ObservableCollection<PhuDinhData.tDonHang>(tDonHangs);
            _tDonHangs.CollectionChanged += collection_CollectionChanged;

            UpdateChanhReferenceData();
            UpdateKhachHangReferenceData();

            dgDonHang.DataContext = _tDonHangs;

            dgDonHang.SelectedIndex = index;
        }

        void collection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var tDonHang = e.NewItems[0] as PhuDinhData.tDonHang;
                tDonHang.Ngay = (DataGridColumnHeaderDateFilter.DonHang.IsUsed) ? DataGridColumnHeaderDateFilter.DonHang.Date : DateTime.Now;
                tDonHang.rChanhList = _rChanhs;
                tDonHang.rKhachHangList = _rKhachHangs;

                if (rKhachHangDefault != null)
                {
                    tDonHang.MaKhachHang = rKhachHangDefault.Ma;
                }
            }
        }

        #endregion

        private void dgDonHang_HeaderAddButtonClick(object sender, EventArgs e)
        {
            CommitEdit();
            var header = sender as DataGridColumnHeader;

            BaseView view = null;

            switch (header.Content.ToString())
            {
                case "Khách hàng":
                    view = new rKhachHangView();
                    view.RefreshView();
                    ChildWindowUtils.ShowChildWindow("Khách hàng", view);

                    UpdateKhachHangReferenceData();
                    break;
                case "Chành":
                    view = new rKhachHangChanhView();
                    view.RefreshView();
                    ChildWindowUtils.ShowChildWindow("Chành", view);

                    UpdateChanhReferenceData();
                    break;
            }
        }

        private void UpdateChanhReferenceData()
        {
            _rChanhs = PhuDinhData.Repository.rChanhRepository.GetData(_context, FilterChanh);

            if (_tDonHangs == null)
            {
                return;
            }

            foreach (var tDonHang in _tDonHangs)
            {
                tDonHang.rChanhList = _rChanhs;
            }
        }

        private void UpdateKhachHangReferenceData()
        {
            _rKhachHangs = PhuDinhData.Repository.rKhachHangRepository.GetData(_context, FilterKhachHang);

            if (_tDonHangs == null)
            {
                return;
            }

            foreach (var tDonHang in _tDonHangs)
            {
                tDonHang.rKhachHangList = _rKhachHangs;
            }
        }
    }
}
