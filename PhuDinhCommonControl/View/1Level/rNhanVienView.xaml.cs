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
    /// Interaction logic for View.xaml
    /// </summary>
    public partial class rNhanVienView : BaseView<PhuDinhData.rNhanVien>
    {
        public Filter_rNhanVien FilterNhanVien { get; set; }
        public Expression<Func<PhuDinhData.rPhuongTien, bool>> FilterPhuongTien { get; set; }
        public PhuDinhData.rPhuongTien rPhuongTienDefault { get; set; }

        private ObservableCollection<PhuDinhData.rNhanVien> _rNhanViens;
        private List<PhuDinhData.rPhuongTien> _rPhuongTiens;
        private PhuDinhData.PhuDinhEntities _context = ContextFactory.CreateContext();

        private List<PhuDinhData.rNhanVien> _origData;

        private string _filterPhuongTien = string.Empty;

        public rNhanVienView()
        {
            InitializeComponent();

            FilterPhuongTien = (p => true);
            FilterNhanVien = new Filter_rNhanVien();

            Loaded += rNhanVienView_Loaded;
            Unloaded += rNhanVienView_Unloaded;
        }

        void rNhanVienView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            DataGridColumnHeaderTextFilter.NhanVien_PhuongTien.Text = _filterPhuongTien;
            DataGridColumnHeaderTextFilter.NhanVien_PhuongTien.PropertyChanged += NhanVien_PhuongTien_PropertyChanged;
        }

        void rNhanVienView_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            _filterPhuongTien = DataGridColumnHeaderTextFilter.NhanVien_PhuongTien.Text;
            DataGridColumnHeaderTextFilter.NhanVien_PhuongTien.PropertyChanged -= NhanVien_PhuongTien_PropertyChanged;
        }

        void NhanVien_PhuongTien_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(DataGridColumnHeaderTextFilter.NhanVien_PhuongTien.Text) == false)
            {
                FilterNhanVien.FilterPhuongTien = (p => p.rPhuongTien.TenPhuongTien.Contains(DataGridColumnHeaderTextFilter.NhanVien_PhuongTien.Text));
            }
            else
            {
                FilterNhanVien.FilterPhuongTien = (p => true);
            }

            RefreshView();
        }

        #region Override base view method
        public override void CommitEdit()
        {
            dgNhanVien.CommitEdit();
            base.CommitEdit();
        }

        public override void Save()
        {
            CommitEdit();
            try
            {
                if (FilterNhanVien == null)
                {
                    return;
                }

                var data = dgNhanVien.DataContext as ObservableCollection<PhuDinhData.rNhanVien>;
                PhuDinhData.Repository.rNhanVienRepository.Save(_context, data.ToList(), _origData);
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
            if (FilterNhanVien == null)
            {
                dgNhanVien.DataContext = null;
                return;
            }

            var index = dgNhanVien.SelectedIndex;

            if (_rNhanViens != null)
            {
                _rNhanViens.CollectionChanged -= collection_CollectionChanged;
            }

            _context = ContextFactory.CreateContext();
            _origData = PhuDinhData.Repository.rNhanVienRepository.GetData(_context, FilterNhanVien.FilterPhuongTien);

            _rNhanViens = new ObservableCollection<PhuDinhData.rNhanVien>(_origData);
            _rNhanViens.CollectionChanged += collection_CollectionChanged;

            UpdatePhuongTienReferenceData();

            dgNhanVien.DataContext = _rNhanViens;

            dgNhanVien.SelectedIndex = index;
        }

        void collection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var nhanVienGiaoHang = e.NewItems[0] as PhuDinhData.rNhanVien;
                nhanVienGiaoHang.rPhuongTienList = _rPhuongTiens;

                if (rPhuongTienDefault != null)
                {
                    nhanVienGiaoHang.MaPhuongTien = rPhuongTienDefault.Ma;
                }
            }
        }
        #endregion

        private void dgNhanVien_HeaderAddButtonClick(object sender, EventArgs e)
        {
            CommitEdit();

            var view = new rPhuongTienView();
            view.RefreshView();
            ChildWindowUtils.ShowChildWindow("Phương Tiện", view);

            UpdatePhuongTienReferenceData();
        }

        private void UpdatePhuongTienReferenceData()
        {
            _rPhuongTiens = PhuDinhData.Repository.rPhuongTienRepository.GetData(_context, FilterPhuongTien);

            if (_rNhanViens == null)
            {
                return;
            }

            foreach (var rNhanVien in _rNhanViens)
            {
                rNhanVien.rPhuongTienList = _rPhuongTiens;
            }
        }
    }
}
