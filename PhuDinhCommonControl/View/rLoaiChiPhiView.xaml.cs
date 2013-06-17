using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for rLoaiChiPhiView.xaml
    /// </summary>
    public partial class rLoaiChiPhiView : BaseView
    {
        public Expression<Func<PhuDinhData.rLoaiChiPhi, bool>> FilterLoaiChiPhi { get; set; }

        public rLoaiChiPhiView()
        {
            InitializeComponent();

            FilterLoaiChiPhi = (p => true);
        }

        #region Override base view method
        public override void Save()
        {
            try
            {
                var data = this.dgLoaiChiPhi.DataContext as List<PhuDinhData.rLoaiChiPhi>;
                PhuDinhData.Repository.rLoaiChiPhiRepository.Save(data, FilterLoaiChiPhi);
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
            this.dgLoaiChiPhi.DataContext = PhuDinhData.Repository.rLoaiChiPhiRepository.GetData(FilterLoaiChiPhi);
        }
        #endregion
    }
}
