using System;
using System.Linq.Expressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CustomControl;
using PhuDinhCommon;
using PhuDinhData;
using PhuDinhData.ViewModel;

namespace PhuDinhCommonControl
{
    public class BaseView<T> : UserControl where T : BindableObject
    {
        protected BaseViewModel<T> _viewModel;

        public event EventHandler AfterSave;
        public event EventHandler AfterCancel;

        public event EventHandler MoveFocus;

        public DataGridExt dg { get; set; }

        protected BaseView()
        {
            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
        }

        protected virtual void OnLoaded(object sender, RoutedEventArgs e)
        {
            var designTime = System.ComponentModel.DesignerProperties.GetIsInDesignMode(new DependencyObject());
            if (designTime == true)
            {
                return;
            }

            if (_viewModel == null)
            {
                return;
            }

            _viewModel.HeaderFilterChanged += OnHeaderFilterChanged;

            _viewModel.Load();

            RefreshView();
        }

        protected virtual void OnUnloaded(object sender, RoutedEventArgs e)
        {
            if (_viewModel == null)
                return;

            _viewModel.HeaderFilterChanged -= OnHeaderFilterChanged;

            _viewModel.Unload();
            _viewModel.Dispose();
        }

        protected virtual void OnHeaderFilterChanged(object sender, EventArgs e)
        {
            RefreshView();
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control)
            {
                switch (e.Key)
                {
                    case Key.Q:
                        Save();
                        break;
                    case Key.S:
                        Save();

                        if (MoveFocus != null)
                        {
                            MoveFocus(this, null);
                        }
                        break;
                    case Key.Z:
                        Cancel();
                        break;
                }
            }
        }

        public virtual void CommitEdit()
        {
            dg.CommitEdit();
        }

        public virtual void RefreshView()
        {
            if (_viewModel == null)
                return;

            if (_viewModel.MainFilter.IsClearAllData == true)
            {
                _viewModel.Entity.Clear();
                return;
            }

            var index = dg.SelectedIndex;

            _viewModel.RefreshData();

            dg.SelectedIndex = index;
        }

        public virtual void Save()
        {
            if (_viewModel == null)
                return;

            CommitEdit();

            //enforce data grid show Add new row placeholder
            dg.RaiseEvent(
                new KeyEventArgs(
                    InputManager.Current.PrimaryKeyboardDevice
                    , PresentationSource.FromDependencyObject(dg)
                    , Environment.ProcessorCount
                    , Key.Return)
                {
                    RoutedEvent = UIElement.KeyDownEvent
                });

            try
            {
                _viewModel.Save();
            }
            catch (Exception ex)
            {
                LogManager.Log(event_type.et_Internal, severity_type.st_error, "Save_Exception", ex);
            }

            if (AfterSave != null)
            {
                AfterSave(this, null);
            }
        }

        public virtual void Cancel()
        {
            RefreshView();

            if (AfterCancel != null)
            {
                AfterCancel(this, null);
            }
        }

        public void SetMainFilter(string key, object value, bool setFalse = false)
        {
            _viewModel.MainFilter.SetFilter(key, value, setFalse);
        }

        public void SetReferenceFilter<T1>(string columnName, Expression<Func<T1, bool>> filter) where T1 : class
        {
            _viewModel.SetReferenceFilter(columnName, filter);
        }

        public void SetDefaultValue(string columnName, object value)
        {
            _viewModel.SetDefaultValue(columnName, value);
        }

        protected virtual void bmMenu_Click(object sender, RoutedEventArgs e)
        {
            var button = e.OriginalSource as Button;
            if (button.Name == "btnSave")
            {
                Save();
            }
            else if (button.Name == "btnCancel")
            {
                Cancel();
            }
        }
    }
}
