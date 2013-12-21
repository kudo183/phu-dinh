namespace PhuDinhData.ViewModel.DataGridColumnHeaderFilterModel
{
    public abstract class HeaderFilterBaseModel : BindableObject, IHeaderFilterModel
    {
        protected HeaderFilterBaseModel(string name)
        {
            _name = name;
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
