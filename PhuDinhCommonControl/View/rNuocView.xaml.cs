using System;
using System.Collections.Generic;
using System.Data;
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

        private readonly PhuDinhData.PhuDinhEntities _context = new PhuDinhData.PhuDinhEntities();

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
                PhuDinhData.Repository.rNuocRepository.Save(_context, data, FilterNuoc);
                RefreshView();
            }
            catch (Exception ex)
            {
                PhuDinhCommon.EntityFrameworkUtils.UndoContextChange(_context, EntityState.Modified);
            }
        }

        public override void Cancel()
        {
            RefreshView();
        }

        public override void RefreshView()
        {
            this.dgNuoc.DataContext = PhuDinhData.Repository.rNuocRepository.GetData(_context, FilterNuoc);
        }
        #endregion
    }
}
