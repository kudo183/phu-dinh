using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for rNguyenLieuView.xaml
    /// </summary>
    public partial class rNguyenLieuView : BaseView
    {
        public Expression<Func<PhuDinhData.rNguyenLieu, bool>> FilterNguyenLieu { get; set; }
        public Expression<Func<PhuDinhData.rLoaiNguyenLieu, bool>> FilterLoaiNguyenLieu { get; set; }
        public PhuDinhData.rLoaiNguyenLieu rLoaiNguyenLieuDefault { get; set; }

        private ObservableCollection<PhuDinhData.rNguyenLieu> _rNguyenLieus;
        private List<PhuDinhData.rLoaiNguyenLieu> _rLoaiNguyenLieus;
        private PhuDinhData.PhuDinhEntities _context = ContextFactory.CreateContext();

        public rNguyenLieuView()
        {
            InitializeComponent();

            FilterNguyenLieu = (p => true);
            FilterLoaiNguyenLieu = (p => true);
        }

        #region Override base view method
        public override void Save()
        {
            try
            {
                if (FilterNguyenLieu == null)
                {
                    return;
                }

                var data = dgNguyenLieu.DataContext as ObservableCollection<PhuDinhData.rNguyenLieu>;
                PhuDinhData.Repository.rNguyenLieuRepository.Save(_context, data.ToList(), FilterNguyenLieu);
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
            if (FilterNguyenLieu == null)
            {
                dgNguyenLieu.DataContext = null;
                return;
            }

            var index = dgNguyenLieu.SelectedIndex;

            if (_rNguyenLieus != null)
            {
                _rNguyenLieus.CollectionChanged -= collection_CollectionChanged;
            }

            _context = ContextFactory.CreateContext();
            var rNguyenLieus = PhuDinhData.Repository.rNguyenLieuRepository.GetData(_context, FilterNguyenLieu);

            _rNguyenLieus = new ObservableCollection<PhuDinhData.rNguyenLieu>(rNguyenLieus);
            _rNguyenLieus.CollectionChanged += collection_CollectionChanged;

            UpdateLoaiNguyenLieuReferenceData();

            dgNguyenLieu.DataContext = _rNguyenLieus;

            dgNguyenLieu.SelectedIndex = index;
        }

        void collection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var NguyenLieu = e.NewItems[0] as PhuDinhData.rNguyenLieu;
                NguyenLieu.rLoaiNguyenLieuList = _rLoaiNguyenLieus;

                if (rLoaiNguyenLieuDefault != null)
                {
                    NguyenLieu.MaLoaiNguyenLieu = rLoaiNguyenLieuDefault.Ma;
                }
            }
        }

        #endregion

        private void dgNguyenLieu_HeaderAddButtonClick(object sender, EventArgs e)
        {
            dgNguyenLieu.CommitEdit();

            var view = new rLoaiNguyenLieuView();
            view.RefreshView();
            ChildWindowUtils.ShowChildWindow("Loại Nguyên Liệu", view);

            UpdateLoaiNguyenLieuReferenceData();
        }

        private void UpdateLoaiNguyenLieuReferenceData()
        {
            _rLoaiNguyenLieus = PhuDinhData.Repository.rLoaiNguyenLieuRepository.GetData(_context, FilterLoaiNguyenLieu);

            if (_rNguyenLieus == null)
            {
                return;
            }

            foreach (var rNguyenLieu in _rNguyenLieus)
            {
                rNguyenLieu.rLoaiNguyenLieuList = _rLoaiNguyenLieus;
            }
        }
    }
}
