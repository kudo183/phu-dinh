using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for tChiPhiNhanVienGiaoHangView.xaml
    /// </summary>
    public partial class tChiPhiNhanVienGiaoHangView : BaseView
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
                var gridDataSource = this.tChiPhiNhanVienGiaoHangDataGrid.DataContext as IEnumerable<PhuDinhData.tChiPhiNhanVienGiaoHang>;

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

            var collection = new ObservableCollection<PhuDinhData.tChiPhiNhanVienGiaoHang>(context.tChiPhiNhanVienGiaoHangs.ToList());
            collection.CollectionChanged += collection_CollectionChanged;
            this.tChiPhiNhanVienGiaoHangDataGrid.DataContext = collection;

            this.tChiPhiNhanVienGiaoHangDataGrid.UpdateLayout();
        }

        void collection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var chiPhiNhanVienGiaoHang = e.NewItems[0] as PhuDinhData.tChiPhiNhanVienGiaoHang;
                chiPhiNhanVienGiaoHang.Ngay = DateTime.Now;
            }
        }

        #endregion
    }
}
