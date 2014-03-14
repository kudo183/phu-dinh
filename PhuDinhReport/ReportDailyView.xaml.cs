using PhuDinhData.ReportData;
using System;
using System.Windows.Controls;

namespace PhuDinhReport
{
    /// <summary>
    /// Interaction logic for ReportDailyView.xaml
    /// </summary>
    public partial class ReportDailyView : UserControl
    {
        public ReportDailyView()
        {
            InitializeComponent();

            var now = DateTime.Now;
            dpNgay.SelectedDate = now;
        }

        private void btnNgay_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var ngay = dpNgay.SelectedDate.Value.Date;

            dg.ItemsSource = ReportDaily.FilterByDate(ngay);
        }
    }
}
