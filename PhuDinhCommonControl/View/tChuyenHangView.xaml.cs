using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for tChuyenHangView.xaml
    /// </summary>
    public partial class tChuyenHangView : BaseView
    {
        public tChuyenHangView()
        {
            InitializeComponent();
        }
        private static void RemoveOrUpdateItem(PhuDinhData.PhuDinhEntities context, IEnumerable<PhuDinhData.tChuyenHang> gridDataSource)
        {
            foreach (var item in context.tChuyenHangs.ToList())
            {
                var entity = gridDataSource.FirstOrDefault(p => p.Ma == item.Ma);
                //remove deleted item
                if (entity == null)
                {
                    context.tChuyenHangs.Remove(item);
                }
                //update exist item
                else
                {
                    item.MaNhanVienGiaoHang = entity.MaNhanVienGiaoHang;
                    item.Ngay = entity.Ngay;
                    item.Gio = entity.Gio;
                }
            }
        }

        private static void AddNewItem(PhuDinhData.PhuDinhEntities context, IEnumerable<PhuDinhData.tChuyenHang> gridDataSource)
        {
            foreach (var item in gridDataSource)
            {
                if (item.Ma == 0)
                {
                    context.tChuyenHangs.Add(item);
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
                var gridDataSource = this.tChuyenHangDataGrid.DataContext as IEnumerable<PhuDinhData.tChuyenHang>;

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
            PhuDinhData.tChuyenHang.rNhanVienGiaoHangs = context.rNhanVienGiaoHangs.ToList();

            var data = context.tChuyenHangs.ToList();

            foreach (var tDonHang in data)
            {
                tDonHang.NhanVienGiaoHang = PhuDinhData.tChuyenHang.rNhanVienGiaoHangs.FirstOrDefault(
                    p => p.Ma == tDonHang.MaNhanVienGiaoHang);
            }

            this.tChuyenHangDataGrid.DataContext = data;

            this.tChuyenHangDataGrid.UpdateLayout();
        }
        #endregion
    }
}
