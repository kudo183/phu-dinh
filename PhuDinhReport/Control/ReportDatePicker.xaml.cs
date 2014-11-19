using System;
using System.Windows;
using System.Windows.Controls;

namespace PhuDinhReport.Control
{
    /// <summary>
    /// Interaction logic for ReportDatePicker.xaml
    /// </summary>
    public partial class ReportDatePicker : UserControl
    {
        public DateTime? Ngay
        {
            get { return dpNgay.dp.SelectedDate; }
            set { dpNgay.dp.SelectedDate = value; }
        }

        public DateTime? TuNgay
        {
            get { return dpTuNgay.dp.SelectedDate; }
            set { dpTuNgay.dp.SelectedDate = value; }
        }

        public DateTime? DenNgay
        {
            get { return dpDenNgay.dp.SelectedDate; }
            set { dpDenNgay.dp.SelectedDate = value; }
        }

        public string NgayMsg
        {
            get { return txtNgay.Text; }
            set
            {
                txtNgay.Foreground = System.Windows.Media.Brushes.Blue;
                txtNgay.Text = value;
                
                txtTuNgayDenNgay.Foreground = System.Windows.Media.Brushes.Black;
            }
        }

        public string TuNgayDenNgayMsg
        {
            get { return txtTuNgayDenNgay.Text; }
            set
            {
                txtTuNgayDenNgay.Foreground = System.Windows.Media.Brushes.Blue;
                txtTuNgayDenNgay.Text = value;
                
                txtNgay.Foreground = System.Windows.Media.Brushes.Black;
            }
        }

        public event EventHandler NgaySelected;
        public event EventHandler TuNgayDenNgaySelected;

        public ReportDatePicker()
        {
            InitializeComponent();
            dpNgay.dp.SelectedDateChanged += dp_SelectedDateChanged;
            btnNgay.Click += btnNgay_Click;
            btnTuNgayDenNgay.Click += btnTuNgayDenNgay_Click;
            
            var now = DateTime.Now;
            dpNgay.dp.SelectedDate = now;
            dpTuNgay.dp.SelectedDate = now;
            dpDenNgay.dp.SelectedDate = now;
        }

        void btnTuNgayDenNgay_Click(object sender, RoutedEventArgs e)
        {
            OnTuNgayDenNgaySelected();
        }

        void btnNgay_Click(object sender, RoutedEventArgs e)
        {
            OnNgaySelected();
        }

        void dp_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            OnNgaySelected();
        }

        protected void OnNgaySelected()
        {
            if (NgaySelected != null)
            {
                NgaySelected(this, null);
            }
        }

        protected void OnTuNgayDenNgaySelected()
        {
            if (TuNgayDenNgaySelected != null)
            {
                TuNgayDenNgaySelected(this, null);
            }
        }
    }
}
