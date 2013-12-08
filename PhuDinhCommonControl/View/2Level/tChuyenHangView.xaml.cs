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
    /// Interaction logic for tChuyenHangView.xaml
    /// </summary>
    public partial class tChuyenHangView : BaseView
    {
        public Expression<Func<PhuDinhData.tChuyenHang, bool>> FilterChuyenHang { get; set; }
        public Expression<Func<PhuDinhData.rNhanVien, bool>> FilterNhanVien { get; set; }

        private ObservableCollection<PhuDinhData.tChuyenHang> _tChuyenHangs;
        private List<PhuDinhData.rNhanVien> _rNhanViens;

        private PhuDinhData.PhuDinhEntities _context = ContextFactory.CreateContext();

        private List<PhuDinhData.tChuyenHang> _origData;

        private bool _isUsedDateFilter = true;
        private DateTime _filterDate = DateTime.Now.Date;

        public tChuyenHangView()
        {
            InitializeComponent();

            FilterNhanVien = (p => true);

            Loaded += tChuyenHangView_Loaded;
            Unloaded += tChuyenHangView_Unloaded;
        }

        void tChuyenHangView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            DataGridColumnHeaderDateFilter.ChuyenHang.Date = _filterDate;
            DataGridColumnHeaderDateFilter.ChuyenHang.IsUsed = _isUsedDateFilter;
            DataGridColumnHeaderDateFilter.ChuyenHang.PropertyChanged += ChuyenHang_PropertyChanged;
            ChuyenHang_PropertyChanged(null, null);
        }

        void tChuyenHangView_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            DataGridColumnHeaderDateFilter.ChuyenHang.PropertyChanged -= ChuyenHang_PropertyChanged;
            _filterDate = DataGridColumnHeaderDateFilter.ChuyenHang.Date;
            _isUsedDateFilter = DataGridColumnHeaderDateFilter.ChuyenHang.IsUsed;
        }

        void ChuyenHang_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (DataGridColumnHeaderDateFilter.ChuyenHang.IsUsed)
            {
                FilterChuyenHang = (p => p.Ngay == DataGridColumnHeaderDateFilter.ChuyenHang.Date);
            }
            else
            {
                FilterChuyenHang = (p => true);
            }

            RefreshView();
        }

        #region Override base view method
        public override void CommitEdit()
        {
            dgChuyenHang.CommitEdit();
            base.CommitEdit();
        }
        
        public override void Save()
        {
            CommitEdit();
            try
            {
                if (FilterChuyenHang == null)
                {
                    return;
                }

                var data = dgChuyenHang.DataContext as ObservableCollection<PhuDinhData.tChuyenHang>;
                PhuDinhData.Repository.tChuyenHangRepository.Save(_context, data.ToList(), _origData);
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
            if (FilterChuyenHang == null)
            {
                dgChuyenHang.DataContext = null;
                return;
            }

            var index = dgChuyenHang.SelectedIndex;

            if (_tChuyenHangs != null)
            {
                _tChuyenHangs.CollectionChanged -= collection_CollectionChanged;
            }

            _context = ContextFactory.CreateContext();
            _origData = PhuDinhData.Repository.tChuyenHangRepository.GetData(_context, FilterChuyenHang);

            _tChuyenHangs = new ObservableCollection<PhuDinhData.tChuyenHang>(_origData);
            _tChuyenHangs.CollectionChanged += collection_CollectionChanged;

            UpdateNhanVienGiaoHangReferenceData();

            dgChuyenHang.DataContext = _tChuyenHangs;

            dgChuyenHang.SelectedIndex = index;
        }

        void collection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var tChuyenHang = e.NewItems[0] as PhuDinhData.tChuyenHang;
                tChuyenHang.Ngay = (DataGridColumnHeaderDateFilter.ChuyenHang.IsUsed) ? DataGridColumnHeaderDateFilter.ChuyenHang.Date : DateTime.Now;
                tChuyenHang.Gio = DateTime.Now.TimeOfDay;
                tChuyenHang.rNhanVienList = _rNhanViens;
            }
        }

        #endregion

        private void dgChuyenHang_HeaderAddButtonClick(object sender, EventArgs e)
        {
            CommitEdit();

            var view = new rNhanVienView();
            view.RefreshView();
            ChildWindowUtils.ShowChildWindow("Nhân Viên", view);

            UpdateNhanVienGiaoHangReferenceData();
        }

        private void UpdateNhanVienGiaoHangReferenceData()
        {
            _rNhanViens = PhuDinhData.Repository.rNhanVienRepository.GetData(_context, FilterNhanVien);

            if (_tChuyenHangs == null)
            {
                return;
            }

            foreach (var tChuyenHang in _tChuyenHangs)
            {
                tChuyenHang.rNhanVienList = _rNhanViens;
            }
        }
    }
}
