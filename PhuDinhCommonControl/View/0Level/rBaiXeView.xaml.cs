using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Expressions;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for rBaiXeView.xaml
    /// </summary>
    public partial class rBaiXeView : BaseView<PhuDinhData.rBaiXe>
    {
        public Expression<Func<PhuDinhData.rBaiXe, bool>> FilterBaiXe { get; set; }

        private PhuDinhData.PhuDinhEntities _context = ContextFactory.CreateContext();

        private List<PhuDinhData.rBaiXe> _origData;

        public rBaiXeView()
        {
            InitializeComponent();

            FilterBaiXe = (p => true);
        }

        #region Override base view method
        public override void CommitEdit()
        {
            dgBaiXe.CommitEdit();
            base.CommitEdit();
        }

        public override void Save()
        {
            CommitEdit();
            try
            {
                var data = dgBaiXe.DataContext as List<PhuDinhData.rBaiXe>;
                PhuDinhData.Repository.rBaiXeRepository.Save(_context, data, _origData);
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
            var index = dgBaiXe.SelectedIndex;

            _context = ContextFactory.CreateContext();
            _origData = PhuDinhData.Repository.rBaiXeRepository.GetData(_context, FilterBaiXe);
            dgBaiXe.DataContext = _origData;

            dgBaiXe.SelectedIndex = index;
        }
        #endregion
    }
}
