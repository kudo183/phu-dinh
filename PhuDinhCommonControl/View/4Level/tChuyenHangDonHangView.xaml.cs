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
    /// Interaction logic for tChuyenHangDonHangView.xaml
    /// </summary>
    public partial class tChuyenHangDonHangView : BaseView
    {
        public Expression<Func<PhuDinhData.tChuyenHangDonHang, bool>> FilterChuyenHangDonHang { get; set; }
        public Expression<Func<PhuDinhData.tChuyenHang, bool>> FilterChuyenHang { get; set; }
        public Expression<Func<PhuDinhData.tDonHang, bool>> FilterDonHang { get; set; }

        public PhuDinhData.tChuyenHang tChuyenHangDefault { get; set; }
        public PhuDinhData.tDonHang tDonHangDefault { get; set; }
        
        private ObservableCollection<PhuDinhData.tChuyenHangDonHang> _tChuyenHangDonHangs;
        private List<PhuDinhData.tChuyenHang> _tChuyenHangs;
        private List<PhuDinhData.tDonHang> _tDonHangs;
        private PhuDinhData.PhuDinhEntities _context = new PhuDinhData.PhuDinhEntities();

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
                if (FilterChuyenHangDonHang == null)
                {
                    return;
                }

                var data = dgChuyenHangDonHang.DataContext as ObservableCollection<PhuDinhData.tChuyenHangDonHang>;
                PhuDinhData.Repository.tChuyenHangDonHangRepository.Save(_context, data.ToList(), FilterChuyenHangDonHang);
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
            if (FilterChuyenHangDonHang == null)
            {
                dgChuyenHangDonHang.DataContext = null;
                return;
            }

            var index = dgChuyenHangDonHang.SelectedIndex;

            if (_tChuyenHangDonHangs != null)
            {
                _tChuyenHangDonHangs.CollectionChanged -= collection_CollectionChanged;
            }

            _context = new PhuDinhData.PhuDinhEntities();
            var tChuyenHangDonHangs = PhuDinhData.Repository.tChuyenHangDonHangRepository.GetData(_context, FilterChuyenHangDonHang);

            _tChuyenHangDonHangs = new ObservableCollection<PhuDinhData.tChuyenHangDonHang>(tChuyenHangDonHangs);
            _tChuyenHangDonHangs.CollectionChanged += collection_CollectionChanged;
            dgChuyenHangDonHang.DataContext = _tChuyenHangDonHangs;

            UpdateChuyenHangReferenceData();
            UpdateDonHangReferenceData();

            dgChuyenHangDonHang.UpdateLayout();

            dgChuyenHangDonHang.SelectedIndex = index;
        }

        void collection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var tChuyenHangDonHang = e.NewItems[0] as PhuDinhData.tChuyenHangDonHang;
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
        #endregion

        private void dgChuyenHangDonHang_HeaderAddButtonClick(object sender, EventArgs e)
        {
            dgChuyenHangDonHang.CommitEdit();

            var header = sender as DataGridColumnHeader;

            BaseView view = null;

            switch (header.Content.ToString())
            {
                case "Chuyến hàng":
                    view = new tChuyenHangView();
                    view.RefreshView();
                    ChildWindowUtils.ShowChildWindow("Chuyến hàng", view);

                    UpdateChuyenHangReferenceData();
                    break;
                case "Đơn Hàng":
                    view = new tDonHangView();
                    view.RefreshView();
                    ChildWindowUtils.ShowChildWindow("Đơn Hàng", view);

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
