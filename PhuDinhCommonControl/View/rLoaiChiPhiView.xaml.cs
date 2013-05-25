using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for rLoaiChiPhiView.xaml
    /// </summary>
    public partial class rLoaiChiPhiView : UserControl
    {
        public rLoaiChiPhiView()
        {
            InitializeComponent();
        }
        private static void RemoveOrUpdateItem(PhuDinhData.PhuDinhEntities context, IEnumerable<PhuDinhData.rLoaiChiPhi> gridDataSource)
        {
            foreach (var item in context.rLoaiChiPhis.ToList())
            {
                var entity = gridDataSource.FirstOrDefault(p => p.Ma == item.Ma);
                //remove deleted item
                if (entity == null)
                {
                    context.rLoaiChiPhis.Remove(item);
                }
                //update exist item
                else
                {
                    item.TenLoaiChiPhi = entity.TenLoaiChiPhi;
                }
            }
        }

        private static void AddNewItem(PhuDinhData.PhuDinhEntities context, IEnumerable<PhuDinhData.rLoaiChiPhi> gridDataSource)
        {
            foreach (var item in gridDataSource)
            {
                if (item.Ma == 0)
                {
                    context.rLoaiChiPhis.Add(item);
                }
            }
        }

        public void RefreshGrid()
        {
            var context = new PhuDinhData.PhuDinhEntities();

            this.rLoaiChiPhiDataGrid.DataContext = context.rLoaiChiPhis.ToList();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var context = new PhuDinhData.PhuDinhEntities();
                var gridDataSource = this.rLoaiChiPhiDataGrid.DataContext as IEnumerable<PhuDinhData.rLoaiChiPhi>;

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
