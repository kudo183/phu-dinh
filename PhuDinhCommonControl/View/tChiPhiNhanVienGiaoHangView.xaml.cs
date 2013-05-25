using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for tChiPhiNhanVienGiaoHangView.xaml
    /// </summary>
    public partial class tChiPhiNhanVienGiaoHangView : UserControl
    {
        public tChiPhiNhanVienGiaoHangView()
        {
            InitializeComponent();
        }
        private static void RemoveOrUpdateItem(PhuDinhData.PhuDinhEntities context, IEnumerable<PhuDinhData.tChiPhiNhanVienGiaoHang> gridDataSource)
        {
            foreach (var item in context.tChiPhiNhanVienGiaoHangs.ToList())
            {
                var entity = gridDataSource.FirstOrDefault(p => p.Ma == item.Ma);
                //remove deleted item
                if (entity == null)
                {
                    context.tChiPhiNhanVienGiaoHangs.Remove(item);
                }
                //update exist item
                else
                {
                    item.MaLoaiChiPhi = entity.MaLoaiChiPhi;
                    item.MaNhanVienGiaoHang = entity.MaNhanVienGiaoHang;
                    item.Ngay = entity.Ngay;
                    item.SoTien = entity.SoTien;
                    item.GhiChu = entity.GhiChu;
                }
            }
        }

        private static void AddNewItem(PhuDinhData.PhuDinhEntities context, IEnumerable<PhuDinhData.tChiPhiNhanVienGiaoHang> gridDataSource)
        {
            foreach (var item in gridDataSource)
            {
                if (item.Ma == 0)
                {
                    context.tChiPhiNhanVienGiaoHangs.Add(item);
                }
            }
        }

        public void RefreshGrid()
        {
            var context = new PhuDinhData.PhuDinhEntities();
            PhuDinhData.tChiPhiNhanVienGiaoHang.rLoaiChiPhis = context.rLoaiChiPhis.ToList();
            PhuDinhData.tChiPhiNhanVienGiaoHang.rNhanVienGiaoHangs = context.rNhanVienGiaoHangs.ToList();

            var data = context.tChiPhiNhanVienGiaoHangs.ToList();

            foreach (var tChiPhiNhanVienGiaoHang in data)
            {
                tChiPhiNhanVienGiaoHang.NhanVienGiaoHang = PhuDinhData.tChiPhiNhanVienGiaoHang.rNhanVienGiaoHangs.FirstOrDefault(
                    p => p.Ma == tChiPhiNhanVienGiaoHang.MaNhanVienGiaoHang);
                tChiPhiNhanVienGiaoHang.LoaiChiPhi = PhuDinhData.tChiPhiNhanVienGiaoHang.rLoaiChiPhis.FirstOrDefault(
                        p => p.Ma == tChiPhiNhanVienGiaoHang.MaLoaiChiPhi);
            }

            this.tChiPhiNhanVienGiaoHangDataGrid.DataContext = data;

            this.tChiPhiNhanVienGiaoHangDataGrid.UpdateLayout();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var context = new PhuDinhData.PhuDinhEntities();
                var gridDataSource = this.tChiPhiNhanVienGiaoHangDataGrid.DataContext as IEnumerable<PhuDinhData.tChiPhiNhanVienGiaoHang>;

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
