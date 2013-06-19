﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for tChiTietChuyenHangDonHangView.xaml
    /// </summary>
    public partial class tChiTietChuyenHangDonHangView : BaseView
    {
        public Expression<Func<PhuDinhData.tChiTietChuyenHangDonHang, bool>> FilterChiTietChuyenHangDonHang { get; set; }
        public Expression<Func<PhuDinhData.tChuyenHangDonHang, bool>> FilterChuyenHangDonHang { get; set; }
        public Expression<Func<PhuDinhData.tMatHang, bool>> FilterMatHang { get; set; }

        private List<PhuDinhData.tChuyenHangDonHang> _tChuyenHangDonHangs;
        private List<PhuDinhData.tMatHang> _tMatHangs;
        private readonly PhuDinhData.PhuDinhEntities _context = new PhuDinhData.PhuDinhEntities();

        public tChiTietChuyenHangDonHangView()
        {
            InitializeComponent();
            FilterChiTietChuyenHangDonHang = (p => true);
            FilterChuyenHangDonHang = (p => true);
            FilterMatHang = (p => true);
        }

        #region Override base view method
        public override void Save()
        {
            try
            {
                var data = this.dgChiTietChuyenHangDonHang.DataContext as ObservableCollection<PhuDinhData.tChiTietChuyenHangDonHang>;
                PhuDinhData.Repository.tChiTietChuyenHangDonHangRepository.Save(_context, data.ToList(), FilterChiTietChuyenHangDonHang);
                RefreshView();
            }
            catch (Exception ex)
            {
                PhuDinhCommon.EntityFrameworkUtils.UndoContextChange(_context, EntityState.Modified);
            }
        }

        public override void Cancel()
        {
            RefreshView();
        }

        public override void RefreshView()
        {
            _tChuyenHangDonHangs = PhuDinhData.Repository.tChuyenHangDonHangRepository.GetData(_context, FilterChuyenHangDonHang);
            _tMatHangs = PhuDinhData.Repository.tMatHangRepository.GetData(_context, FilterMatHang);

            var data = PhuDinhData.Repository.tChiTietChuyenHangDonHangRepository.GetData(_context, FilterChiTietChuyenHangDonHang);

            foreach (var tChiTietChuyenHangDonHang in data)
            {
                tChiTietChuyenHangDonHang.tChuyenHangDonHangList = _tChuyenHangDonHangs;
                tChiTietChuyenHangDonHang.tMatHangList = _tMatHangs;
            }

            var collection = new ObservableCollection<PhuDinhData.tChiTietChuyenHangDonHang>(data);
            collection.CollectionChanged += collection_CollectionChanged;
            this.dgChiTietChuyenHangDonHang.DataContext = collection;

            this.dgChiTietChuyenHangDonHang.UpdateLayout();
        }

        void collection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var tChiTietChuyenHangDonHang = e.NewItems[0] as PhuDinhData.tChiTietChuyenHangDonHang;
                tChiTietChuyenHangDonHang.tChuyenHangDonHangList = _tChuyenHangDonHangs;
                tChiTietChuyenHangDonHang.tMatHangList = _tMatHangs;
            }
        }
        #endregion
    }
}
