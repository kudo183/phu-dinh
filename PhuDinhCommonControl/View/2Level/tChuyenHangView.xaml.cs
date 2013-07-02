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

        private List<PhuDinhData.tChuyenHang> _tChuyenHangs;
        private List<PhuDinhData.rNhanVienGiaoHang> _rNhanVienGiaoHangs;
        private PhuDinhData.PhuDinhEntities _context = new PhuDinhData.PhuDinhEntities();

        public tChuyenHangView()
        {
            InitializeComponent();

            FilterChuyenHang = (p => true);
            FilterNhanVienGiaoHang = (p => true);
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

            _context = new PhuDinhData.PhuDinhEntities();

            _tChuyenHangs = PhuDinhData.Repository.tChuyenHangRepository.GetData(_context, FilterChuyenHang);
            var collection = new ObservableCollection<PhuDinhData.tChuyenHang>(_tChuyenHangs);
            collection.CollectionChanged += collection_CollectionChanged;
            dgChuyenHang.DataContext = collection;

            UpdateNhanVienGiaoHangReferenceData();

            dgChuyenHang.UpdateLayout();

            dgChuyenHang.SelectedIndex = index;
        }

        void collection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var tChuyenHang = e.NewItems[0] as PhuDinhData.tChuyenHang;
                tChuyenHang.Ngay = DateTime.Now;
                tChuyenHang.Gio = DateTime.Now.TimeOfDay;
                tChuyenHang.rNhanVienGiaoHangList = _rNhanVienGiaoHangs;
            }
        }

        #endregion

        private void dgChuyenHang_HeaderAddButtonClick(object sender, EventArgs e)
        {
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
