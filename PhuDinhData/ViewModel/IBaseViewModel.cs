using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using Common;
using PhuDinhData.Filter;
using PhuDinhData.Repository;

namespace PhuDinhData.ViewModel
{
    public interface IBaseViewModel<T> where T : BindableObject
    {
        event EventHandler HeaderFilterChanged;
        void Load();
        void Unload();
        void Dispose();
        void RefreshData();
        List<Repository<T>.ChangedItemData> Save();
        void SetDefaultValue(string columnName, object value);
        void SetReferenceFilter<T1>(string columnName, Expression<Func<T1, bool>> filter) where T1 : BindableObject;
        ObservableCollection<T> Entity { get; set; }
        IFilter<T> MainFilter { get; set; }
        bool IsValid { get; set; }
        void UpdateReferenceData(string columnName);
    }
}
