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
    /// Interaction logic for tNhapMatHangView.xaml
    /// </summary>
    public partial class tNhapMatHangView : BaseView
    {
        public Filter_tNhapMatHang FilterChiPhi { get; set; }
        public Expression<Func<PhuDinhData.tMatHang, bool>> FilterMatHang { get; set; }
        public Expression<Func<PhuDinhData.rNhanVien, bool>> FilterNhanVien { get; set; }
        public PhuDinhData.tMatHang tMatHangDefault { get; set; }
        public PhuDinhData.rNhanVien rNhanVienDefault { get; set; }

        private ObservableCollection<PhuDinhData.tNhapMatHang> _tNhapMatHangs;
        private List<PhuDinhData.tMatHang> _tMatHangs;
        private List<PhuDinhData.rNhanVien> _rNhanViens;

        private PhuDinhData.PhuDinhEntities _context = ContextFactory.CreateContext();

        private bool _isUsedDateFilter = true;
        private DateTime _filterDate = DateTime.Now.Date;

        public tNhapMatHangView()
        {
            InitializeComponent();

            FilterChiPhi = new Filter_tNhapMatHang();
            FilterMatHang = (p => true);
            FilterNhanVien = (p => true);

            Loaded += tNhapMatHangView_Loaded;
            Unloaded += tNhapMatHangView_Unloaded;
        }

        void tNhapMatHangView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            DataGridColumnHeaderDateFilter.ChiPhi.Date = _filterDate;
            DataGridColumnHeaderDateFilter.ChiPhi.IsUsed = _isUsedDateFilter;
            DataGridColumnHeaderDateFilter.ChiPhi.PropertyChanged += ChiPhi_PropertyChanged;
            ChiPhi_PropertyChanged(null, null);
        }

        void tNhapMatHangView_Unloaded(object sender, System.Windows.RoutedEventArgs e)
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
            dgNhapMatHang.CommitEdit();
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

                var data = dgNhapMatHang.DataContext as ObservableCollection<PhuDinhData.tNhapMatHang>;
                PhuDinhData.Repository.tNhapMatHangRepository.Save(_context, data.ToList(), FilterChiPhi.FilterChiPhi);
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
                dgNhapMatHang.DataContext = null;
                return;
            }

            var index = dgNhapMatHang.SelectedIndex;

            if (_tNhapMatHangs != null)
            {
                _tNhapMatHangs.CollectionChanged -= collection_CollectionChanged;
            }

            _context = ContextFactory.CreateContext();
            var tNhapMatHangs = PhuDinhData.Repository.tNhapMatHangRepository.GetData(_context, FilterChiPhi.FilterChiPhi);

            _tNhapMatHangs = new ObservableCollection<PhuDinhData.tNhapMatHang>(tNhapMatHangs);
            _tNhapMatHangs.CollectionChanged += collection_CollectionChanged;

            UpdateNhanVienGiaoHangReferenceData();
            UpdateLoaiChiPhiReferenceData();

            dgNhapMatHang.DataContext = _tNhapMatHangs;

            dgNhapMatHang.SelectedIndex = index;
        }

        void collection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var tNhapMatHang = e.NewItems[0] as PhuDinhData.tNhapMatHang;
                tNhapMatHang.Ngay = (DataGridColumnHeaderDateFilter.ChiPhi.IsUsed) ? DataGridColumnHeaderDateFilter.ChiPhi.Date : DateTime.Now;
                tNhapMatHang.tMatHangList = _tMatHangs;
                tNhapMatHang.rNhanVienList = _rNhanViens;

                if (tMatHangDefault != null)
                {
                    tNhapMatHang.MaMatHang = tMatHangDefault.Ma;
                }

                if (rNhanVienDefault != null)
                {
                    tNhapMatHang.MaNhanVien = rNhanVienDefault.Ma;
                }
            }
        }

        #endregion

        private void dgNhapMatHang_HeaderAddButtonClick(object sender, EventArgs e)
        {
            CommitEdit();

            var header = sender as DataGridColumnHeader;

            BaseView view = null;

            switch (header.Content.ToString())
            {
                case "Nhân Viên":
                    view = new rNhanVienView();
                    view.RefreshView();
                    ChildWindowUtils.ShowChildWindow("Nhân Viên", view);

                    UpdateNhanVienGiaoHangReferenceData();
                    break;
                case "Mặt Hàng":
                    view = new tMatHangView();
                    view.RefreshView();
                    ChildWindowUtils.ShowChildWindow("Mặt Hàng", view);

                    UpdateLoaiChiPhiReferenceData();
                    break;
            }
        }

        private void UpdateLoaiChiPhiReferenceData()
        {
            _tMatHangs = PhuDinhData.Repository.tMatHangRepository.GetData(_context, FilterMatHang);

            if (_tNhapMatHangs == null)
            {
                return;
            }

            foreach (var tNhapMatHang in _tNhapMatHangs)
            {
                tNhapMatHang.tMatHangList = _tMatHangs;
            }
        }

        private void UpdateNhanVienGiaoHangReferenceData()
        {
            _rNhanViens = PhuDinhData.Repository.rNhanVienRepository.GetData(_context, FilterNhanVien);

            if (_tNhapMatHangs == null)
            {
                return;
            }

            foreach (var tNhapMatHang in _tNhapMatHangs)
            {
                tNhapMatHang.rNhanVienList = _rNhanViens;
            }
        }
    }
}
