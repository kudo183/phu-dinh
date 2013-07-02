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
    /// Interaction logic for tChiPhiNhanVienGiaoHangView.xaml
    /// </summary>
    public partial class tChiPhiNhanVienGiaoHangView : BaseView
    {
        public Expression<Func<PhuDinhData.tChiPhiNhanVienGiaoHang, bool>> FilterChiPhiNhanVienGiaoHang { get; set; }
        public Expression<Func<PhuDinhData.rLoaiChiPhi, bool>> FilterLoaiChiPhi { get; set; }
        public Expression<Func<PhuDinhData.rNhanVienGiaoHang, bool>> FilterNhanVienGiaoHang { get; set; }
        public PhuDinhData.rLoaiChiPhi rLoaiChiPhiDefault { get; set; }
        public PhuDinhData.rNhanVienGiaoHang rNhanVienGiaoHangDefault { get; set; }

        private List<PhuDinhData.tChiPhiNhanVienGiaoHang> _tChiPhiNhanVienGiaoHangs;
        private List<PhuDinhData.rLoaiChiPhi> _rLoaiChiPhis;
        private List<PhuDinhData.rNhanVienGiaoHang> _rNhanVienGiaoHangs;
        private PhuDinhData.PhuDinhEntities _context = new PhuDinhData.PhuDinhEntities();

        public tChiPhiNhanVienGiaoHangView()
        {
            InitializeComponent();

            FilterChiPhiNhanVienGiaoHang = (p => true);
            FilterLoaiChiPhi = (p => true);
            FilterNhanVienGiaoHang = (p => true);
        }

        #region Override base view method
        public override void Save()
        {
            try
            {
                if (FilterChiPhiNhanVienGiaoHang == null)
                {
                    return;
                }

                var data = dgChiPhiNhanVienGiaoHang.DataContext as ObservableCollection<PhuDinhData.tChiPhiNhanVienGiaoHang>;
                PhuDinhData.Repository.tChiPhiNhanVienGiaoHangRepository.Save(_context, data.ToList(), FilterChiPhiNhanVienGiaoHang);
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
            if (FilterChiPhiNhanVienGiaoHang == null)
            {
                dgChiPhiNhanVienGiaoHang.DataContext = null;
                return;
            }

            var index = dgChiPhiNhanVienGiaoHang.SelectedIndex;

            _context = new PhuDinhData.PhuDinhEntities();

            _tChiPhiNhanVienGiaoHangs = PhuDinhData.Repository.tChiPhiNhanVienGiaoHangRepository.GetData(_context, FilterChiPhiNhanVienGiaoHang);
            var collection = new ObservableCollection<PhuDinhData.tChiPhiNhanVienGiaoHang>(_tChiPhiNhanVienGiaoHangs);
            collection.CollectionChanged += collection_CollectionChanged;
            dgChiPhiNhanVienGiaoHang.DataContext = collection;

            UpdateNhanVienGiaoHangReferenceData();
            UpdateLoaiChiPhiReferenceData();

            dgChiPhiNhanVienGiaoHang.UpdateLayout();

            dgChiPhiNhanVienGiaoHang.SelectedIndex = index;
        }

        void collection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var tChiPhiNhanVienGiaoHang = e.NewItems[0] as PhuDinhData.tChiPhiNhanVienGiaoHang;
                tChiPhiNhanVienGiaoHang.Ngay = DateTime.Now;
                tChiPhiNhanVienGiaoHang.rLoaiChiPhiList = _rLoaiChiPhis;
                tChiPhiNhanVienGiaoHang.rNhanVienGiaoHangList = _rNhanVienGiaoHangs;

                if (rLoaiChiPhiDefault != null)
                {
                    tChiPhiNhanVienGiaoHang.MaLoaiChiPhi = rLoaiChiPhiDefault.Ma;
                }

                if (rNhanVienGiaoHangDefault != null)
                {
                    tChiPhiNhanVienGiaoHang.MaNhanVienGiaoHang = rNhanVienGiaoHangDefault.Ma;
                }
            }
        }

        #endregion

        private void dgChiPhiNhanVienGiaoHang_HeaderAddButtonClick(object sender, EventArgs e)
        {
            var header = sender as DataGridColumnHeader;
            
            BaseView view = null;

            switch (header.Content.ToString())
            {
                case "Nhân Viên":
                    view = new rNhanVienGiaoHangView();
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
            foreach (var tChiPhiNhanVienGiaoHang in _tChiPhiNhanVienGiaoHangs)
            {
                tChiPhiNhanVienGiaoHang.rLoaiChiPhiList = _rLoaiChiPhis;
            }
        }

        private void UpdateNhanVienGiaoHangReferenceData()
        {
            _rNhanVienGiaoHangs = PhuDinhData.Repository.rNhanVienGiaoHangRepository.GetData(_context, FilterNhanVienGiaoHang);
            foreach (var tChiPhiNhanVienGiaoHang in _tChiPhiNhanVienGiaoHangs)
            {
                tChiPhiNhanVienGiaoHang.rNhanVienGiaoHangList = _rNhanVienGiaoHangs;
            }
        }
    }
}
