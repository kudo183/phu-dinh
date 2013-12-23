using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Expressions;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for rPhuongTienView.xaml
    /// </summary>
    public partial class rPhuongTienView : BaseView<PhuDinhData.rPhuongTien>
    {
        public Expression<Func<PhuDinhData.rPhuongTien, bool>> FilterPhuongTien { get; set; }

        private PhuDinhData.PhuDinhEntities _context = ContextFactory.CreateContext();

        private List<PhuDinhData.rPhuongTien> _origData;

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
                PhuDinhData.Repository.rPhuongTienRepository.Save(_context, data, _origData);
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
            _origData = PhuDinhData.Repository.rPhuongTienRepository.GetData(_context, FilterPhuongTien);
            dgPhuongTien.DataContext = _origData;

            dgPhuongTien.SelectedIndex = index;
        }
        #endregion
    }
}
