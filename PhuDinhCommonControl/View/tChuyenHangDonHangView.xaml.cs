using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for tChuyenHangDonHangView.xaml
    /// </summary>
    public partial class tChuyenHangDonHangView : BaseView
    {
        public tChuyenHangDonHangView()
        {
            InitializeComponent();
        }
        private static void RemoveOrUpdateItem(PhuDinhData.PhuDinhEntities context, IEnumerable<PhuDinhData.tChuyenHangDonHang> gridDataSource)
        {
            foreach (var item in context.tChuyenHangDonHangs.ToList())
            {
                var entity = gridDataSource.FirstOrDefault(p => p.Ma == item.Ma);
                //remove deleted item
                if (entity == null)
                {
                    context.tChuyenHangDonHangs.Remove(item);
                }
                //update exist item
                else
                {
                    item.MaChuyenHang = entity.MaChuyenHang;
                    item.MaDonHang = entity.MaDonHang;
                }
            }
        }

        private static void AddNewItem(PhuDinhData.PhuDinhEntities context, IEnumerable<PhuDinhData.tChuyenHangDonHang> gridDataSource)
        {
            foreach (var item in gridDataSource)
            {
                if (item.Ma == 0)
                {
                    context.tChuyenHangDonHangs.Add(item);
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
                var gridDataSource = this.tChuyenHangDonHangDataGrid.DataContext as IEnumerable<PhuDinhData.tChuyenHangDonHang>;

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
            PhuDinhData.tChuyenHangDonHang.tChuyenHangs = context.tChuyenHangs.ToList();
            PhuDinhData.tChuyenHangDonHang.tDonHangs = context.tDonHangs.ToList();

            var data = context.tChuyenHangDonHangs.ToList();

            foreach (var tChuyenHangDonHang in data)
            {
                tChuyenHangDonHang.ChuyenHang = PhuDinhData.tChuyenHangDonHang.tChuyenHangs.FirstOrDefault(
                    p => p.Ma == tChuyenHangDonHang.MaChuyenHang);
                tChuyenHangDonHang.DonHang = PhuDinhData.tChuyenHangDonHang.tDonHangs.FirstOrDefault(
                        p => p.Ma == tChuyenHangDonHang.MaDonHang);
            }

            this.tChuyenHangDonHangDataGrid.DataContext = data;

            this.tChuyenHangDonHangDataGrid.UpdateLayout();
        }
        #endregion
    }
}
