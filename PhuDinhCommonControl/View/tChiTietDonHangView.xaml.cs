using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for tChiTietDonHangView.xaml
    /// </summary>
    public partial class tChiTietDonHangView : BaseView
    {
        public Expression<Func<PhuDinhData.tChiTietDonHang, bool>> FilterChiTietDonHang { get; set; }
        public Expression<Func<PhuDinhData.tDonHang, bool>> FilterDonHang { get; set; }
        public Expression<Func<PhuDinhData.tMatHang, bool>> FilterMatHang { get; set; }

        public tChiTietDonHangView()
        {
            InitializeComponent();

            FilterChiTietDonHang = (p => true);
            FilterDonHang = (p => true);
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
                var data = this.dgChiTietDonHang.DataContext as List<PhuDinhData.tChiTietDonHang>;
                PhuDinhData.Repository.tChiTietDonHangRepository.Save(data, FilterChiTietDonHang);
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
            PhuDinhData.tChiTietDonHang.tDonHangs = PhuDinhData.Repository.tDonHangRepository.GetData(context, FilterDonHang);
            PhuDinhData.tChiTietDonHang.tMatHangs = PhuDinhData.Repository.tMatHangRepository.GetData(context, FilterMatHang);

            var data = PhuDinhData.Repository.tChiTietDonHangRepository.GetData(context, FilterChiTietDonHang);

            foreach (var tChiTietDonHang in data)
            {
            }

            this.dgChiTietDonHang.DataContext = data;

            this.dgChiTietDonHang.UpdateLayout();
        }
        #endregion
    }
}
