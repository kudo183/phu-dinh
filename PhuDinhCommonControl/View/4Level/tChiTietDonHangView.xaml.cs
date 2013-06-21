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
    /// Interaction logic for tChiTietDonHangView.xaml
    /// </summary>
    public partial class tChiTietDonHangView : BaseView
    {
        public Expression<Func<PhuDinhData.tChiTietDonHang, bool>> FilterChiTietDonHang { get; set; }
        public Expression<Func<PhuDinhData.tDonHang, bool>> FilterDonHang { get; set; }
        public Expression<Func<PhuDinhData.tMatHang, bool>> FilterMatHang { get; set; }

        private List<PhuDinhData.tChiTietDonHang> _tChiTietDonHangs;
        private List<PhuDinhData.tDonHang> _tDonHangs;
        private List<PhuDinhData.tMatHang> _tMatHangs;
        private readonly PhuDinhData.PhuDinhEntities _context = new PhuDinhData.PhuDinhEntities();

        public tChiTietDonHangView()
        {
            InitializeComponent();

            FilterChiTietDonHang = (p => true);
            FilterDonHang = (p => true);
            FilterMatHang = (p => true);
        }

        #region Override base view method
        public override void Save()
        {
            try
            {
                var data = dgChiTietDonHang.DataContext as ObservableCollection<PhuDinhData.tChiTietDonHang>;
                PhuDinhData.Repository.tChiTietDonHangRepository.Save(_context, data.ToList(), FilterChiTietDonHang);
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
            _tChiTietDonHangs = PhuDinhData.Repository.tChiTietDonHangRepository.GetData(_context, FilterChiTietDonHang);
            var collection = new ObservableCollection<PhuDinhData.tChiTietDonHang>(_tChiTietDonHangs);
            collection.CollectionChanged += collection_CollectionChanged;
            dgChiTietDonHang.DataContext = collection;

            UpdateDonHangReferenceData();
            UpdateMatHangReferenceData();

            dgChiTietDonHang.UpdateLayout();
        }

        void collection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var tChiTietDonHang = e.NewItems[0] as PhuDinhData.tChiTietDonHang;
                tChiTietDonHang.tDonHangList = _tDonHangs;
                tChiTietDonHang.tMatHangList = _tMatHangs;
            }
        }
        #endregion

        private void dgChiTietDonHang_HeaderAddButtonClick(object sender, EventArgs e)
        {
            var header = sender as DataGridColumnHeader;

            BaseView view = null;
            Window w = null;

            switch (header.Content.ToString())
            {
                case "Đơn hàng":
                    view = new tDonHangView();
                    view.RefreshView();
                    w = new Window { Title = "Đơn hàng", Content = view };
                    w.ShowDialog();

                    UpdateDonHangReferenceData();
                    break;
                case "Mặt Hàng":
                    view = new tMatHangView();
                    view.RefreshView();
                    w = new Window { Title = "Mặt Hàng", Content = view };
                    w.ShowDialog();

                    UpdateMatHangReferenceData();
                    break;
            }
        }

        private void UpdateDonHangReferenceData()
        {
            _tDonHangs = PhuDinhData.Repository.tDonHangRepository.GetData(_context, FilterDonHang);
            foreach (var tChiTietDonHang in _tChiTietDonHangs)
            {
                tChiTietDonHang.tDonHangList = _tDonHangs;
            }
        }

        private void UpdateMatHangReferenceData()
        {
            _tMatHangs = PhuDinhData.Repository.tMatHangRepository.GetData(_context, FilterMatHang);
            foreach (var tChiTietDonHang in _tChiTietDonHangs)
            {
                tChiTietDonHang.tMatHangList = _tMatHangs;
            }
        }
    }
}
