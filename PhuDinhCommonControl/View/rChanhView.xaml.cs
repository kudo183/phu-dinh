using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for rChanhView.xaml
    /// </summary>
    public partial class rChanhView : BaseView
    {
        public Expression<Func<PhuDinhData.rChanh, bool>> FilterChanh { get; set; }
        public Expression<Func<PhuDinhData.rBaiXe, bool>> FilterBaiXe { get; set; }

        public rChanhView()
        {
            InitializeComponent();

            FilterChanh = (p => true);
            FilterBaiXe = (p => true);
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
                var data = this.dgChanh.DataContext as List<PhuDinhData.rChanh>;
                PhuDinhData.Repository.rChanhRepository.Save(data, FilterChanh);
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

            PhuDinhData.rChanh.rBaiXes = PhuDinhData.Repository.rBaiXeRepository.GetData(context, FilterBaiXe);

            var data = PhuDinhData.Repository.rChanhRepository.GetData(context, FilterChanh);

            foreach (var rChanh in data)
            {
                rChanh.BaiXe = PhuDinhData.rChanh.rBaiXes.FirstOrDefault(
                    p => p.Ma == rChanh.MaBaiXe);
            }

            this.dgChanh.DataContext = data;

            this.dgChanh.UpdateLayout();
        }
        #endregion
    }
}
