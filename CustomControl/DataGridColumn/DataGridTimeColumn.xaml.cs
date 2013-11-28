using System.Windows;
using System.Windows.Controls;

namespace CustomControl
{
    /// <summary>
    /// Interaction logic for DataGridTimeColumn.xaml
    /// </summary>
    public partial class DataGridTimeColumn : DataGridBoundColumn
    {
        public DataGridTimeColumn()
        {
            InitializeComponent();
            FontSize = 16;
        }

        public double FontSize { get; set; }

        protected override FrameworkElement GenerateEditingElement(DataGridCell cell, object dataItem)
        {
            var dp = new TimeInput { FontSize = FontSize };
            dp.SetBinding(TimeInput.ValueProperty, Binding);

            return dp;
        }

        protected override FrameworkElement GenerateElement(DataGridCell cell, object dataItem)
        {
            var txt = new TextBlock { FontSize = FontSize };
            txt.SetBinding(TextBlock.TextProperty, Binding);

            return txt;
        }

        protected override object PrepareCellForEdit(FrameworkElement editingElement, RoutedEventArgs editingEventArgs)
        {
            var dp = editingElement as TimeInput;

            return dp.Value;
        }
    }
}
