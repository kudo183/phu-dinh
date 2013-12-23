using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for rNguyenLieuView.xaml
    /// </summary>
    public partial class rNguyenLieuView : BaseView<PhuDinhData.rNguyenLieu>
    {
        public Filter_rNguyenLieu FilterNguyenLieu { get; set; }
        public Expression<Func<PhuDinhData.rLoaiNguyenLieu, bool>> FilterLoaiNguyenLieu { get; set; }
        public PhuDinhData.rLoaiNguyenLieu rLoaiNguyenLieuDefault { get; set; }

        private ObservableCollection<PhuDinhData.rNguyenLieu> _rNguyenLieus;
        private List<PhuDinhData.rLoaiNguyenLieu> _rLoaiNguyenLieus;
        private PhuDinhData.PhuDinhEntities _context = ContextFactory.CreateContext();

        private List<PhuDinhData.rNguyenLieu> _origData;

        private string _filterLoaiNguyenLieu = string.Empty;

        public rNguyenLieuView()
        {
            InitializeComponent();

            FilterNguyenLieu = new Filter_rNguyenLieu();
            FilterLoaiNguyenLieu = (p => true);

            Loaded += rNguyenLieuView_Loaded;
            Unloaded += rNguyenLieuView_Unloaded;
        }

        void rNguyenLieuView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            DataGridColumnHeaderTextFilter.NguyenLieu_LoaiNguyenLieu.Text = _filterLoaiNguyenLieu;
            DataGridColumnHeaderTextFilter.NguyenLieu_LoaiNguyenLieu.PropertyChanged += NguyenLieu_LoaiNguyenLieu_PropertyChanged;
        }

        void rNguyenLieuView_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            _filterLoaiNguyenLieu = DataGridColumnHeaderTextFilter.NguyenLieu_LoaiNguyenLieu.Text;
            DataGridColumnHeaderTextFilter.NguyenLieu_LoaiNguyenLieu.PropertyChanged -= NguyenLieu_LoaiNguyenLieu_PropertyChanged;
        }

        void NguyenLieu_LoaiNguyenLieu_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(DataGridColumnHeaderTextFilter.NguyenLieu_LoaiNguyenLieu.Text) == false)
            {
                FilterNguyenLieu.FilterLoaiNguyenLieu = (p => p.rLoaiNguyenLieu.TenLoai.Contains(DataGridColumnHeaderTextFilter.NguyenLieu_LoaiNguyenLieu.Text));
            }
            else
            {
                FilterNguyenLieu.FilterLoaiNguyenLieu = (p => true);
            }

            RefreshView();
        }

        #region Override base view method
        public override void CommitEdit()
        {
            dgNguyenLieu.CommitEdit();
            base.CommitEdit();
        }

        public override void Save()
        {
            CommitEdit();
            try
            {
                if (FilterNguyenLieu == null)
                {
                    return;
                }

                var data = dgNguyenLieu.DataContext as ObservableCollection<PhuDinhData.rNguyenLieu>;
                PhuDinhData.Repository.rNguyenLieuRepository.Save(_context, data.ToList(), _origData);
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
            _origData = PhuDinhData.Repository.rNguyenLieuRepository.GetData(_context, FilterNguyenLieu.FilterNguyenLieu);

            _rNguyenLieus = new ObservableCollection<PhuDinhData.rNguyenLieu>(_origData);
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
            CommitEdit();

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
