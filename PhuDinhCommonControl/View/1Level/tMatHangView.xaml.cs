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
    /// Interaction logic for View.xaml
    /// </summary>
    public partial class tMatHangView : BaseView
    {
        public Filter_tMatHang FilterMatHang { get; set; }
        public Expression<Func<PhuDinhData.rLoaiHang, bool>> FilterLoaiHang { get; set; }
        public PhuDinhData.rLoaiHang rLoaiHangDefault { get; set; }

        private ObservableCollection<PhuDinhData.tMatHang> _tMatHangs;
        private List<PhuDinhData.rLoaiHang> _rLoaiHangs;
        private PhuDinhData.PhuDinhEntities _context = ContextFactory.CreateContext();

        private string _filterLoaiHang = string.Empty;

        public tMatHangView()
        {
            InitializeComponent();

            FilterMatHang = new Filter_tMatHang();
            FilterLoaiHang = (p => true);

            Loaded += tMatHangView_Loaded;
            Unloaded += tMatHangView_Unloaded;
        }

        void tMatHangView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            DataGridColumnHeaderTextFilter.MatHang_LoaiHang.Text = _filterLoaiHang;
            DataGridColumnHeaderTextFilter.MatHang_LoaiHang.PropertyChanged += MatHang_LoaiHang_PropertyChanged;
        }

        void tMatHangView_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            _filterLoaiHang = DataGridColumnHeaderTextFilter.MatHang_LoaiHang.Text;
            DataGridColumnHeaderTextFilter.MatHang_LoaiHang.PropertyChanged -= MatHang_LoaiHang_PropertyChanged;
        }

        void MatHang_LoaiHang_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(DataGridColumnHeaderTextFilter.MatHang_LoaiHang.Text) == false)
            {
                FilterMatHang.FilterLoai = (p => p.rLoaiHang.TenLoai.Contains(DataGridColumnHeaderTextFilter.MatHang_LoaiHang.Text));
            }
            else
            {
                FilterMatHang.FilterLoai = (p => true);
            }

            RefreshView();
        }

        #region Override base view method
        public override void CommitEdit()
        {
            dgMatHang.CommitEdit();
            base.CommitEdit();
        }

        public override void Save()
        {
            CommitEdit();
            try
            {
                if (FilterMatHang == null)
                {
                    return;
                }

                var data = dgMatHang.DataContext as ObservableCollection<PhuDinhData.tMatHang>;
                PhuDinhData.Repository.tMatHangRepository.Save(_context, data.ToList(), FilterMatHang.FiltetMatHang);
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
            if (FilterMatHang == null)
            {
                dgMatHang.DataContext = null;
                return;
            }

            var index = dgMatHang.SelectedIndex;

            if (_tMatHangs != null)
            {
                _tMatHangs.CollectionChanged -= collection_CollectionChanged;
            }

            _context = ContextFactory.CreateContext();
            var tMatHangs = PhuDinhData.Repository.tMatHangRepository.GetData(_context, FilterMatHang.FiltetMatHang);

            _tMatHangs = new ObservableCollection<PhuDinhData.tMatHang>(tMatHangs);
            _tMatHangs.CollectionChanged += collection_CollectionChanged;

            UpdateLoaiHangReferenceData();

            dgMatHang.DataContext = _tMatHangs;

            dgMatHang.SelectedIndex = index;
        }

        void collection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var tMatHang = e.NewItems[0] as PhuDinhData.tMatHang;
                tMatHang.rLoaiHangList = _rLoaiHangs;

                if (rLoaiHangDefault != null)
                {
                    tMatHang.MaLoai = rLoaiHangDefault.Ma;
                }
            }
        }

        #endregion

        private void dgMatHang_HeaderAddButtonClick(object sender, EventArgs e)
        {
            CommitEdit();

            var view = new rLoaiHangView();
            view.RefreshView();
            ChildWindowUtils.ShowChildWindow("Loại Hàng", view);

            UpdateLoaiHangReferenceData();
        }

        private void UpdateLoaiHangReferenceData()
        {
            _rLoaiHangs = PhuDinhData.Repository.rLoaiHangRepository.GetData(_context, FilterLoaiHang);

            if (_tMatHangs == null)
            {
                return;
            }

            foreach (var tMatHang in _tMatHangs)
            {
                tMatHang.rLoaiHangList = _rLoaiHangs;
            }

        }
    }
}
