using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Expressions;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for rLoaiChiPhiView.xaml
    /// </summary>
    public partial class rLoaiChiPhiView : BaseView<PhuDinhData.rLoaiChiPhi>
    {
        public Expression<Func<PhuDinhData.rLoaiChiPhi, bool>> FilterLoaiChiPhi { get; set; }

        private PhuDinhData.PhuDinhEntities _context = ContextFactory.CreateContext();

        private List<PhuDinhData.rLoaiChiPhi> _origData;

        public rLoaiChiPhiView()
        {
            InitializeComponent();

            FilterLoaiChiPhi = (p => true);
        }

        #region Override base view method
        public override void CommitEdit()
        {
            dgLoaiChiPhi.CommitEdit();
            base.CommitEdit();
        }

        public override void Save()
        {
            CommitEdit();
            try
            {
                var data = dgLoaiChiPhi.DataContext as List<PhuDinhData.rLoaiChiPhi>;
                PhuDinhData.Repository.rLoaiChiPhiRepository.Save(_context, data, _origData);
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
            var index = dgLoaiChiPhi.SelectedIndex;

            _context = ContextFactory.CreateContext();
            _origData = PhuDinhData.Repository.rLoaiChiPhiRepository.GetData(_context, FilterLoaiChiPhi);            
            dgLoaiChiPhi.DataContext = _origData;

            dgLoaiChiPhi.SelectedIndex = index;
        }
        #endregion
    }
}
