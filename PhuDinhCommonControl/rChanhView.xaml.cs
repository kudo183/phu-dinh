using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for rChanhView.xaml
    /// </summary>
    public partial class rChanhView : UserControl
    {
        public rChanhView()
        {
            InitializeComponent();
        }
        private static void RemoveOrUpdateItem(PhuDinhData.PhuDinhEntities context, IEnumerable<PhuDinhData.rChanh> gridDataSource)
        {
            foreach (var item in context.rChanhs.ToList())
            {
                var entity = gridDataSource.FirstOrDefault(p => p.Ma == item.Ma);
                //remove deleted item
                if (entity == null)
                {
                    context.rChanhs.Remove(item);
                }
                //update exist item
                else
                {
                    item.TenChanh = entity.TenChanh;
                }
            }
        }

        private static void AddNewItem(PhuDinhData.PhuDinhEntities context, IEnumerable<PhuDinhData.rChanh> gridDataSource)
        {
            foreach (var item in gridDataSource)
            {
                if (item.Ma == 0)
                {
                    context.rChanhs.Add(item);
                }
            }
        }

        public void RefreshGrid()
        {
            var context = new PhuDinhData.PhuDinhEntities();

            this.rChanhDataGrid.DataContext = context.rChanhs.ToList();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var context = new PhuDinhData.PhuDinhEntities();
                var gridDataSource = this.rChanhDataGrid.DataContext as IEnumerable<PhuDinhData.rChanh>;

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
