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
    /// Interaction logic for tChuyenHangDonHangView.xaml
    /// </summary>
    public partial class tChuyenHangDonHangView : BaseView
    {
        public Expression<Func<PhuDinhData.tChuyenHangDonHang, bool>> FilterChuyenHangDonHang { get; set; }
        public Expression<Func<PhuDinhData.tChuyenHang, bool>> FilterChuyenHang { get; set; }
        public Expression<Func<PhuDinhData.tDonHang, bool>> FilterDonHang { get; set; }

        private List<PhuDinhData.tChuyenHang> _tChuyenHangs;
        private List<PhuDinhData.tDonHang> _tDonHangs;
        private readonly PhuDinhData.PhuDinhEntities _context = new PhuDinhData.PhuDinhEntities();

        public tChuyenHangDonHangView()
        {
            InitializeComponent();

            FilterChuyenHangDonHang = (p => true);
            FilterChuyenHang = (p => true);
            FilterDonHang = (p => true);
        }

        #region Override base view method
        public override void Save()
        {
            try
            {
                var data = this.dgChuyenHangDonHang.DataContext as ObservableCollection<PhuDinhData.tChuyenHangDonHang>;
                PhuDinhData.Repository.tChuyenHangDonHangRepository.Save(_context, data.ToList(), FilterChuyenHangDonHang);
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
            _tChuyenHangs = PhuDinhData.Repository.tChuyenHangRepository.GetData(_context, FilterChuyenHang);
            _tDonHangs = PhuDinhData.Repository.tDonHangRepository.GetData(_context, FilterDonHang);

            var data = PhuDinhData.Repository.tChuyenHangDonHangRepository.GetData(_context, FilterChuyenHangDonHang);

            foreach (var tChuyenHangDonHang in data)
            {
                tChuyenHangDonHang.tChuyenHangList = _tChuyenHangs;
                tChuyenHangDonHang.tDonHangList = _tDonHangs;
            }

            var collection = new ObservableCollection<PhuDinhData.tChuyenHangDonHang>(data);
            collection.CollectionChanged += collection_CollectionChanged;
            this.dgChuyenHangDonHang.DataContext = collection;

            this.dgChuyenHangDonHang.UpdateLayout();
        }

        void collection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var tChuyenHangDonHang = e.NewItems[0] as PhuDinhData.tChuyenHangDonHang;
                tChuyenHangDonHang.tChuyenHangList = _tChuyenHangs;
                tChuyenHangDonHang.tDonHangList = _tDonHangs;
            }
        }
        #endregion
    }
}
