using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for rKhachHangView.xaml
    /// </summary>
    public partial class rKhachHangView : BaseView
    {
        public rKhachHangView()
        {
            InitializeComponent();
        }
        private static void RemoveOrUpdateItem(PhuDinhData.PhuDinhEntities context, IEnumerable<PhuDinhData.rKhachHang> gridDataSource)
        {
            foreach (var item in context.rKhachHangs.ToList())
            {
                var entity = gridDataSource.FirstOrDefault(p => p.Ma == item.Ma);
                //remove deleted item
                if (entity == null)
                {
                    context.rKhachHangs.Remove(item);
                }
                //update exist item
                else
                {
                    item.TenKhachHang = entity.TenKhachHang;
                }
            }
        }

        private static void AddNewItem(PhuDinhData.PhuDinhEntities context, IEnumerable<PhuDinhData.rKhachHang> gridDataSource)
        {
            foreach (var item in gridDataSource)
            {
                if (item.Ma == 0)
                {
                    context.rKhachHangs.Add(item);
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
                var gridDataSource = this.rKhachHangDataGrid.DataContext as IEnumerable<PhuDinhData.rKhachHang>;

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
            PhuDinhData.rKhachHang.rDiaDiems = context.rDiaDiems.ToList();

            var data = context.rKhachHangs.ToList();

            foreach (var rKhachHang in data)
            {
                rKhachHang.DiaDiem = PhuDinhData.rKhachHang.rDiaDiems.FirstOrDefault(
                    p => p.Ma == rKhachHang.MaDiaDiem);
            }

            this.rKhachHangDataGrid.DataContext = data;

            this.rKhachHangDataGrid.UpdateLayout();
        }

        #endregion
    }
}
