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
    public partial class rNhanVienView : BaseView
    {
        public Expression<Func<PhuDinhData.rNhanVien, bool>> FilterNhanVien { get; set; }
        public Expression<Func<PhuDinhData.rPhuongTien, bool>> FilterPhuongTien { get; set; }
        public PhuDinhData.rPhuongTien rPhuongTienDefault { get; set; }

        private ObservableCollection<PhuDinhData.rNhanVien> _rNhanViens;
        private List<PhuDinhData.rPhuongTien> _rPhuongTiens;
        private PhuDinhData.PhuDinhEntities _context = new PhuDinhData.PhuDinhEntities();

        public rNhanVienView()
        {
            InitializeComponent();

            FilterPhuongTien = (p => true);
            FilterNhanVien = (p => true);
        }

        #region Override base view method
        public override void Save()
        {
            try
            {
                if (FilterNhanVien == null)
                {
                    return;
                }

                var data = dgNhanVien.DataContext as ObservableCollection<PhuDinhData.rNhanVien>;
                PhuDinhData.Repository.rNhanVienRepository.Save(_context, data.ToList(), FilterNhanVien);
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

            _context = new PhuDinhData.PhuDinhEntities();
            var rNhanViens = PhuDinhData.Repository.rNhanVienRepository.GetData(_context, FilterNhanVien);

            _rNhanViens = new ObservableCollection<PhuDinhData.rNhanVien>(rNhanViens);
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
            dgNhanVien.CommitEdit();

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
