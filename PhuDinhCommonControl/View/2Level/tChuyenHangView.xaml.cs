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

        private List<PhuDinhData.rNhanVienGiaoHang> _rNhanVienGiaoHangs;
        private readonly PhuDinhData.PhuDinhEntities _context = new PhuDinhData.PhuDinhEntities();

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
                var data = this.dgChuyenHang.DataContext as ObservableCollection<PhuDinhData.tChuyenHang>;
                PhuDinhData.Repository.tChuyenHangRepository.Save(_context, data.ToList(), FilterChuyenHang);
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
            _rNhanVienGiaoHangs = PhuDinhData.Repository.rNhanVienGiaoHangRepository.GetData(_context, FilterNhanVienGiaoHang);

            var data = PhuDinhData.Repository.tChuyenHangRepository.GetData(_context, FilterChuyenHang);

            foreach (var tChuyenHang in data)
            {
                tChuyenHang.rNhanVienGiaoHangList = _rNhanVienGiaoHangs;
            }

            var collection = new ObservableCollection<PhuDinhData.tChuyenHang>(data);
            collection.CollectionChanged += collection_CollectionChanged;
            this.dgChuyenHang.DataContext = collection;

            this.dgChuyenHang.UpdateLayout();
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
    }
}
