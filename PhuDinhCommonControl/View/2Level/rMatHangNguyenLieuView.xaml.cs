using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for rMatHangNguyenLieuView.xaml
    /// </summary>
    public partial class rMatHangNguyenLieuView : BaseView<PhuDinhData.rMatHangNguyenLieu>
    {
        public Expression<Func<PhuDinhData.rMatHangNguyenLieu, bool>> FilterMatHangNguyenLieu { get; set; }
        public Expression<Func<PhuDinhData.tMatHang, bool>> FiltetMatHang { get; set; }
        public Expression<Func<PhuDinhData.rNguyenLieu, bool>> FilterNguyenLieu { get; set; }
        public PhuDinhData.tMatHang tMatHangDefault { get; set; }
        public PhuDinhData.rNguyenLieu rNguyenLieuDefault { get; set; }

        private ObservableCollection<PhuDinhData.rMatHangNguyenLieu> _rMatHangNguyenLieus;
        private List<PhuDinhData.tMatHang> _tMatHangs;
        private List<PhuDinhData.rNguyenLieu> _rNguyenLieus;

        private PhuDinhData.PhuDinhEntities _context = ContextFactory.CreateContext();

        private List<PhuDinhData.rMatHangNguyenLieu> _origData;

        public rMatHangNguyenLieuView()
        {
            InitializeComponent();

            FilterMatHangNguyenLieu = (p => true);
            FiltetMatHang = (p => true);
            FilterNguyenLieu = (p => true);
        }

        #region Override base view method
        public override void CommitEdit()
        {
            dgMatHangNguyenLieu.CommitEdit();
            base.CommitEdit();
        }

        public override void Save()
        {
            try
            {
                if (FilterMatHangNguyenLieu == null)
                {
                    return;
                }

                var data = dgMatHangNguyenLieu.DataContext as ObservableCollection<PhuDinhData.rMatHangNguyenLieu>;
                PhuDinhData.Repository.rMatHangNguyenLieuRepository.Save(_context, data.ToList(), _origData);
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
            if (FilterMatHangNguyenLieu == null)
            {
                dgMatHangNguyenLieu.DataContext = null;
                return;
            }

            var index = dgMatHangNguyenLieu.SelectedIndex;

            if (_rMatHangNguyenLieus != null)
            {
                _rMatHangNguyenLieus.CollectionChanged -= collection_CollectionChanged;
            }

            _context = ContextFactory.CreateContext();
            _origData = PhuDinhData.Repository.rMatHangNguyenLieuRepository.GetData(_context, FilterMatHangNguyenLieu);

            _rMatHangNguyenLieus = new ObservableCollection<PhuDinhData.rMatHangNguyenLieu>(_origData);
            _rMatHangNguyenLieus.CollectionChanged += collection_CollectionChanged;

            UpdateNhanVienGiaoHangReferenceData();
            UpdatetMatHangReferenceData();

            dgMatHangNguyenLieu.DataContext = _rMatHangNguyenLieus;

            dgMatHangNguyenLieu.SelectedIndex = index;
        }

        void collection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var rMatHangNguyenLieu = e.NewItems[0] as PhuDinhData.rMatHangNguyenLieu;
                rMatHangNguyenLieu.tMatHangList = _tMatHangs;
                rMatHangNguyenLieu.rNguyenLieuList = _rNguyenLieus;

                if (tMatHangDefault != null)
                {
                    rMatHangNguyenLieu.MaMatHang = tMatHangDefault.Ma;
                }

                if (rNguyenLieuDefault != null)
                {
                    rMatHangNguyenLieu.MaNguyenLieu = rNguyenLieuDefault.Ma;
                }
            }
        }

        #endregion

        private void dgMatHangNguyenLieu_HeaderAddButtonClick(object sender, EventArgs e)
        {
            CommitEdit();

            var header = sender as DataGridColumnHeader;

            UserControl view = null;

            switch (header.Content.ToString())
            {
                case "Nguyên Liệu":
                    view = new rNguyenLieuView();
                    ChildWindowUtils.ShowChildWindow("Nguyên Liệu", view);

                    UpdateNhanVienGiaoHangReferenceData();
                    break;
                case "MặtHàng":
                    view = new tMatHangView();
                    ChildWindowUtils.ShowChildWindow("Mặt Hàng", view);

                    UpdatetMatHangReferenceData();
                    break;
            }
        }

        private void UpdatetMatHangReferenceData()
        {
            _tMatHangs = PhuDinhData.Repository.tMatHangRepository.GetData(_context, FiltetMatHang);

            if (_rMatHangNguyenLieus == null)
            {
                return;
            }

            foreach (var rMatHangNguyenLieu in _rMatHangNguyenLieus)
            {
                rMatHangNguyenLieu.tMatHangList = _tMatHangs;
            }
        }

        private void UpdateNhanVienGiaoHangReferenceData()
        {
            _rNguyenLieus = PhuDinhData.Repository.rNguyenLieuRepository.GetData(_context, FilterNguyenLieu);

            if (_rMatHangNguyenLieus == null)
            {
                return;
            }

            foreach (var rMatHangNguyenLieu in _rMatHangNguyenLieus)
            {
                rMatHangNguyenLieu.rNguyenLieuList = _rNguyenLieus;
            }
        }
    }
}
