using System;

namespace CustomControl.DataGridColumnHeaderFilterModel
{
    public class HeaderDateFilterModel : HeaderFilterBaseModel
    {
        public HeaderDateFilterModel(string name)
            : base(name, "DateFilter")
        {
            _isUsed = true;
        }

        private DateTime _date = DateTime.Now.Date;
        public DateTime Date
        {
            get { return _date; }
            set
            {
                if (_date == value)
                {
                    return;
                }

                _date = value;
                RaisePropertyChanged("Date");
            }
        }

        public override object GetFilterValue()
        {
            if (IsUsed == false)
                return null;

            return Date;
        }
    }
}
