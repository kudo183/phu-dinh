using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;

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

        public tDonHangView()
        {
            InitializeComponent();

            FilterDonHang = (p => true);
            FilterChuyenHang = (p => true);
            FilterKhachHang = (p => true);
            FilterChanh = (p => true);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Save();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Cancel();
        }

        #region Override base view method
        public override void Save()
        {
            try
            {
                var data = this.dgDonHang.DataContext as ObservableCollection<PhuDinhData.tDonHang>;
                PhuDinhData.Repository.tDonHangRepository.Save(data.ToList(), FilterDonHang);
                RefreshView();
            }
            catch (Exception ex)
            {
            }
        }

        public override void Cancel()
        {
            RefreshView();
        }

        public override void RefreshView()
        {
            var context = new PhuDinhData.PhuDinhEntities();
            PhuDinhData.tDonHang.tChuyenHangs =
                PhuDinhData.Repository.tChuyenHangRepository.GetData(context, FilterChuyenHang);
            PhuDinhData.tDonHang.rKhachHangs =
                PhuDinhData.Repository.rKhachHangRepository.GetData(context, FilterKhachHang);
            PhuDinhData.tDonHang.rChanhs =
                PhuDinhData.Repository.rChanhRepository.GetData(context, FilterChanh);

            var data = PhuDinhData.Repository.tDonHangRepository.GetData(context, FilterDonHang);

            foreach (var tDonHang in data)
            {
                tDonHang.KhachHang = PhuDinhData.tDonHang.rKhachHangs.FirstOrDefault(
                    p => p.Ma == tDonHang.MaKhachHang);
                tDonHang.Chanh = PhuDinhData.tDonHang.rChanhs.FirstOrDefault(
                    p => p.Ma == tDonHang.MaChanh);
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
