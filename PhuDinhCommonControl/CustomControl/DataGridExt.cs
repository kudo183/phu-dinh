using System.Windows.Controls;

namespace PhuDinhCommonControl.CustomControl
{
    public class DataGridExt : DataGrid
    {
        protected override void OnPreparingCellForEdit(DataGridPreparingCellForEditEventArgs e)
        {
            base.OnPreparingCellForEdit(e);
            var count = System.Windows.Media.VisualTreeHelper.GetChildrenCount(e.EditingElement);

            var editingElementType = e.EditingElement.GetType();
            if (editingElementType == typeof(ComboBox))
            {
                ActiveComboBox(e.EditingElement as ComboBox);
                return;
            }

            for (int i = 0; i < count; i++)
            {
                var element = System.Windows.Media.VisualTreeHelper.GetChild(e.EditingElement, i);
                var type = element.GetType();
                if (type == typeof(DatePicker))
                {
                    ActiveDatePicker(element as DatePicker);
                }
                else if (type == typeof(ComboBox))
                {
                    ActiveComboBox(element as ComboBox);
                }
                else if (type == typeof(TimeInput))
                {
                    ActiveTimeInput(element as TimeInput);
                }
            }
        }

        private void ActiveComboBox(ComboBox combo)
        {
            var txt = VisualTreeUtils.FindChild<TextBox>(combo, "PART_EditableTextBox");
            System.Windows.Input.Keyboard.Focus(txt);
            txt.SelectAll();
            combo.IsDropDownOpen = true;
        }

        private void ActiveTimeInput(TimeInput timeInput)
        {
            System.Windows.Input.Keyboard.Focus(timeInput.txtHour);
        }

        private void ActiveDatePicker(DatePicker datePicker)
        {
            var txt = VisualTreeUtils.FindChild<TextBox>(datePicker, "PART_TextBox");
            System.Windows.Input.Keyboard.Focus(txt);
            txt.Select(0, 2);
        }
    }
}
