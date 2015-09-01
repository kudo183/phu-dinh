namespace CustomControl.DataGridColumnHeaderFilterModel
{
    public class HeaderComboBoxFilterModel : HeaderFilterBaseModel
    {
        public HeaderComboBoxFilterModel(string name)
            : base(name, "ComboBoxFilter")
        {
            _isUsed = true;
        }

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

        public override object GetFilterValue()
        {
            if (IsUsed == false)
                return null;

            return SelectedValue;
        }
    }
}
