using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace PhuDinhCommonControl.CustomControl
{
    /// <summary>
    /// Interaction logic for DataGridComboBoxColumnExt.xaml
    /// </summary>
    public partial class DataGridComboBoxColumnExt : DataGridComboBoxColumn
    {
        public DataGridComboBoxColumnExt()
        {
            InitializeComponent();
        }

        protected override FrameworkElement GenerateEditingElement(DataGridCell cell, object dataItem)
        {
            FrameworkElement element = base.GenerateEditingElement(cell, dataItem);
            CopyItemsSource(element);
            return element;
        }

        protected override FrameworkElement GenerateElement(DataGridCell cell, object dataItem)
        {
            FrameworkElement element = base.GenerateElement(cell, dataItem);
            CopyItemsSource(element);
            return element;
        }

        private void CopyItemsSource(FrameworkElement element)
        {
            var binding = BindingOperations.GetBinding(this, ComboBox.ItemsSourceProperty);
            if (binding == null)
            {
                return;
            }

            BindingOperations.SetBinding(element, ComboBox.ItemsSourceProperty, binding);
        }
    }
}
