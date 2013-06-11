using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for rKhachHangView.xaml
    /// </summary>
    public partial class rKhachHangView : BaseView
    {
        public Expression<Func<PhuDinhData.rKhachHang, bool>> FilterKhachHang { get; set; }
        public Expression<Func<PhuDinhData.rDiaDiem, bool>> FilterDiaDiem { get; set; }

        public rKhachHangView()
        {
            InitializeComponent();

            FilterKhachHang = (p => true);
            FilterDiaDiem = (p => true);
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
                var data = this.dgKhachHang.DataContext as List<PhuDinhData.rKhachHang>;
                PhuDinhData.Repository.rKhachHangRepository.Save(data, FilterKhachHang);
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
            PhuDinhData.rKhachHang.rDiaDiems = PhuDinhData.Repository.rDiaDiemRepository.GetData(context, FilterDiaDiem);

            var data = PhuDinhData.Repository.rKhachHangRepository.GetData(context, FilterKhachHang);

            foreach (var rKhachHang in data)
            {
                rKhachHang.DiaDiem = PhuDinhData.rKhachHang.rDiaDiems.FirstOrDefault(
                    p => p.Ma == rKhachHang.MaDiaDiem);
            }

            this.dgKhachHang.DataContext = data;

            this.dgKhachHang.UpdateLayout();
        }
        #endregion
    }
}
