using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for rBaiXeView.xaml
    /// </summary>
    public partial class rBaiXeView : UserControl
    {
        public rBaiXeView()
        {
            InitializeComponent();
        }
        private static void RemoveOrUpdateItem(PhuDinhData.PhuDinhEntities context, IEnumerable<PhuDinhData.rBaiXe> gridDataSource)
        {
            foreach (var item in context.rBaiXes.ToList())
            {
                var entity = gridDataSource.FirstOrDefault(p => p.Ma == item.Ma);
                //remove deleted item
                if (entity == null)
                {
                    context.rBaiXes.Remove(item);
                }
                //update exist item
                else
                {
                    item.DiaDiemBaiXe = entity.DiaDiemBaiXe;
                }
            }
        }

        private static void AddNewItem(PhuDinhData.PhuDinhEntities context, IEnumerable<PhuDinhData.rBaiXe> gridDataSource)
        {
            foreach (var item in gridDataSource)
            {
                if (item.Ma == 0)
                {
                    context.rBaiXes.Add(item);
                }
            }
        }

        public void RefreshGrid()
        {
            var context = new PhuDinhData.PhuDinhEntities();

            this.rBaiXeDataGrid.DataContext = context.rBaiXes.ToList();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var context = new PhuDinhData.PhuDinhEntities();
                var gridDataSource = this.rBaiXeDataGrid.DataContext as IEnumerable<PhuDinhData.rBaiXe>;

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
