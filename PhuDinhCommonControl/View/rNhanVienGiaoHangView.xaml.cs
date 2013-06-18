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
                var data = this.dgNhanVienGiaoHang.DataContext as ObservableCollection<PhuDinhData.rNhanVienGiaoHang>;
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
            _rPhuongTiens = PhuDinhData.Repository.rPhuongTienRepository.GetData(_context, FilterPhuongTien);

            var data = PhuDinhData.Repository.rNhanVienGiaoHangRepository.GetData(_context, FilterNhanVienGiaoHang);

            foreach (var rNhanVienGiaoHang in data)
            {
                rNhanVienGiaoHang.rPhuongTienList = _rPhuongTiens;
            }

            var collection = new ObservableCollection<PhuDinhData.rNhanVienGiaoHang>(data);
            collection.CollectionChanged += collection_CollectionChanged;
            this.dgNhanVienGiaoHang.DataContext = collection;

            this.dgNhanVienGiaoHang.UpdateLayout();
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
    }
}
