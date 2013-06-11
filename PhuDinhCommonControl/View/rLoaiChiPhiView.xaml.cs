using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;

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

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Save();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Cancel();
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
