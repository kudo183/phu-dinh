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
        private int _flag = 0;//used to skip SelectedDateChanged second fire because SelectedDateChanged fired twice
        public ReportDailyView()
        {
            InitializeComponent();

            dpNgay.dp.SelectedDate = DateTime.Now;
            Report();

            dpNgay.dp.SelectedDateChanged += dp_SelectedDateChanged;
        }

        void dp_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_flag == 0)
            {
                Report();
                _flag = 1;
                return;
            }

            _flag = 0;
        }

        private void Report()
        {
            var ngay = dpNgay.dp.SelectedDate.Value.Date;

            dg.ItemsSource = ReportDaily.FilterByDate(ngay);
        }
    }
}
