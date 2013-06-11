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
    public partial class rNhanVienGiaoHangView : BaseView
    {
        public Expression<Func<PhuDinhData.rPhuongTien, bool>> FilterPhuongTien { get; set; }
        public Expression<Func<PhuDinhData.rNhanVienGiaoHang, bool>> FilterNhanVienGiaoHang { get; set; }

        public rNhanVienGiaoHangView()
        {
            InitializeComponent();

            FilterPhuongTien = (p => true);
            FilterNhanVienGiaoHang = (p => true);
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
                var data = this.dgNhanVienGiaoHang.DataContext as List<PhuDinhData.rNhanVienGiaoHang>;
                PhuDinhData.Repository.rNhanVienGiaoHangRepository.Save(data, FilterNhanVienGiaoHang);
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
            PhuDinhData.rNhanVienGiaoHang.rPhuongTiens =
                PhuDinhData.Repository.rPhuongTienRepository.GetData(context, FilterPhuongTien);

            var data = PhuDinhData.Repository.rNhanVienGiaoHangRepository.GetData(context, FilterNhanVienGiaoHang);

            foreach (var rNhanVienGiaoHang in data)
            {
                rNhanVienGiaoHang.PhuongTien = PhuDinhData.rNhanVienGiaoHang.rPhuongTiens.FirstOrDefault(
                    p => p.Ma == rNhanVienGiaoHang.MaPhuongTien);
            }

            this.dgNhanVienGiaoHang.DataContext = data;
        }
        #endregion
    }
}
