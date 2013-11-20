using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for rPhuongTienView.xaml
    /// </summary>
    public partial class rPhuongTienView : BaseView
    {
        public Expression<Func<PhuDinhData.rPhuongTien, bool>> FilterPhuongTien { get; set; }

        private PhuDinhData.PhuDinhEntities _context = ContextFactory.CreateContext();

        public rPhuongTienView()
        {
            InitializeComponent();

            FilterPhuongTien = (p => true);
        }

        #region Override base view method
        public override void CommitEdit()
        {
            dgPhuongTien.CommitEdit();
            base.CommitEdit();
        }

        public override void Save()
        {
            CommitEdit();
            try
            {
                var data = dgPhuongTien.DataContext as List<PhuDinhData.rPhuongTien>;
                PhuDinhData.Repository.rPhuongTienRepository.Save(_context, data, FilterPhuongTien);
                RefreshView();
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
            var index = dgPhuongTien.SelectedIndex;

            _context = ContextFactory.CreateContext();
            dgPhuongTien.DataContext = PhuDinhData.Repository.rPhuongTienRepository.GetData(_context, FilterPhuongTien);

            dgPhuongTien.SelectedIndex = index;
        }
        #endregion
    }
}
