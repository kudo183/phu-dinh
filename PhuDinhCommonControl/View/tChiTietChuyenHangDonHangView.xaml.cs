using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for tChiTietChuyenHangDonHangView.xaml
    /// </summary>
    public partial class tChiTietChuyenHangDonHangView : BaseView
    {
        public tChiTietChuyenHangDonHangView()
        {
            InitializeComponent();
        }
        private static void RemoveOrUpdateItem(PhuDinhData.PhuDinhEntities context, IEnumerable<PhuDinhData.tChiTietChuyenHangDonHang> gridDataSource)
        {
            foreach (var item in context.tChiTietChuyenHangDonHangs.ToList())
            {
                var entity = gridDataSource.FirstOrDefault(p => p.Ma == item.Ma);
                //remove deleted item
                if (entity == null)
                {
                    context.tChiTietChuyenHangDonHangs.Remove(item);
                }
                //update exist item
                else
                {
                    item.MaChuyenHangDonHang = entity.MaChuyenHangDonHang;
                    item.MaMatHang = entity.MaMatHang;
                    item.SoLuong = entity.SoLuong;
                }
            }
        }

        private static void AddNewItem(PhuDinhData.PhuDinhEntities context, IEnumerable<PhuDinhData.tChiTietChuyenHangDonHang> gridDataSource)
        {
            foreach (var item in gridDataSource)
            {
                if (item.Ma == 0)
                {
                    context.tChiTietChuyenHangDonHangs.Add(item);
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
                var gridDataSource = this.tChiTietChuyenHangDonHangDataGrid.DataContext as IEnumerable<PhuDinhData.tChiTietChuyenHangDonHang>;

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
            PhuDinhData.tChiTietChuyenHangDonHang.tChuyenHangDonHangs = context.tChuyenHangDonHangs.ToList();
            PhuDinhData.tChiTietChuyenHangDonHang.tMatHangs = context.tMatHangs.ToList();

            var data = context.tChiTietChuyenHangDonHangs.ToList();

            foreach (var tChiTietChuyenHangDonHang in data)
            {
                tChiTietChuyenHangDonHang.ChuyenHangDonHang = PhuDinhData.tChiTietChuyenHangDonHang.tChuyenHangDonHangs.FirstOrDefault(
                    p => p.Ma == tChiTietChuyenHangDonHang.MaChuyenHangDonHang);
                tChiTietChuyenHangDonHang.MatHang = PhuDinhData.tChiTietChuyenHangDonHang.tMatHangs.FirstOrDefault(
                        p => p.Ma == tChiTietChuyenHangDonHang.MaMatHang);
            }

            this.tChiTietChuyenHangDonHangDataGrid.DataContext = data;

            this.tChiTietChuyenHangDonHangDataGrid.UpdateLayout();
        }
        #endregion
    }
}
