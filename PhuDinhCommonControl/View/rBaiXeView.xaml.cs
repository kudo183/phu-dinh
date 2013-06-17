using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for rBaiXeView.xaml
    /// </summary>
    public partial class rBaiXeView : BaseView
    {
        public Expression<Func<PhuDinhData.rBaiXe, bool>> FilterBaiXe { get; set; }

        public rBaiXeView()
        {
            InitializeComponent();

            FilterBaiXe = (p => true);
        }

        #region Override base view method
        public override void Save()
        {
            try
            {
                var data = this.dgBaiXe.DataContext as List<PhuDinhData.rBaiXe>;
                PhuDinhData.Repository.rBaiXeRepository.Save(data, FilterBaiXe);
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
            this.dgBaiXe.DataContext = PhuDinhData.Repository.rBaiXeRepository.GetData(FilterBaiXe);
        }
        #endregion
    }
}
