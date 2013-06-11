using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;

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

        public tChuyenHangDonHangView()
        {
            InitializeComponent();

            FilterChuyenHangDonHang = (p => true);
            FilterChuyenHang = (p => true);
            FilterDonHang = (p => true);
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
                var data = this.dgChuyenHangDonHang.DataContext as List<PhuDinhData.tChuyenHangDonHang>;
                PhuDinhData.Repository.tChuyenHangDonHangRepository.Save(data, FilterChuyenHangDonHang);
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
            PhuDinhData.tChuyenHangDonHang.tChuyenHangs = PhuDinhData.Repository.tChuyenHangRepository.GetData(context, FilterChuyenHang);
            PhuDinhData.tChuyenHangDonHang.tDonHangs = PhuDinhData.Repository.tDonHangRepository.GetData(context, FilterDonHang);

            var data = PhuDinhData.Repository.tChuyenHangDonHangRepository.GetData(context, FilterChuyenHangDonHang);

            foreach (var tChuyenHangDonHang in data)
            {
                tChuyenHangDonHang.ChuyenHang = PhuDinhData.tChuyenHangDonHang.tChuyenHangs.FirstOrDefault(
                    p => p.Ma == tChuyenHangDonHang.MaChuyenHang);
                tChuyenHangDonHang.DonHang = PhuDinhData.tChuyenHangDonHang.tDonHangs.FirstOrDefault(
                        p => p.Ma == tChuyenHangDonHang.MaDonHang);
            }

            this.dgChuyenHangDonHang.DataContext = data;

            this.dgChuyenHangDonHang.UpdateLayout();
        }
        #endregion
    }
}
