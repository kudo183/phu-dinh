using System.Collections.Generic;
using System.Text;
using System.Windows;
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

        private void Button_CopyClick(object sender, System.Windows.RoutedEventArgs e)
        {
            var data = dg.ItemsSource as List<ReportDaily.ReportDailyRowData>;

            var builder = new StringBuilder();

            builder.AppendLine(string.Format("\t{0:dd/MM/yyyy}", dpNgay.dp.SelectedDate.Value.Date));
            foreach (var row in data)
            {
                builder.Append("\t");
                builder.Append(row.TenKhachHang);
                builder.Append("\t");
                builder.Append(row.SoLuong);
                builder.Append("\t");
                builder.Append("\t");
                builder.Append(row.TenMatHang);

                builder.AppendLine("");
            }

            Clipboard.SetText(builder.ToString());
        }
    }
}
