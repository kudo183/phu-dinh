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
    /// Interaction logic for rKhachHangChanhView.xaml
    /// </summary>
    public partial class rKhachHangChanhView : BaseView
    {
        public Filter_rKhachHangChanh FilterKhachHangChanh { get; set; }
        public Expression<Func<PhuDinhData.rKhachHang, bool>> FilterKhachHang { get; set; }
        public Expression<Func<PhuDinhData.rChanh, bool>> FilterChanh { get; set; }
        public PhuDinhData.rKhachHang rKhachHangDefault { get; set; }

        private List<PhuDinhData.rKhachHang> _rKhachHangs;
        private List<PhuDinhData.rChanh> _rChanhs;

        private ObservableCollection<PhuDinhData.rKhachHangChanh> _rKhachHangChanhs;
        private PhuDinhData.PhuDinhEntities _context = ContextFactory.CreateContext();

        private string _filterKhachHang = string.Empty;

        private string _filterChanh = string.Empty;

        public rKhachHangChanhView()
        {
            InitializeComponent();

            FilterKhachHangChanh = new Filter_rKhachHangChanh();
            FilterKhachHang = (p => true);
            FilterChanh = (p => true);

            Loaded += rKhachHangChanhView_Loaded;
            Unloaded += rKhachHangChanhView_Unloaded;
        }

        void rKhachHangChanhView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            DataGridColumnHeaderTextFilter.KhachHangChanh_KhachHang.Text = _filterKhachHang;
            DataGridColumnHeaderTextFilter.KhachHangChanh_KhachHang.PropertyChanged += KhachHangChanh_KhachHang_PropertyChanged;

            DataGridColumnHeaderTextFilter.KhachHangChanh_Chanh.Text = _filterChanh;
            DataGridColumnHeaderTextFilter.KhachHangChanh_Chanh.PropertyChanged += KhachHangChanh_Chanh_PropertyChanged;

            RefreshView();
        }

        void rKhachHangChanhView_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            DataGridColumnHeaderTextFilter.KhachHangChanh_KhachHang.PropertyChanged -= KhachHangChanh_KhachHang_PropertyChanged;
            _filterKhachHang = DataGridColumnHeaderTextFilter.KhachHangChanh_KhachHang.Text;

            DataGridColumnHeaderTextFilter.KhachHangChanh_KhachHang.PropertyChanged -= KhachHangChanh_Chanh_PropertyChanged;
            _filterChanh = DataGridColumnHeaderTextFilter.KhachHangChanh_KhachHang.Text;
        }

        void KhachHangChanh_KhachHang_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

            if (string.IsNullOrEmpty(DataGridColumnHeaderTextFilter.KhachHangChanh_KhachHang.Text) == false)
            {
                FilterKhachHangChanh.FilterKhachHang = (p => p.rKhachHang.TenKhachHang.Contains(DataGridColumnHeaderTextFilter.KhachHangChanh_KhachHang.Text));
            }
            else
            {
                FilterKhachHangChanh.FilterKhachHang = (p => true);
            }

            RefreshView();
        }

        void KhachHangChanh_Chanh_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(DataGridColumnHeaderTextFilter.KhachHangChanh_Chanh.Text) == false)
            {
                FilterKhachHangChanh.FilterChanh = (p => p.rChanh.TenChanh.Contains(DataGridColumnHeaderTextFilter.KhachHangChanh_Chanh.Text));
            }
            else
            {
                FilterKhachHangChanh.FilterChanh = (p => true);
            }

            RefreshView();
        }

        #region Override base view method
        public override void CommitEdit()
        {
            dgKhachHangChanh.CommitEdit();
            base.CommitEdit();
        }

        public override void Save()
        {
            CommitEdit();
            try
            {
                if (FilterKhachHangChanh == null)
                {
                    return;
                }

                var data = dgKhachHangChanh.DataContext as ObservableCollection<PhuDinhData.rKhachHangChanh>;
                PhuDinhData.Repository.rKhachHangChanhRepository.Save(_context, data.ToList(), FilterKhachHangChanh.FilterKhachHangChanh);
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
            if (FilterKhachHangChanh == null)
            {
                dgKhachHangChanh.DataContext = null;
                return;
            }

            var index = dgKhachHangChanh.SelectedIndex;

            if (_rKhachHangChanhs != null)
            {
                _rKhachHangChanhs.CollectionChanged -= collection_CollectionChanged;
            }

            _context = ContextFactory.CreateContext();
            var rKhachHangChanhs = PhuDinhData.Repository.rKhachHangChanhRepository.GetData(_context, FilterKhachHangChanh.FilterKhachHangChanh);

            _rKhachHangChanhs = new ObservableCollection<PhuDinhData.rKhachHangChanh>(rKhachHangChanhs);
            _rKhachHangChanhs.CollectionChanged += collection_CollectionChanged;

            UpdateChanhReferenceData();
            UpdateKhachHangReferenceData();

            dgKhachHangChanh.DataContext = _rKhachHangChanhs;

            dgKhachHangChanh.SelectedIndex = index;
        }

        void collection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var rKhachHangChanh = e.NewItems[0] as PhuDinhData.rKhachHangChanh;
                rKhachHangChanh.rChanhList = _rChanhs;
                rKhachHangChanh.rKhachHangList = _rKhachHangs;

                if (rKhachHangDefault != null)
                {
                    rKhachHangChanh.MaKhachHang = rKhachHangDefault.Ma;
                }
            }
        }

        #endregion

        private void dgKhachHangChanh_HeaderAddButtonClick(object sender, EventArgs e)
        {
            CommitEdit();

            var header = sender as DataGridColumnHeader;

            BaseView view = null;

            switch (header.Content.ToString())
            {
                case "Khách hàng":
                    view = new rKhachHangView();
                    view.RefreshView();
                    ChildWindowUtils.ShowChildWindow("Khách hàng", view);

                    UpdateKhachHangReferenceData();
                    break;
                case "Chành":
                    view = new rChanhView();
                    view.RefreshView();
                    ChildWindowUtils.ShowChildWindow("Chành", view);

                    UpdateChanhReferenceData();
                    break;
            }
        }

        private void UpdateChanhReferenceData()
        {
            _rChanhs = PhuDinhData.Repository.rChanhRepository.GetData(_context, FilterChanh);

            if (_rKhachHangChanhs == null)
            {
                return;
            }

            foreach (var rKhachHangChanh in _rKhachHangChanhs)
            {
                rKhachHangChanh.rChanhList = _rChanhs;
            }
        }

        private void UpdateKhachHangReferenceData()
        {
            _rKhachHangs = PhuDinhData.Repository.rKhachHangRepository.GetData(_context, FilterKhachHang);

            if (_rKhachHangChanhs == null)
            {
                return;
            }

            foreach (var rKhachHangChanh in _rKhachHangChanhs)
            {
                rKhachHangChanh.rKhachHangList = _rKhachHangs;
            }
        }
    }
}
