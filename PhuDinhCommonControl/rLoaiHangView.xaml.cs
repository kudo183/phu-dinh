using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for rLoaiHangView.xaml
    /// </summary>
    public partial class rLoaiHangView : UserControl
    {
        public rLoaiHangView()
        {
            InitializeComponent();
        }
        private static void RemoveOrUpdateItem(PhuDinhData.PhuDinhEntities context, IEnumerable<PhuDinhData.rLoaiHang> gridDataSource)
        {
            foreach (var item in context.rLoaiHangs.ToList())
            {
                var entity = gridDataSource.FirstOrDefault(p => p.Ma == item.Ma);
                //remove deleted item
                if (entity == null)
                {
                    context.rLoaiHangs.Remove(item);
                }
                //update exist item
                else
                {
                    item.TenLoai = entity.TenLoai;
                }
            }
        }

        private static void AddNewItem(PhuDinhData.PhuDinhEntities context, IEnumerable<PhuDinhData.rLoaiHang> gridDataSource)
        {
            foreach (var item in gridDataSource)
            {
                if (item.Ma == 0)
                {
                    context.rLoaiHangs.Add(item);
                }
            }
        }

        public void RefreshGrid()
        {
            var context = new PhuDinhData.PhuDinhEntities();

            this.rLoaiHangDataGrid.DataContext = context.rLoaiHangs.ToList();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var context = new PhuDinhData.PhuDinhEntities();
                var gridDataSource = this.rLoaiHangDataGrid.DataContext as IEnumerable<PhuDinhData.rLoaiHang>;

                RemoveOrUpdateItem(context, gridDataSource);

                AddNewItem(context, gridDataSource);

                context.SaveChanges();
                RefreshGrid();
            }
            catch (Exception ex)
            {
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshGrid();
        }
    }
}
