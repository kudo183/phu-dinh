﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for tDonHangView.xaml
    /// </summary>
    public partial class tDonHangView : BaseView
    {
        public tDonHangView()
        {
            InitializeComponent();
        }
        private static void RemoveOrUpdateItem(PhuDinhData.PhuDinhEntities context, IEnumerable<PhuDinhData.tDonHang> gridDataSource)
        {
            foreach (var item in context.tDonHangs.ToList())
            {
                var entity = gridDataSource.FirstOrDefault(p => p.Ma == item.Ma);
                //remove deleted item
                if (entity == null)
                {
                    context.tDonHangs.Remove(item);
                }
                //update exist item
                else
                {
                    item.MaChuyenHang = entity.MaChuyenHang;
                    item.MaKhachHang = entity.MaKhachHang;
                    item.MaBaiXe = entity.MaBaiXe;
                    item.MaChanh = entity.MaChanh;
                }
            }
        }

        private static void AddNewItem(PhuDinhData.PhuDinhEntities context, IEnumerable<PhuDinhData.tDonHang> gridDataSource)
        {
            foreach (var item in gridDataSource)
            {
                if (item.Ma == 0)
                {
                    context.tDonHangs.Add(item);
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
                var gridDataSource = this.tDonHangDataGrid.DataContext as IEnumerable<PhuDinhData.tDonHang>;

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
            PhuDinhData.tDonHang.tChuyenHangs = context.tChuyenHangs.ToList();
            PhuDinhData.tDonHang.rKhachHangs = context.rKhachHangs.ToList();
            PhuDinhData.tDonHang.rBaiXes = context.rBaiXes.ToList();
            PhuDinhData.tDonHang.rChanhs = context.rChanhs.ToList();

            var data = context.tDonHangs.ToList();

            foreach (var tDonHang in data)
            {
                tDonHang.ChuyenHang = PhuDinhData.tDonHang.tChuyenHangs.FirstOrDefault(
                    p => p.Ma == tDonHang.MaChuyenHang);
                tDonHang.KhachHang = PhuDinhData.tDonHang.rKhachHangs.FirstOrDefault(
                    p => p.Ma == tDonHang.MaKhachHang);
                tDonHang.BaiXe = PhuDinhData.tDonHang.rBaiXes.FirstOrDefault(
                    p => p.Ma == tDonHang.MaBaiXe);
                tDonHang.Chanh = PhuDinhData.tDonHang.rChanhs.FirstOrDefault(
                    p => p.Ma == tDonHang.MaChanh);
            }

            this.tDonHangDataGrid.DataContext = data;

            this.tDonHangDataGrid.UpdateLayout();
        }
        #endregion
    }
}