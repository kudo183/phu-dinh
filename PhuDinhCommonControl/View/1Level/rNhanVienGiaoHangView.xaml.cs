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
    public partial class rNhanVienGiaoHangView : BaseView
    {
        public Expression<Func<PhuDinhData.rNhanVienGiaoHang, bool>> FilterNhanVienGiaoHang { get; set; }
        public Expression<Func<PhuDinhData.rPhuongTien, bool>> FilterPhuongTien { get; set; }
        public PhuDinhData.rPhuongTien rPhuongTienDefault { get; set; }

        private ObservableCollection<PhuDinhData.rNhanVienGiaoHang> _rNhanVienGiaoHangs;
        private List<PhuDinhData.rPhuongTien> _rPhuongTiens;
        private PhuDinhData.PhuDinhEntities _context = new PhuDinhData.PhuDinhEntities();

        public rNhanVienGiaoHangView()
        {
            InitializeComponent();

            FilterPhuongTien = (p => true);
            FilterNhanVienGiaoHang = (p => true);
        }

        #region Override base view method
        public override void Save()
        {
            try
            {
                if (FilterNhanVienGiaoHang == null)
                {
                    return;
                }

                var data = dgNhanVienGiaoHang.DataContext as ObservableCollection<PhuDinhData.rNhanVienGiaoHang>;
                PhuDinhData.Repository.rNhanVienGiaoHangRepository.Save(_context, data.ToList(), FilterNhanVienGiaoHang);
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
            if (FilterNhanVienGiaoHang == null)
            {
                dgNhanVienGiaoHang.DataContext = null;
                return;
            }

            var index = dgNhanVienGiaoHang.SelectedIndex;

            if (_rNhanVienGiaoHangs != null)
            {
                _rNhanVienGiaoHangs.CollectionChanged -= collection_CollectionChanged;
            }

            _context = new PhuDinhData.PhuDinhEntities();
            var rNhanVienGiaoHangs = PhuDinhData.Repository.rNhanVienGiaoHangRepository.GetData(_context, FilterNhanVienGiaoHang);

            _rNhanVienGiaoHangs = new ObservableCollection<PhuDinhData.rNhanVienGiaoHang>(rNhanVienGiaoHangs);
            _rNhanVienGiaoHangs.CollectionChanged += collection_CollectionChanged;
            dgNhanVienGiaoHang.DataContext = _rNhanVienGiaoHangs;

            UpdatePhuongTienReferenceData();

            dgNhanVienGiaoHang.UpdateLayout();

            dgNhanVienGiaoHang.SelectedIndex = index;
        }

        void collection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var nhanVienGiaoHang = e.NewItems[0] as PhuDinhData.rNhanVienGiaoHang;
                nhanVienGiaoHang.rPhuongTienList = _rPhuongTiens;

                if (rPhuongTienDefault != null)
                {
                    nhanVienGiaoHang.MaPhuongTien = rPhuongTienDefault.Ma;
                }
            }
        }
        #endregion

        private void dgNhanVienGiaoHang_HeaderAddButtonClick(object sender, EventArgs e)
        {
            dgNhanVienGiaoHang.CommitEdit();

            var view = new rPhuongTienView();
            view.RefreshView();
            ChildWindowUtils.ShowChildWindow("Phương Tiện", view);

            UpdatePhuongTienReferenceData();
        }

        private void UpdatePhuongTienReferenceData()
        {
            _rPhuongTiens = PhuDinhData.Repository.rPhuongTienRepository.GetData(_context, FilterPhuongTien);

            if (_rNhanVienGiaoHangs == null)
            {
                return;
            }

            foreach (var rNhanVienGiaoHang in _rNhanVienGiaoHangs)
            {
                rNhanVienGiaoHang.rPhuongTienList = _rPhuongTiens;
            }
        }
    }
}
