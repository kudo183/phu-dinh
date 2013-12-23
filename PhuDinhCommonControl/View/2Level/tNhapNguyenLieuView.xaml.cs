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
    /// Interaction logic for tNhapNguyenLieuView.xaml
    /// </summary>
    public partial class tNhapNguyenLieuView : BaseView<PhuDinhData.tNhapNguyenLieu>
    {
        public Filter_tNhapNguyenLieu FilterChiPhi { get; set; }
        public Expression<Func<PhuDinhData.rNguyenLieu, bool>> FilterNguyenLieu { get; set; }
        public Expression<Func<PhuDinhData.rNhaCungCap, bool>> FilterNhaCungCap { get; set; }
        public PhuDinhData.rNguyenLieu rNguyenLieuDefault { get; set; }
        public PhuDinhData.rNhaCungCap rNhaCungCapDefault { get; set; }

        private ObservableCollection<PhuDinhData.tNhapNguyenLieu> _tNhapNguyenLieus;
        private List<PhuDinhData.rNguyenLieu> _rNguyenLieus;
        private List<PhuDinhData.rNhaCungCap> _rNhaCungCaps;

        private PhuDinhData.PhuDinhEntities _context = ContextFactory.CreateContext();

        private List<PhuDinhData.tNhapNguyenLieu> _origData;

        private bool _isUsedDateFilter = true;
        private DateTime _filterDate = DateTime.Now.Date;

        public tNhapNguyenLieuView()
        {
            InitializeComponent();

            FilterChiPhi = new Filter_tNhapNguyenLieu();
            FilterNguyenLieu = (p => true);
            FilterNhaCungCap = (p => true);

            Loaded += tNhapNguyenLieuView_Loaded;
            Unloaded += tNhapNguyenLieuView_Unloaded;
        }

        void tNhapNguyenLieuView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            DataGridColumnHeaderDateFilter.ChiPhi.Date = _filterDate;
            DataGridColumnHeaderDateFilter.ChiPhi.IsUsed = _isUsedDateFilter;
            DataGridColumnHeaderDateFilter.ChiPhi.PropertyChanged += ChiPhi_PropertyChanged;
            ChiPhi_PropertyChanged(null, null);
        }

        void tNhapNguyenLieuView_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            DataGridColumnHeaderDateFilter.ChiPhi.PropertyChanged -= ChiPhi_PropertyChanged;
            _filterDate = DataGridColumnHeaderDateFilter.ChiPhi.Date;
            _isUsedDateFilter = DataGridColumnHeaderDateFilter.ChiPhi.IsUsed;
        }

        void ChiPhi_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (DataGridColumnHeaderDateFilter.ChiPhi.IsUsed)
            {
                FilterChiPhi.FilterNgay = (p => p.Ngay == DataGridColumnHeaderDateFilter.ChiPhi.Date);
            }
            else
            {
                FilterChiPhi.FilterNgay = (p => true);
            }

            RefreshView();
        }

        #region Override base view method
        public override void CommitEdit()
        {
            dgNhapNguyenLieu.CommitEdit();
            base.CommitEdit();
        }

        public override void Save()
        {
            CommitEdit();
            try
            {
                if (FilterChiPhi == null)
                {
                    return;
                }

                var data = dgNhapNguyenLieu.DataContext as ObservableCollection<PhuDinhData.tNhapNguyenLieu>;
                PhuDinhData.Repository.tNhapNguyenLieuRepository.Save(_context, data.ToList(), _origData);
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
            if (FilterChiPhi.IsClearAllData == true)
            {
                dgNhapNguyenLieu.DataContext = null;
                return;
            }

            var index = dgNhapNguyenLieu.SelectedIndex;

            if (_tNhapNguyenLieus != null)
            {
                _tNhapNguyenLieus.CollectionChanged -= collection_CollectionChanged;
            }

            _context = ContextFactory.CreateContext();
            _origData = PhuDinhData.Repository.tNhapNguyenLieuRepository.GetData(_context, FilterChiPhi.FilterChiPhi);

            _tNhapNguyenLieus = new ObservableCollection<PhuDinhData.tNhapNguyenLieu>(_origData);
            _tNhapNguyenLieus.CollectionChanged += collection_CollectionChanged;

            UpdateNhanVienGiaoHangReferenceData();
            UpdateLoaiChiPhiReferenceData();

            dgNhapNguyenLieu.DataContext = _tNhapNguyenLieus;

            dgNhapNguyenLieu.SelectedIndex = index;
        }

        void collection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var tNhapNguyenLieu = e.NewItems[0] as PhuDinhData.tNhapNguyenLieu;
                tNhapNguyenLieu.Ngay = (DataGridColumnHeaderDateFilter.ChiPhi.IsUsed) ? DataGridColumnHeaderDateFilter.ChiPhi.Date : DateTime.Now;
                tNhapNguyenLieu.rNguyenLieuList = _rNguyenLieus;
                tNhapNguyenLieu.rNhaCungCapList = _rNhaCungCaps;

                if (rNguyenLieuDefault != null)
                {
                    tNhapNguyenLieu.MaNguyenLieu = rNguyenLieuDefault.Ma;
                }

                if (rNhaCungCapDefault != null)
                {
                    tNhapNguyenLieu.MaNhaCungCap = rNhaCungCapDefault.Ma;
                }
            }
        }

        #endregion

        private void dgNhapNguyenLieu_HeaderAddButtonClick(object sender, EventArgs e)
        {
            CommitEdit();

            var header = sender as DataGridColumnHeader;

            UserControl view = null;

            switch (header.Content.ToString())
            {
                case "Nhà Cung Cấp":
                    view = new rNhaCungCapView();
                    ChildWindowUtils.ShowChildWindow("Nhà Cung Cấp", view);

                    UpdateNhanVienGiaoHangReferenceData();
                    break;
                case "Nguyên Liệu":
                    view = new rNguyenLieuView();
                    ChildWindowUtils.ShowChildWindow("Nguyên Liệu", view);

                    UpdateLoaiChiPhiReferenceData();
                    break;
            }
        }

        private void UpdateLoaiChiPhiReferenceData()
        {
            _rNguyenLieus = PhuDinhData.Repository.rNguyenLieuRepository.GetData(_context, FilterNguyenLieu);

            if (_tNhapNguyenLieus == null)
            {
                return;
            }

            foreach (var tNhapNguyenLieu in _tNhapNguyenLieus)
            {
                tNhapNguyenLieu.rNguyenLieuList = _rNguyenLieus;
            }
        }

        private void UpdateNhanVienGiaoHangReferenceData()
        {
            _rNhaCungCaps = PhuDinhData.Repository.rNhaCungCapRepository.GetData(_context, FilterNhaCungCap);

            if (_tNhapNguyenLieus == null)
            {
                return;
            }

            foreach (var tNhapNguyenLieu in _tNhapNguyenLieus)
            {
                tNhapNguyenLieu.rNhaCungCapList = _rNhaCungCaps;
            }
        }
    }
}
