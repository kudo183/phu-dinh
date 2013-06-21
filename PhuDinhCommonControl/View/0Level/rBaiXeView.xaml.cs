using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for rBaiXeView.xaml
    /// </summary>
    public partial class rBaiXeView : BaseView
    {
        public Expression<Func<PhuDinhData.rBaiXe, bool>> FilterBaiXe { get; set; }

        private readonly PhuDinhData.PhuDinhEntities _context = new PhuDinhData.PhuDinhEntities();

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
                PhuDinhData.Repository.rBaiXeRepository.Save(_context, data, FilterBaiXe);
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
            this.dgBaiXe.DataContext = PhuDinhData.Repository.rBaiXeRepository.GetData(_context, FilterBaiXe);
        }
        #endregion
    }
}
