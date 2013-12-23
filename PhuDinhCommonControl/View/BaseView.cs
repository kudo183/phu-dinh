using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CustomControl;
using PhuDinhCommon;
using PhuDinhData.ViewModel;

namespace PhuDinhCommonControl
{
    public class BaseView<T> : UserControl where T : class
    {
        protected BaseViewModel<T> _viewModel;
        public BaseViewModel<T> ViewModel { get { return _viewModel; } }

        public event EventHandler AfterSave;
        public event EventHandler AfterCancel;

        public DataGridExt dg { private get; set; }

        protected BaseView()
        {
            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
        }

        protected virtual void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (_viewModel == null)
                return;

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
                    case Key.S:
                        Save();
                        break;
                    case Key.X:
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

        protected void bmMenu_Click(object sender, RoutedEventArgs e)
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
