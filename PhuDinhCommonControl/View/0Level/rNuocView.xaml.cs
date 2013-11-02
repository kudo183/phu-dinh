using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for rNuocView.xaml
    /// </summary>
    public partial class rNuocView : BaseView
    {
        public Expression<Func<PhuDinhData.rNuoc, bool>> FilterNuoc { get; set; }

        private PhuDinhData.PhuDinhEntities _context = ContextFactory.CreateContext();

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
                var data = dgNuoc.DataContext as List<PhuDinhData.rNuoc>;
                PhuDinhData.Repository.rNuocRepository.Save(_context, data, FilterNuoc);
            }
            catch (Exception ex)
            {
                PhuDinhCommon.EntityFrameworkUtils.UndoContextChange(_context, EntityState.Modified);
            }

            base.Save();
        }

        public override void Cancel()
        {
            RefreshView();

            base.Cancel();
        }

        public override void RefreshView()
        {
            var index = dgNuoc.SelectedIndex;

            _context = ContextFactory.CreateContext();
            dgNuoc.DataContext = PhuDinhData.Repository.rNuocRepository.GetData(_context, FilterNuoc);

            dgNuoc.SelectedIndex = index;
        }
        #endregion
    }
}
