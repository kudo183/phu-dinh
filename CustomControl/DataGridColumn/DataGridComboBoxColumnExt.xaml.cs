using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace CustomControl
{
    /// <summary>
    /// Interaction logic for DataGridComboBoxColumnExt.xaml
    /// </summary>
    public partial class DataGridComboBoxColumnExt : DataGridComboBoxColumn
    {
        public event EventHandler HeaderAddButtonClick;

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

        void headerAddButton_Click(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is System.Windows.Controls.Primitives.DataGridColumnHeader)
            {
                return;
            }

            if (HeaderAddButtonClick == null)
            {
                return;
            }

            HeaderAddButtonClick(sender, e);
        }
    }
}
