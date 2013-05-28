using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for tChiTietDonHangView.xaml
    /// </summary>
    public partial class tChiTietDonHangView : BaseView
    {
        public tChiTietDonHangView()
        {
            InitializeComponent();
        }
        private static void RemoveOrUpdateItem(PhuDinhData.PhuDinhEntities context, IEnumerable<PhuDinhData.tChiTietDonHang> gridDataSource)
        {
            foreach (var item in context.tChiTietDonHangs.ToList())
            {
                var entity = gridDataSource.FirstOrDefault(p => p.Ma == item.Ma);
                //remove deleted item
                if (entity == null)
                {
                    context.tChiTietDonHangs.Remove(item);
                }
                //update exist item
                else
                {
                    item.MaDonHang = entity.MaDonHang;
                    item.MaMatHang = entity.MaMatHang;
                    item.SoLuong = entity.SoLuong;
                }
            }
        }

        private static void AddNewItem(PhuDinhData.PhuDinhEntities context, IEnumerable<PhuDinhData.tChiTietDonHang> gridDataSource)
        {
            foreach (var item in gridDataSource)
            {
                if (item.Ma == 0)
                {
                    context.tChiTietDonHangs.Add(item);
                }
            }
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
                var context = new PhuDinhData.PhuDinhEntities();
                var gridDataSource = this.tChiTietDonHangDataGrid.DataContext as IEnumerable<PhuDinhData.tChiTietDonHang>;

                RemoveOrUpdateItem(context, gridDataSource);

                AddNewItem(context, gridDataSource);

                context.SaveChanges();
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
            PhuDinhData.tChiTietDonHang.tDonHangs = context.tDonHangs.ToList();
            PhuDinhData.tChiTietDonHang.tMatHangs = context.tMatHangs.ToList();

            var data = context.tChiTietDonHangs.ToList();

            foreach (var tChiTietDonHang in data)
            {
                tChiTietDonHang.DonHang = PhuDinhData.tChiTietDonHang.tDonHangs.FirstOrDefault(
                    p => p.Ma == tChiTietDonHang.MaDonHang);
                tChiTietDonHang.MatHang = PhuDinhData.tChiTietDonHang.tMatHangs.FirstOrDefault(
                        p => p.Ma == tChiTietDonHang.MaMatHang);
            }

            this.tChiTietDonHangDataGrid.DataContext = data;

            this.tChiTietDonHangDataGrid.UpdateLayout();
        }
        #endregion
    }
}
