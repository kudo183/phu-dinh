using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
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
        public PhuDinhData.tChuyenHangDonHang tChuyenHangDonHangDefault { get; set; }

        private ObservableCollection<PhuDinhData.tChiTietChuyenHangDonHang> _tChiTietChuyenHangDonHangs;
        private List<PhuDinhData.tChuyenHangDonHang> _tChuyenHangDonHangs;
        private List<PhuDinhData.tChiTietDonHang> _tChiTietDonHangs;
        private PhuDinhData.PhuDinhEntities _context = ContextFactory.CreateContext();

        private List<PhuDinhData.tChiTietChuyenHangDonHang> _origData;

        public tChiTietChuyenHangDonHangView()
        {
            InitializeComponent();
            FilterChiTietChuyenHangDonHang = (p => true);
            FilterChuyenHangDonHang = (p => true);
            FilterChiTietDonHang = (p => true);
        }

        #region Override base view method
        public override void CommitEdit()
        {
            dgChiTietChuyenHangDonHang.CommitEdit();
            base.CommitEdit();
        }

        public override void Save()
        {
            CommitEdit();
            try
            {
                if (FilterChiTietChuyenHangDonHang == null)
                {
                    return;
                }

                var data = dgChiTietChuyenHangDonHang.DataContext as ObservableCollection<PhuDinhData.tChiTietChuyenHangDonHang>;
                PhuDinhData.Repository.tChiTietChuyenHangDonHangRepository.Save(_context, data.ToList(), _origData);
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
            if (FilterChiTietChuyenHangDonHang == null)
            {
                dgChiTietChuyenHangDonHang.DataContext = null;
                return;
            }

            var index = dgChiTietChuyenHangDonHang.SelectedIndex;

            if (_tChiTietChuyenHangDonHangs != null)
            {
                _tChiTietChuyenHangDonHangs.CollectionChanged -= collection_CollectionChanged;
            }

            _context = ContextFactory.CreateContext();
            _origData = PhuDinhData.Repository.tChiTietChuyenHangDonHangRepository.GetData(_context, FilterChiTietChuyenHangDonHang);

            _tChiTietChuyenHangDonHangs = new ObservableCollection<PhuDinhData.tChiTietChuyenHangDonHang>(_origData);
            _tChiTietChuyenHangDonHangs.CollectionChanged += collection_CollectionChanged;

            UpdateChiTietDonHangReferenceData();
            UpdateChuyenHangDonHangReferenceData();

            dgChiTietChuyenHangDonHang.DataContext = _tChiTietChuyenHangDonHangs;

            dgChiTietChuyenHangDonHang.SelectedIndex = index;
        }

        void collection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var tChiTietChuyenHangDonHang = e.NewItems[0] as PhuDinhData.tChiTietChuyenHangDonHang;
                tChiTietChuyenHangDonHang.tChuyenHangDonHangList = _tChuyenHangDonHangs;
                tChiTietChuyenHangDonHang.tChiTietDonHangList = _tChiTietDonHangs;

                if (tChuyenHangDonHangDefault != null)
                {
                    tChiTietChuyenHangDonHang.MaChuyenHangDonHang = tChuyenHangDonHangDefault.Ma;
                }
            }
        }
        #endregion

        private void dgChiTietChuyenHangDonHang_HeaderAddButtonClick(object sender, EventArgs e)
        {
            CommitEdit();

            var header = sender as DataGridColumnHeader;

            BaseView view = null;

            switch (header.Content.ToString())
            {
                case "Chuyến hàng đơn hàng":
                    view = new tChuyenHangDonHangView();
                    view.RefreshView();
                    ChildWindowUtils.ShowChildWindow("Chuyến hàng đơn hàng", view);

                    UpdateChuyenHangDonHangReferenceData();
                    break;
                case "Chi tiết đơn hàng":
                    view = new tMatHangView();
                    view.RefreshView();
                    ChildWindowUtils.ShowChildWindow("Chi tiết đơn hàng", view);

                    UpdateChiTietDonHangReferenceData();
                    break;
            }
        }

        private void UpdateChuyenHangDonHangReferenceData()
        {
            _tChuyenHangDonHangs = PhuDinhData.Repository.tChuyenHangDonHangRepository.GetData(_context, FilterChuyenHangDonHang);

            if (_tChiTietChuyenHangDonHangs == null)
            {
                return;
            }

            foreach (var tChiTietChuyenHangDonHang in _tChiTietChuyenHangDonHangs)
            {
                tChiTietChuyenHangDonHang.tChuyenHangDonHangList = _tChuyenHangDonHangs;
            }
        }

        private void UpdateChiTietDonHangReferenceData()
        {
            _tChiTietDonHangs = PhuDinhData.Repository.tChiTietDonHangRepository.GetData(_context, FilterChiTietDonHang);

            if (_tChiTietChuyenHangDonHangs == null)
            {
                return;
            }

            foreach (var tChiTietChuyenHangDonHang in _tChiTietChuyenHangDonHangs)
            {
                tChiTietChuyenHangDonHang.tChiTietDonHangList = _tChiTietDonHangs;
            }
        }
    }
}
