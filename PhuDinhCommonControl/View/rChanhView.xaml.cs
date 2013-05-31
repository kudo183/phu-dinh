﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for rChanhView.xaml
    /// </summary>
    public partial class rChanhView : BaseView
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
                var gridDataSource = this.rChanhDataGrid.DataContext as IEnumerable<PhuDinhData.rChanh>;

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
            PhuDinhData.rChanh.rBaiXes = context.rBaiXes.ToList();

            var data = context.rChanhs.ToList();

            foreach (var rChanh in data)
            {
                rChanh.BaiXe = PhuDinhData.rChanh.rBaiXes.FirstOrDefault(
                    p => p.Ma == rChanh.MaBaiXe);
            }

            this.rChanhDataGrid.DataContext = data;

            this.rChanhDataGrid.UpdateLayout();
        }
        #endregion
    }
}
