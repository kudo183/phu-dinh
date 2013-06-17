using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for rNuocView.xaml
    /// </summary>
    public partial class rNuocView : BaseView
    {
        public Expression<Func<PhuDinhData.rNuoc, bool>> FilterNuoc { get; set; }

        public rNuocView()
        {
            InitializeComponent();

            FilterNuoc = (p => true);
        }

        #region Override base view method
        public override void Save()
        {
            try
            {
                var data = this.dgNuoc.DataContext as List<PhuDinhData.rNuoc>;
                PhuDinhData.Repository.rNuocRepository.Save(data, FilterNuoc);
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
            this.dgNuoc.DataContext = PhuDinhData.Repository.rNuocRepository.GetData(FilterNuoc);
        }
        #endregion
    }
}
