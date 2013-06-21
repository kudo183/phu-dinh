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
    /// Interaction logic for tChuyenHangDonHangView.xaml
    /// </summary>
    public partial class tChuyenHangDonHangView : BaseView
    {
        public Expression<Func<PhuDinhData.tChuyenHangDonHang, bool>> FilterChuyenHangDonHang { get; set; }
        public Expression<Func<PhuDinhData.tChuyenHang, bool>> FilterChuyenHang { get; set; }
        public Expression<Func<PhuDinhData.tDonHang, bool>> FilterDonHang { get; set; }

        private List<PhuDinhData.tChuyenHangDonHang> _tChuyenHangDonHangs;
        private List<PhuDinhData.tChuyenHang> _tChuyenHangs;
        private List<PhuDinhData.tDonHang> _tDonHangs;
        private readonly PhuDinhData.PhuDinhEntities _context = new PhuDinhData.PhuDinhEntities();

        public tChuyenHangDonHangView()
        {
            InitializeComponent();

            FilterChuyenHangDonHang = (p => true);
            FilterChuyenHang = (p => true);
            FilterDonHang = (p => true);
        }

        #region Override base view method
        public override void Save()
        {
            try
            {
                var data = dgChuyenHangDonHang.DataContext as ObservableCollection<PhuDinhData.tChuyenHangDonHang>;
                PhuDinhData.Repository.tChuyenHangDonHangRepository.Save(_context, data.ToList(), FilterChuyenHangDonHang);
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
            var collection = new ObservableCollection<PhuDinhData.tChuyenHangDonHang>(_tChuyenHangDonHangs);
            collection.CollectionChanged += collection_CollectionChanged;
            dgChuyenHangDonHang.DataContext = collection;

            UpdateChuyenHangReferenceData();
            UpdateDonHangReferenceData();

            dgChuyenHangDonHang.UpdateLayout();
        }

        void collection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var tChuyenHangDonHang = e.NewItems[0] as PhuDinhData.tChuyenHangDonHang;
                tChuyenHangDonHang.tChuyenHangList = _tChuyenHangs;
                tChuyenHangDonHang.tDonHangList = _tDonHangs;
            }
        }
        #endregion

        private void dgChuyenHangDonHang_HeaderAddButtonClick(object sender, EventArgs e)
        {
            var header = sender as DataGridColumnHeader;

            BaseView view = null;
            Window w = null;

            switch (header.Content.ToString())
            {
                case "Chuyến hàng":
                    view = new tChuyenHangView();
                    view.RefreshView();
                    w = new Window { Title = "Chuyến hàng", Content = view };
                    w.ShowDialog();

                    UpdateChuyenHangReferenceData();
                    break;
                case "Đơn Hàng":
                    view = new tDonHangView();
                    view.RefreshView();
                    w = new Window { Title = "Đơn Hàng", Content = view };
                    w.ShowDialog();

                    UpdateDonHangReferenceData();
                    break;
            }
        }

        private void UpdateChuyenHangReferenceData()
        {
            _tChuyenHangs = PhuDinhData.Repository.tChuyenHangRepository.GetData(_context, FilterChuyenHang);
            foreach (var tChuyenHangDonHang in _tChuyenHangDonHangs)
            {
                tChuyenHangDonHang.tChuyenHangList = _tChuyenHangs;
            }
        }

        private void UpdateDonHangReferenceData()
        {
            _tDonHangs = PhuDinhData.Repository.tDonHangRepository.GetData(_context, FilterDonHang);
            foreach (var tChuyenHangDonHang in _tChuyenHangDonHangs)
            {
                tChuyenHangDonHang.tDonHangList = _tDonHangs;
            }
        }
    }
}
