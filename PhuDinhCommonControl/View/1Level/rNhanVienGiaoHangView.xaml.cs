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

        private List<PhuDinhData.rNhanVienGiaoHang> _rNhanVienGiaoHangs;
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

            _context = new PhuDinhData.PhuDinhEntities();

            _rNhanVienGiaoHangs = PhuDinhData.Repository.rNhanVienGiaoHangRepository.GetData(_context, FilterNhanVienGiaoHang);
            var collection = new ObservableCollection<PhuDinhData.rNhanVienGiaoHang>(_rNhanVienGiaoHangs);
            collection.CollectionChanged += collection_CollectionChanged;
            dgNhanVienGiaoHang.DataContext = collection;

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
            var view = new rPhuongTienView();
            view.RefreshView();
            ChildWindowUtils.ShowChildWindow("Phương Tiện", view);

            UpdatePhuongTienReferenceData();
        }

        private void UpdatePhuongTienReferenceData()
        {
            _rPhuongTiens = PhuDinhData.Repository.rPhuongTienRepository.GetData(_context, FilterPhuongTien);
            foreach (var rNhanVienGiaoHang in _rNhanVienGiaoHangs)
            {
                rNhanVienGiaoHang.rPhuongTienList = _rPhuongTiens;
            }
        }
    }
}
