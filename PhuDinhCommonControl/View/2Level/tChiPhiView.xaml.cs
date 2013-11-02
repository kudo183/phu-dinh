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
    /// Interaction logic for tChiPhiView.xaml
    /// </summary>
    public partial class tChiPhiView : BaseView
    {
        public Filter_tChiPhi FilterChiPhi { get; set; }
        public Expression<Func<PhuDinhData.rLoaiChiPhi, bool>> FilterLoaiChiPhi { get; set; }
        public Expression<Func<PhuDinhData.rNhanVien, bool>> FilterNhanVien { get; set; }
        public PhuDinhData.rLoaiChiPhi rLoaiChiPhiDefault { get; set; }
        public PhuDinhData.rNhanVien rNhanVienDefault { get; set; }

        private ObservableCollection<PhuDinhData.tChiPhi> _tChiPhis;
        private List<PhuDinhData.rLoaiChiPhi> _rLoaiChiPhis;
        private List<PhuDinhData.rNhanVien> _rNhanViens;

        private PhuDinhData.PhuDinhEntities _context = ContextFactory.CreateContext();

        private bool _isUsedDateFilter = true;
        private DateTime _filterDate = DateTime.Now.Date;

        public tChiPhiView()
        {
            InitializeComponent();

            FilterChiPhi = new Filter_tChiPhi();
            FilterLoaiChiPhi = (p => true);
            FilterNhanVien = (p => true);

            Loaded += tChiPhiView_Loaded;
            Unloaded += tChiPhiView_Unloaded;
        }

        void tChiPhiView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            DataGridColumnHeaderDateFilter.ChiPhi.Date = _filterDate;
            DataGridColumnHeaderDateFilter.ChiPhi.IsUsed = _isUsedDateFilter;
            DataGridColumnHeaderDateFilter.ChiPhi.PropertyChanged += ChiPhi_PropertyChanged;
            ChiPhi_PropertyChanged(null, null);
        }

        void tChiPhiView_Unloaded(object sender, System.Windows.RoutedEventArgs e)
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
        public override void Save()
        {
            try
            {
                if (FilterChiPhi == null)
                {
                    return;
                }

                var data = dgChiPhi.DataContext as ObservableCollection<PhuDinhData.tChiPhi>;
                PhuDinhData.Repository.tChiPhiRepository.Save(_context, data.ToList(), FilterChiPhi.FilterChiPhi);
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
                dgChiPhi.DataContext = null;
                return;
            }

            var index = dgChiPhi.SelectedIndex;

            if (_tChiPhis != null)
            {
                _tChiPhis.CollectionChanged -= collection_CollectionChanged;
            }

            _context = ContextFactory.CreateContext();
            var tChiPhis = PhuDinhData.Repository.tChiPhiRepository.GetData(_context, FilterChiPhi.FilterChiPhi);

            _tChiPhis = new ObservableCollection<PhuDinhData.tChiPhi>(tChiPhis);
            _tChiPhis.CollectionChanged += collection_CollectionChanged;

            UpdateNhanVienGiaoHangReferenceData();
            UpdateLoaiChiPhiReferenceData();

            dgChiPhi.DataContext = _tChiPhis;

            dgChiPhi.SelectedIndex = index;
        }

        void collection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var tChiPhi = e.NewItems[0] as PhuDinhData.tChiPhi;
                tChiPhi.Ngay = (DataGridColumnHeaderDateFilter.ChiPhi.IsUsed) ? DataGridColumnHeaderDateFilter.ChiPhi.Date : DateTime.Now;
                tChiPhi.rLoaiChiPhiList = _rLoaiChiPhis;
                tChiPhi.rNhanVienList = _rNhanViens;

                if (rLoaiChiPhiDefault != null)
                {
                    tChiPhi.MaLoaiChiPhi = rLoaiChiPhiDefault.Ma;
                }

                if (rNhanVienDefault != null)
                {
                    tChiPhi.MaNhanVienGiaoHang = rNhanVienDefault.Ma;
                }
            }
        }

        #endregion

        private void dgChiPhi_HeaderAddButtonClick(object sender, EventArgs e)
        {
            dgChiPhi.CommitEdit();

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
                case "Loại Chi Phí":
                    view = new rLoaiChiPhiView();
                    view.RefreshView();
                    ChildWindowUtils.ShowChildWindow("Loại Chi Phí", view);

                    UpdateLoaiChiPhiReferenceData();
                    break;
            }
        }

        private void UpdateLoaiChiPhiReferenceData()
        {
            _rLoaiChiPhis = PhuDinhData.Repository.rLoaiChiPhiRepository.GetData(_context, FilterLoaiChiPhi);

            if (_tChiPhis == null)
            {
                return;
            }

            foreach (var tChiPhi in _tChiPhis)
            {
                tChiPhi.rLoaiChiPhiList = _rLoaiChiPhis;
            }
        }

        private void UpdateNhanVienGiaoHangReferenceData()
        {
            _rNhanViens = PhuDinhData.Repository.rNhanVienRepository.GetData(_context, FilterNhanVien);

            if (_tChiPhis == null)
            {
                return;
            }

            foreach (var tChiPhi in _tChiPhis)
            {
                tChiPhi.rNhanVienList = _rNhanViens;
            }
        }
    }
}
