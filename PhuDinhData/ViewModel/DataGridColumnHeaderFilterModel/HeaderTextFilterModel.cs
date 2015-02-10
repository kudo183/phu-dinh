namespace PhuDinhData.ViewModel.DataGridColumnHeaderFilterModel
{
    public class HeaderTextFilterModel : HeaderFilterBaseModel
    {
        public HeaderTextFilterModel(string name) : base(name, "TextFilter") { }

        private string _text = string.Empty;
        public string Text
        {
            get { return _text; }
            set
            {
                if (_text == value)
                {
                    return;
                }

                _text = value;
                RaisePropertyChanged("Text");
            }
        }
    }
}
