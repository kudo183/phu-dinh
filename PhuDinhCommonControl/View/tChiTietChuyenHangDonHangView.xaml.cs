using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for tChiTietChuyenHangDonHangView.xaml
    /// </summary>
    public partial class tChiTietChuyenHangDonHangView : BaseView
    {
        public Expression<Func<PhuDinhData.tChiTietChuyenHangDonHang, bool>> FilterChiTietChuyenHangDonHang { get; set; }
        public Expression<Func<PhuDinhData.tChuyenHangDonHang, bool>> FilterChuyenHangDonHang { get; set; }
        public Expression<Func<PhuDinhData.tMatHang, bool>> FilterMatHang { get; set; }

        public tChiTietChuyenHangDonHangView()
        {
            InitializeComponent();
            FilterChiTietChuyenHangDonHang = (p => true);
            FilterChuyenHangDonHang = (p => true);
            FilterMatHang = (p => true);
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
                var data = this.dgChiTietChuyenHangDonHang.DataContext as List<PhuDinhData.tChiTietChuyenHangDonHang>;
                PhuDinhData.Repository.tChiTietChuyenHangDonHangRepository.Save(data, FilterChiTietChuyenHangDonHang);
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
            PhuDinhData.tChiTietChuyenHangDonHang.tChuyenHangDonHangs =
                PhuDinhData.Repository.tChuyenHangDonHangRepository.GetData(context, FilterChuyenHangDonHang);
            PhuDinhData.tChiTietChuyenHangDonHang.tMatHangs =
                PhuDinhData.Repository.tMatHangRepository.GetData(context, FilterMatHang);

            var data = PhuDinhData.Repository.tChiTietChuyenHangDonHangRepository.GetData(context, FilterChiTietChuyenHangDonHang);

            foreach (var tChiTietChuyenHangDonHang in data)
            {
            }

            this.dgChiTietChuyenHangDonHang.DataContext = data;

            this.dgChiTietChuyenHangDonHang.UpdateLayout();
        }
        #endregion
    }
}
