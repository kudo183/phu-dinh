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
    /// Interaction logic for tChuyenHangView.xaml
    /// </summary>
    public partial class tChuyenHangView : BaseView
    {
        public Expression<Func<PhuDinhData.tChuyenHang, bool>> FilterChuyenHang { get; set; }
        public Expression<Func<PhuDinhData.rNhanVienGiaoHang, bool>> FilterNhanVienGiaoHang { get; set; }

        private ObservableCollection<PhuDinhData.tChuyenHang> _tChuyenHangs;
        private List<PhuDinhData.rNhanVienGiaoHang> _rNhanVienGiaoHangs;
        private PhuDinhData.PhuDinhEntities _context = new PhuDinhData.PhuDinhEntities();

        public tChuyenHangView()
        {
            InitializeComponent();

            FilterChuyenHang = (p => true);
            FilterNhanVienGiaoHang = (p => true);

            Loaded += tChuyenHangView_Loaded;
            Unloaded += tChuyenHangView_Unloaded;
        }

        void tChuyenHangView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            DataGridColumnHeaderDateFilter.ChuyenHang.PropertyChanged += ChuyenHang_PropertyChanged;
        }

        void tChuyenHangView_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            DataGridColumnHeaderDateFilter.ChuyenHang.PropertyChanged -= ChuyenHang_PropertyChanged;
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
        public override void Save()
        {
            try
            {
                if (FilterChuyenHang == null)
                {
                    return;
                }

                var data = dgChuyenHang.DataContext as ObservableCollection<PhuDinhData.tChuyenHang>;
                PhuDinhData.Repository.tChuyenHangRepository.Save(_context, data.ToList(), FilterChuyenHang);
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

            _context = new PhuDinhData.PhuDinhEntities();
            var tChuyenHangs = PhuDinhData.Repository.tChuyenHangRepository.GetData(_context, FilterChuyenHang);

            _tChuyenHangs = new ObservableCollection<PhuDinhData.tChuyenHang>(tChuyenHangs);
            _tChuyenHangs.CollectionChanged += collection_CollectionChanged;
            dgChuyenHang.DataContext = _tChuyenHangs;

            UpdateNhanVienGiaoHangReferenceData();

            dgChuyenHang.UpdateLayout();

            dgChuyenHang.SelectedIndex = index;
        }

        void collection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var tChuyenHang = e.NewItems[0] as PhuDinhData.tChuyenHang;
                tChuyenHang.Ngay = (DataGridColumnHeaderDateFilter.ChuyenHang.IsUsed) ? DataGridColumnHeaderDateFilter.ChuyenHang.Date : DateTime.Now;
                tChuyenHang.Gio = DateTime.Now.TimeOfDay;
                tChuyenHang.rNhanVienGiaoHangList = _rNhanVienGiaoHangs;
            }
        }

        #endregion

        private void dgChuyenHang_HeaderAddButtonClick(object sender, EventArgs e)
        {
            dgChuyenHang.CommitEdit();

            var view = new rNhanVienGiaoHangView();
            view.RefreshView();
            ChildWindowUtils.ShowChildWindow("Nhân Viên", view);

            UpdateNhanVienGiaoHangReferenceData();
        }

        private void UpdateNhanVienGiaoHangReferenceData()
        {
            _rNhanVienGiaoHangs = PhuDinhData.Repository.rNhanVienGiaoHangRepository.GetData(_context, FilterNhanVienGiaoHang);
            foreach (var tChuyenHang in _tChuyenHangs)
            {
                tChuyenHang.rNhanVienGiaoHangList = _rNhanVienGiaoHangs;
            }
        }
    }
}
