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

            dpNgay.dp.SelectedDateChanged += dp_SelectedDateChanged;

            dpNgay.dp.SelectedDate = DateTime.Now;
        }

        void dp_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Report();
        }

        private void Report()
        {
            var ngay = dpNgay.dp.SelectedDate.Value.Date;

            dg.ItemsSource = ReportDaily.FilterByDate(ngay);
        }
    }
}
