using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for View.xaml
    /// </summary>
    public partial class rNhanVienGiaoHangView : BaseView
    {
        public Expression<Func<PhuDinhData.rPhuongTien, bool>> FilterPhuongTien { get; set; }
        public Expression<Func<PhuDinhData.rNhanVienGiaoHang, bool>> FilterNhanVienGiaoHang { get; set; }

        private List<PhuDinhData.rNhanVienGiaoHang> _rNhanVienGiaoHangs;
        private List<PhuDinhData.rPhuongTien> _rPhuongTiens;
        private readonly PhuDinhData.PhuDinhEntities _context = new PhuDinhData.PhuDinhEntities();

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
                RefreshView();
            }
            catch (Exception ex)
            {
                PhuDinhCommon.EntityFrameworkUtils.UndoContextChange(_context, EntityState.Modified);
            }
        }

        public override void Cancel()
        {
            RefreshView();
        }

        public override void RefreshView()
        {
            if (FilterNhanVienGiaoHang == null)
            {
                dgNhanVienGiaoHang.DataContext = null;
                return;
            }

            _rNhanVienGiaoHangs = PhuDinhData.Repository.rNhanVienGiaoHangRepository.GetData(_context, FilterNhanVienGiaoHang);
            var collection = new ObservableCollection<PhuDinhData.rNhanVienGiaoHang>(_rNhanVienGiaoHangs);
            collection.CollectionChanged += collection_CollectionChanged;
            dgNhanVienGiaoHang.DataContext = collection;

            UpdatePhuongTienReferenceData();
            
            dgNhanVienGiaoHang.UpdateLayout();
        }

        void collection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var nhanVienGiaoHang = e.NewItems[0] as PhuDinhData.rNhanVienGiaoHang;
                nhanVienGiaoHang.rPhuongTienList = _rPhuongTiens;
            }
        }
        #endregion

        private void dgNhanVienGiaoHang_HeaderAddButtonClick(object sender, EventArgs e)
        {
            var view = new rPhuongTienView();
            view.RefreshView();
            var w = new Window { Title = "Phương Tiện", Content = view };
            w.ShowDialog();

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
