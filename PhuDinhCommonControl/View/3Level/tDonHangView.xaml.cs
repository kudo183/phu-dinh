﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for tDonHangView.xaml
    /// </summary>
    public partial class tDonHangView : BaseView
    {
        public Expression<Func<PhuDinhData.tDonHang, bool>> FilterDonHang { get; set; }
        public Expression<Func<PhuDinhData.rKhachHang, bool>> FilterKhachHang { get; set; }
        public Expression<Func<PhuDinhData.rChanh, bool>> FilterChanh { get; set; }
        public PhuDinhData.rKhachHang rKhachHangDefault { get; set; }

        private List<PhuDinhData.tDonHang> _tDonHangs;
        private List<PhuDinhData.rKhachHang> _rKhachHangs;
        private List<PhuDinhData.rChanh> _rChanhs;

        private PhuDinhData.PhuDinhEntities _context = new PhuDinhData.PhuDinhEntities();

        public tDonHangView()
        {
            InitializeComponent();

            FilterDonHang = (p => true);
            FilterKhachHang = (p => true);
            FilterChanh = (p => true);
        }

        #region Override base view method
        public override void Save()
        {
            try
            {
                if (FilterDonHang == null)
                {
                    return;
                }

                var data = dgDonHang.DataContext as ObservableCollection<PhuDinhData.tDonHang>;
                PhuDinhData.Repository.tDonHangRepository.Save(_context, data.ToList(), FilterDonHang);
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
            if (FilterDonHang == null)
            {
                dgDonHang.DataContext = null;
                return;
            }

            var index = dgDonHang.SelectedIndex;

            _context = new PhuDinhData.PhuDinhEntities();
            _tDonHangs = PhuDinhData.Repository.tDonHangRepository.GetData(_context, FilterDonHang);
            var collection = new ObservableCollection<PhuDinhData.tDonHang>(_tDonHangs);
            collection.CollectionChanged += collection_CollectionChanged;
            dgDonHang.DataContext = collection;

            UpdateChanhReferenceData();
            UpdateKhachHangReferenceData();

            dgDonHang.UpdateLayout();

            dgDonHang.SelectedIndex = index;
        }

        void collection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var tDonHang = e.NewItems[0] as PhuDinhData.tDonHang;
                tDonHang.Ngay = DateTime.Now;
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
            var header = sender as DataGridColumnHeader;

            BaseView view = null;
            Window w = null;

            switch (header.Content.ToString())
            {
                case "Khách hàng":
                    view = new rKhachHangView();
                    view.RefreshView();
                    ChildWindowUtils.ShowChildWindow("Khách hàng", view);

                    UpdateKhachHangReferenceData();
                    break;
                case "Chành":
                    view = new rChanhView();
                    view.RefreshView();
                    ChildWindowUtils.ShowChildWindow("Chành", view);

                    UpdateChanhReferenceData();
                    break;
            }
        }

        private void UpdateChanhReferenceData()
        {
            _rChanhs = PhuDinhData.Repository.rChanhRepository.GetData(_context, FilterChanh);
            foreach (var tDonHang in _tDonHangs)
            {
                tDonHang.rChanhList = _rChanhs;
            }
        }

        private void UpdateKhachHangReferenceData()
        {
            _rKhachHangs = PhuDinhData.Repository.rKhachHangRepository.GetData(_context, FilterKhachHang);
            foreach (var tDonHang in _tDonHangs)
            {
                tDonHang.rKhachHangList = _rKhachHangs;
            }
        }
    }
}
