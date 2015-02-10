namespace PhuDinhData.ViewModel.DataGridColumnHeaderFilterModel
{
    public abstract class HeaderFilterBaseModel : BindableObject, IHeaderFilterModel
    {
        public string FilterType { get; set; }

        protected HeaderFilterBaseModel(string name, string filterType)
        {
            _name = name;
            FilterType = filterType;
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name == value)
                {
                    return;
                }

                _name = value;
                RaisePropertyChanged("Name");
            }
        }
    }
}
