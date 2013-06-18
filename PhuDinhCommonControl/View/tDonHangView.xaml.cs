using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for tDonHangView.xaml
    /// </summary>
    public partial class tDonHangView : BaseView
    {
        public Expression<Func<PhuDinhData.tDonHang, bool>> FilterDonHang { get; set; }
        public Expression<Func<PhuDinhData.tChuyenHang, bool>> FilterChuyenHang { get; set; }
        public Expression<Func<PhuDinhData.rKhachHang, bool>> FilterKhachHang { get; set; }
        public Expression<Func<PhuDinhData.rChanh, bool>> FilterChanh { get; set; }

        private readonly PhuDinhData.PhuDinhEntities _context = new PhuDinhData.PhuDinhEntities();

        public tDonHangView()
        {
            InitializeComponent();

            FilterDonHang = (p => true);
            FilterChuyenHang = (p => true);
            FilterKhachHang = (p => true);
            FilterChanh = (p => true);
        }

        #region Override base view method
        public override void Save()
        {
            try
            {
                var data = this.dgDonHang.DataContext as ObservableCollection<PhuDinhData.tDonHang>;
                PhuDinhData.Repository.tDonHangRepository.Save(_context, data.ToList(), FilterDonHang);
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
            PhuDinhData.tDonHang.tChuyenHangs =
                PhuDinhData.Repository.tChuyenHangRepository.GetData(_context, FilterChuyenHang);
            PhuDinhData.tDonHang.rKhachHangs =
                PhuDinhData.Repository.rKhachHangRepository.GetData(_context, FilterKhachHang);
            PhuDinhData.tDonHang.rChanhs =
                PhuDinhData.Repository.rChanhRepository.GetData(_context, FilterChanh);

            var data = PhuDinhData.Repository.tDonHangRepository.GetData(_context, FilterDonHang);

            foreach (var tDonHang in data)
            {
            }

            var collection = new ObservableCollection<PhuDinhData.tDonHang>(data);
            collection.CollectionChanged += collection_CollectionChanged;
            this.dgDonHang.DataContext = collection;

            this.dgDonHang.UpdateLayout();
        }

        void collection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var chuyenHang = e.NewItems[0] as PhuDinhData.tDonHang;
                chuyenHang.Ngay = DateTime.Now;
            }
        }

        #endregion
    }
}
