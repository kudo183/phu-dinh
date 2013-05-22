using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for View.xaml
    /// </summary>
    public partial class rNhanVienGiaoHangView : UserControl
    {
        public rNhanVienGiaoHangView()
        {
            InitializeComponent();
        }

        private static void RemoveOrUpdateItem(PhuDinhData.PhuDinhEntities context, IEnumerable<PhuDinhData.rNhanVienGiaoHang> gridDataSource)
        {
            foreach (var item in context.rNhanVienGiaoHangs.ToList())
            {
                var entity = gridDataSource.FirstOrDefault(p => p.Ma == item.Ma);
                //remove deleted item
                if (entity == null)
                {
                    context.rNhanVienGiaoHangs.Remove(item);
                }
                //update exist item
                else
                {
                    item.MaPhuongTien = entity.MaPhuongTien;
                    item.TenNhanVien = entity.TenNhanVien;
                }
            }
        }

        private static void AddNewItem(PhuDinhData.PhuDinhEntities context, IEnumerable<PhuDinhData.rNhanVienGiaoHang> gridDataSource)
        {
            foreach (var item in gridDataSource)
            {
                if (item.Ma == 0)
                {
                    context.rNhanVienGiaoHangs.Add(item);
                }
            }
        }

        public void RefreshGrid()
        {
            var context = new PhuDinhData.PhuDinhEntities();

            this.rNhanVienGiaoHangDataGrid.DataContext = context.rNhanVienGiaoHangs.ToList();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var context = new PhuDinhData.PhuDinhEntities();
                var gridDataSource = this.rNhanVienGiaoHangDataGrid.DataContext as IEnumerable<PhuDinhData.rNhanVienGiaoHang>;

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
