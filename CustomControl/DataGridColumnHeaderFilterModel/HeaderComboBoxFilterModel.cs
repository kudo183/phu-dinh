using System;

namespace CustomControl.DataGridColumnHeaderFilterModel
{
    public class HeaderComboBoxFilterModel : HeaderFilterBaseModel
    {
        public HeaderComboBoxFilterModel(string name) : base(name, "ComboBoxFilter") { }

        private object _itemSource;
        public object ItemSource
        {
            get { return _itemSource; }
            set
            {
                if (_itemSource == value)
                {
                    return;
                }

                _itemSource = value;
                RaisePropertyChanged("ItemSource");
            }
        }

        private object _selectedValue;
        public object SelectedValue
        {
            get { return _selectedValue; }
            set
            {
                if (_selectedValue == value)
                {
                    return;
                }

                _selectedValue = value;
                RaisePropertyChanged("SelectedValue");
            }
        }

        private bool _isUsed = true;
        public bool IsUsed
        {
            get { return _isUsed; }
            set
            {
                if (_isUsed == value)
                {
                    return;
                }

                _isUsed = value;
                RaisePropertyChanged("IsUsed");
            }
        }
    }
}
