using System;
using System.Windows;

namespace PhuDinhCommonControl
{
    public class DatagridFilter : DependencyObject
    {
        public static readonly DatagridFilter Instance = new DatagridFilter();
        private DatagridFilter() { }

        public event EventHandler DateFilterChanged;

        public DateTime DateFilter
        {
            get { return (DateTime)GetValue(DateFilterProperty); }
            set { SetValue(DateFilterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DateFilter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DateFilterProperty =
            DependencyProperty.Register("DateFilter", typeof(DateTime), typeof(DatagridFilter),
            new PropertyMetadata(DateTime.Now, OnDateFilterPropertyChanged));

        private static void OnDateFilterPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var t = sender as DatagridFilter;
            if (t.DateFilterChanged != null)
            {
                t.DateFilterChanged(t, EventArgs.Empty);
            }
        }
    }
}
