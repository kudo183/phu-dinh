using System;
using System.Linq.Expressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Common;
using CustomControl;
using PhuDinhCommon;
using PhuDinhData.ViewModel;

namespace PhuDinhCommonControl
{
    public abstract class BaseView<T> : UserControl, IBaseView where T : BindableObject
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
            if (IsSaveShortcutKey(e.Key))
            {
                Save();
            }
            else if (IsSaveAndMoveFocusShortcutKey(e.Key))
            {
                Save();

                if (MoveFocus != null)
                {
                    MoveFocus(this, null);
                }
            }
            else if (IsCancelShortcutKey(e.Key))
            {
                Cancel();
            }
        }

        #region check shortcut key helper
        
        private bool IsSaveShortcutKey(Key key)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control && key == Key.Q)
                return true;

            if (key == Key.F3)
                return true;

            return false;
        }

        private bool IsSaveAndMoveFocusShortcutKey(Key key)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control && key == Key.S)
                return true;

            if (key == Key.F4)
                return true;

            return false;
        }

        private bool IsCancelShortcutKey(Key key)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control && key == Key.Z)
                return true;

            if (key == Key.F5)
                return true;

            return false;
        }

        #endregion

        public virtual void CommitEdit()
        {
            dg.CommitEdit(DataGridEditingUnit.Row, true);
        }

        public virtual void RefreshView()
        {
            if (_viewModel == null)
                return;

            var index = dg.SelectedIndex;

            dg.SkippedSelectionChangedEvent = true;
            _viewModel.RefreshData();
            dg.SkippedSelectionChangedEvent = false;

            dg.SelectedIndex = index;
        }

        public virtual void Save()
        {
            if (_viewModel == null)
                return;

            CommitEdit();

            try
            {
                _viewModel.Save();
                RefreshView();
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
