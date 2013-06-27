using System;
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
    /// Interaction logic for tChiTietChuyenHangDonHangView.xaml
    /// </summary>
    public partial class tChiTietChuyenHangDonHangView : BaseView
    {
        public Expression<Func<PhuDinhData.tChiTietChuyenHangDonHang, bool>> FilterChiTietChuyenHangDonHang { get; set; }
        public Expression<Func<PhuDinhData.tChuyenHangDonHang, bool>> FilterChuyenHangDonHang { get; set; }
        public Expression<Func<PhuDinhData.tChiTietDonHang, bool>> FilterChiTietDonHang { get; set; }

        private List<PhuDinhData.tChiTietChuyenHangDonHang> _tChiTietChuyenHangDonHangs;
        private List<PhuDinhData.tChuyenHangDonHang> _tChuyenHangDonHangs;
        private List<PhuDinhData.tChiTietDonHang> _tChiTietDonHangs;
        private readonly PhuDinhData.PhuDinhEntities _context = new PhuDinhData.PhuDinhEntities();

        public tChiTietChuyenHangDonHangView()
        {
            InitializeComponent();
            FilterChiTietChuyenHangDonHang = (p => true);
            FilterChuyenHangDonHang = (p => true);
            FilterChiTietDonHang = (p => true);
        }

        #region Override base view method
        public override void Save()
        {
            try
            {
                var data = dgChiTietChuyenHangDonHang.DataContext as ObservableCollection<PhuDinhData.tChiTietChuyenHangDonHang>;
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
            _tChiTietChuyenHangDonHangs = PhuDinhData.Repository.tChiTietChuyenHangDonHangRepository.GetData(_context, FilterChiTietChuyenHangDonHang);
            var collection = new ObservableCollection<PhuDinhData.tChiTietChuyenHangDonHang>(_tChiTietChuyenHangDonHangs);
            collection.CollectionChanged += collection_CollectionChanged;
            dgChiTietChuyenHangDonHang.DataContext = collection;

            UpdateChiTietDonHangReferenceData();
            UpdateChuyenHangDonHangReferenceData();

            dgChiTietChuyenHangDonHang.UpdateLayout();
        }

        void collection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var tChiTietChuyenHangDonHang = e.NewItems[0] as PhuDinhData.tChiTietChuyenHangDonHang;
                tChiTietChuyenHangDonHang.tChuyenHangDonHangList = _tChuyenHangDonHangs;
                tChiTietChuyenHangDonHang.tChiTietDonHangList = _tChiTietDonHangs;
            }
        }
        #endregion

        private void dgChiTietChuyenHangDonHang_HeaderAddButtonClick(object sender, EventArgs e)
        {
            var header = sender as DataGridColumnHeader;

            BaseView view = null;
            Window w = null;

            switch (header.Content.ToString())
            {
                case "Chuyến hàng đơn hàng":
                    view = new tChuyenHangDonHangView();
                    view.RefreshView();
                    w = new Window { Title = "Chuyến hàng đơn hàng", Content = view };
                    w.ShowDialog();

                    UpdateChuyenHangDonHangReferenceData();
                    break;
                case "Chi tiết đơn hàng":
                    view = new tMatHangView();
                    view.RefreshView();
                    w = new Window { Title = "Chi tiết đơn hàng", Content = view };
                    w.ShowDialog();

                    UpdateChiTietDonHangReferenceData();
                    break;
            }
        }

        private void UpdateChuyenHangDonHangReferenceData()
        {
            _tChuyenHangDonHangs = PhuDinhData.Repository.tChuyenHangDonHangRepository.GetData(_context, FilterChuyenHangDonHang);
            foreach (var tChiTietChuyenHangDonHang in _tChiTietChuyenHangDonHangs)
            {
                tChiTietChuyenHangDonHang.tChuyenHangDonHangList = _tChuyenHangDonHangs;
            }
        }

        private void UpdateChiTietDonHangReferenceData()
        {
            _tChiTietDonHangs = PhuDinhData.Repository.tChiTietDonHangRepository.GetData(_context, FilterChiTietDonHang);
            foreach (var tChiTietChuyenHangDonHang in _tChiTietChuyenHangDonHangs)
            {
                tChiTietChuyenHangDonHang.tChiTietDonHangList = _tChiTietDonHangs;
            }
        }
    }
}
