using System;
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
    /// Interaction logic for tChiTietDonHangView.xaml
    /// </summary>
    public partial class tChiTietDonHangView : BaseView
    {
        public Expression<Func<PhuDinhData.tChiTietDonHang, bool>> FilterChiTietDonHang { get; set; }
        public Expression<Func<PhuDinhData.tDonHang, bool>> FilterDonHang { get; set; }
        public Expression<Func<PhuDinhData.tMatHang, bool>> FilterMatHang { get; set; }
        public PhuDinhData.tDonHang tDonHangDefault { get; set; }

        private ObservableCollection<PhuDinhData.tChiTietDonHang> _tChiTietDonHangs;
        private List<PhuDinhData.tDonHang> _tDonHangs;
        private List<PhuDinhData.tMatHang> _tMatHangs;
        private PhuDinhData.PhuDinhEntities _context = ContextFactory.CreateContext();

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
                if (FilterChiTietDonHang == null)
                {
                    return;
                }

                var data = dgChiTietDonHang.DataContext as ObservableCollection<PhuDinhData.tChiTietDonHang>;
                PhuDinhData.Repository.tChiTietDonHangRepository.Save(_context, data.ToList(), FilterChiTietDonHang);
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
            if (FilterChiTietDonHang == null)
            {
                dgChiTietDonHang.DataContext = null;
                return;
            }

            var index = dgChiTietDonHang.SelectedIndex;

            if (_tChiTietDonHangs != null)
            {
                _tChiTietDonHangs.CollectionChanged -= collection_CollectionChanged;
            }

            _context = ContextFactory.CreateContext();
            var tChiTietDonHangs = PhuDinhData.Repository.tChiTietDonHangRepository.GetData(_context, FilterChiTietDonHang);

            _tChiTietDonHangs = new ObservableCollection<PhuDinhData.tChiTietDonHang>(tChiTietDonHangs);
            _tChiTietDonHangs.CollectionChanged += collection_CollectionChanged;

            UpdateDonHangReferenceData();
            UpdateMatHangReferenceData();

            dgChiTietDonHang.DataContext = _tChiTietDonHangs;

            dgChiTietDonHang.SelectedIndex = index;
        }

        void collection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var tChiTietDonHang = e.NewItems[0] as PhuDinhData.tChiTietDonHang;
                tChiTietDonHang.tDonHangList = _tDonHangs;
                tChiTietDonHang.tMatHangList = _tMatHangs;

                if (tDonHangDefault != null)
                {
                    tChiTietDonHang.MaDonHang = tDonHangDefault.Ma;
                }
            }
        }
        #endregion

        private void dgChiTietDonHang_HeaderAddButtonClick(object sender, EventArgs e)
        {
            dgChiTietDonHang.CommitEdit();

            var header = sender as DataGridColumnHeader;

            BaseView view = null;

            switch (header.Content.ToString())
            {
                case "Đơn hàng":
                    view = new tDonHangView();
                    view.RefreshView();
                    ChildWindowUtils.ShowChildWindow("Đơn hàng", view);

                    UpdateDonHangReferenceData();
                    break;
                case "Mặt Hàng":
                    view = new tMatHangView();
                    view.RefreshView();
                    ChildWindowUtils.ShowChildWindow("Mặt Hàng", view);

                    UpdateMatHangReferenceData();
                    break;
            }
        }

        private void UpdateDonHangReferenceData()
        {
            _tDonHangs = PhuDinhData.Repository.tDonHangRepository.GetData(_context, FilterDonHang);

            if (_tChiTietDonHangs == null)
            {
                return;
            }

            foreach (var tChiTietDonHang in _tChiTietDonHangs)
            {
                tChiTietDonHang.tDonHangList = _tDonHangs;
            }
        }

        private void UpdateMatHangReferenceData()
        {
            _tMatHangs = PhuDinhData.Repository.tMatHangRepository.GetData(_context, FilterMatHang);

            if (_tChiTietDonHangs == null)
            {
                return;
            }

            foreach (var tChiTietDonHang in _tChiTietDonHangs)
            {
                tChiTietDonHang.tMatHangList = _tMatHangs;
            }
        }
    }
}
