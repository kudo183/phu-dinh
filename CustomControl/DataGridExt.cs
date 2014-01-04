using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace CustomControl
{
    public class DataGridExt : DataGrid
    {
        public readonly List<int> SkippedColumnIndex = new List<int>();

        public DataGridExt()
        {
            CanUserSortColumns = false;
            CanUserReorderColumns = false;
            CanUserResizeRows = false;
            HeadersVisibility = DataGridHeadersVisibility.Column;
        }

        public int FindFirstEditableColumnIndex(int beginIndex, DataGrid dataGrid)
        {
            var index = -1;
            for (int i = beginIndex; i < dataGrid.Columns.Count; i++)
            {
                if (dataGrid.Columns[i].IsReadOnly == false
                    && SkippedColumnIndex.Contains(i) == false)
                {
                    index = i;
                    break;
                }
            }

            return index;
        }

        public void FocusCell(int row, int column, bool callBeginEdit = true)
        {
            Keyboard.Focus(this);

            SelectedIndex = row;

            CurrentCell = new DataGridCellInfo(Items[row], Columns[column]);

            if(callBeginEdit)
            {
                BeginEdit();
            }
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            base.OnPreviewKeyDown(e);

            if (!Keyboard.IsKeyDown(Key.Tab)) return;

            var current = Columns.IndexOf(CurrentColumn);

            var index = FindFirstEditableColumnIndex(current + 1, this);

            if (index != -1)
            {
                CurrentCell = new DataGridCellInfo(Items[SelectedIndex], Columns[index]);

                BeginEdit();
            }
            else//move to next row, select first editable column
            {
                var firstEditableColumnIndex = FindFirstEditableColumnIndex(0, this);

                const int itemPlaceHolderCount = 1;
                if (SelectedIndex < Items.Count - 1 - itemPlaceHolderCount)
                {
                    SelectedIndex = SelectedIndex + 1;

                    CurrentCell = new DataGridCellInfo(Items[SelectedIndex], Columns[firstEditableColumnIndex]);

                    BeginEdit();
                }
                else //need add new item
                {
                    var type = ItemsSource.GetType();
                    var item = Items as IEditableCollectionViewAddNewItem;
                    item.AddNewItem(Activator.CreateInstance(type.GetGenericArguments()[0]));

                    SelectedIndex = Items.Count - 1 - itemPlaceHolderCount;

                    CurrentCell = new DataGridCellInfo(Items[SelectedIndex], Columns[firstEditableColumnIndex]);

                    BeginEdit();
                }
            }

            e.Handled = true;
        }

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
            if (editingElementType == typeof(DatePicker))
            {
                ActiveDatePicker(e.EditingElement as DatePicker);
                return;
            }
            if (editingElementType == typeof(TimeInput))
            {
                ActiveTimeInput(e.EditingElement as TimeInput);
                return;
            }

            //for template column: e.EditingElement is ContentPresenter
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
            Keyboard.Focus(txt);
            txt.SelectAll();
            combo.IsDropDownOpen = true;
        }

        private void ActiveTimeInput(TimeInput timeInput)
        {
            Keyboard.Focus(timeInput.txtHour);
        }

        private void ActiveDatePicker(DatePicker datePicker)
        {
            var txt = VisualTreeUtils.FindChild<TextBox>(datePicker, "PART_TextBox");
            Keyboard.Focus(txt);
            txt.Select(0, 2);
        }

        public event EventHandler HeaderAddButtonClick;

        protected void headerAddButton_Click(object sender, EventArgs e)
        {
            if (HeaderAddButtonClick == null)
            {
                return;
            }

            HeaderAddButtonClick(sender, e);
        }
    }
}
