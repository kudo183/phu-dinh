using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Expressions;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for rLoaiHangView.xaml
    /// </summary>
    public partial class rLoaiHangView : BaseView
    {
        public Expression<Func<PhuDinhData.rLoaiHang, bool>> FilterLoaiHang { get; set; }

        private PhuDinhData.PhuDinhEntities _context = ContextFactory.CreateContext();

        private List<PhuDinhData.rLoaiHang> _origData;

        public rLoaiHangView()
        {
            InitializeComponent();

            FilterLoaiHang = (p => true);
        }

        #region Override base view method
        public override void CommitEdit()
        {
            dgLoaiHang.CommitEdit();
            base.CommitEdit();
        }

        public override void Save()
        {
            CommitEdit();
            try
            {
                var data = dgLoaiHang.DataContext as List<PhuDinhData.rLoaiHang>;
                PhuDinhData.Repository.rLoaiHangRepository.Save(_context, data, _origData);
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
            var index = dgLoaiHang.SelectedIndex;

            _context = ContextFactory.CreateContext();
            _origData = PhuDinhData.Repository.rLoaiHangRepository.GetData(_context, FilterLoaiHang);
            dgLoaiHang.DataContext = _origData;

            dgLoaiHang.SelectedIndex = index;
        }
        #endregion
    }
}
