using System.Windows.Controls;

namespace PhuDinhCommonControl.CustomControl
{
    public class DataGridExt : DataGrid
    {
        protected override void OnPreparingCellForEdit(DataGridPreparingCellForEditEventArgs e)
        {
            base.OnPreparingCellForEdit(e);
            var count = System.Windows.Media.VisualTreeHelper.GetChildrenCount(e.EditingElement);

            for (int i = 0; i < count; i++)
            {
                var element = System.Windows.Media.VisualTreeHelper.GetChild(e.EditingElement, i);
                var type = element.GetType();
                if (type == typeof(DatePicker))
                {
                    (element as DatePicker).Focus();
                }
                else if (type == typeof(ComboBox))
                {
                    var combo = (element as ComboBox);
                    var txt = VisualTreeUtils.FindChild<TextBox>(combo, "PART_EditableTextBox");
                    System.Windows.Input.Keyboard.Focus(txt);
                    txt.SelectAll();
                    combo.IsDropDownOpen = true;
                }
                else if (type == typeof(PhuDinhCommonControl.CustomControl.TimeInput))
                {
                    var timeInput = (element as PhuDinhCommonControl.CustomControl.TimeInput);
                    System.Windows.Input.Keyboard.Focus(timeInput.txtHour);
                }
            }
        }
    }
}
