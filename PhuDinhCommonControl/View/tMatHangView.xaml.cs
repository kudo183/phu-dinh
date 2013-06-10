using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for View.xaml
    /// </summary>
    public partial class tMatHangView : BaseView
    {
        public Expression<Func<PhuDinhData.tMatHang, bool>> FilterMatHang { get; set; }
        public Expression<Func<PhuDinhData.rLoaiHang, bool>> FilterLoaiHang { get; set; }

        public tMatHangView()
        {
            InitializeComponent();

            FilterMatHang = (p => true);
            FilterLoaiHang = (p => true);
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
                var data = this.dgMatHang.DataContext as List<PhuDinhData.tMatHang>;
                PhuDinhData.Repository.tMatHangRepository.Save(data, FilterMatHang);
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
            PhuDinhData.tMatHang.rLoaiHangs = PhuDinhData.Repository.rLoaiHangRepository.GetData(context, FilterLoaiHang);
            var data = PhuDinhData.Repository.tMatHangRepository.GetData(context, FilterMatHang);

            foreach (var tMatHang in data)
            {
                tMatHang.LoaiHang = PhuDinhData.tMatHang.rLoaiHangs.FirstOrDefault(
                    p => p.Ma == tMatHang.MaLoai);
            }
            this.dgMatHang.DataContext = data;
        }
        #endregion
    }
}
