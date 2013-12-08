using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Expressions;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for rNhaCungCapView.xaml
    /// </summary>
    public partial class rNhaCungCapView : BaseView
    {
        public Expression<Func<PhuDinhData.rNhaCungCap, bool>> FilterNhaCungCap { get; set; }

        private PhuDinhData.PhuDinhEntities _context = ContextFactory.CreateContext();

        private List<PhuDinhData.rNhaCungCap> _origData;

        public rNhaCungCapView()
        {
            InitializeComponent();

            FilterNhaCungCap = (p => true);
        }

        #region Override base view method
        public override void CommitEdit()
        {
            dgNhaCungCap.CommitEdit();
            base.CommitEdit();
        }

        public override void Save()
        {
            CommitEdit();
            try
            {
                var data = dgNhaCungCap.DataContext as List<PhuDinhData.rNhaCungCap>;
                PhuDinhData.Repository.rNhaCungCapRepository.Save(_context, data, _origData);
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
            var index = dgNhaCungCap.SelectedIndex;

            _context = ContextFactory.CreateContext();
            _origData = PhuDinhData.Repository.rNhaCungCapRepository.GetData(_context, FilterNhaCungCap);
            dgNhaCungCap.DataContext = _origData;

            dgNhaCungCap.SelectedIndex = index;
        }
        #endregion
    }
}
