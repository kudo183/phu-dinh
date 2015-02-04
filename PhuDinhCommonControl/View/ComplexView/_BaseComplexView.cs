using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using PhuDinhData;

namespace PhuDinhCommonControl
{
    public abstract class _BaseComplexView : UserControl
    {
        private readonly List<IBaseView> _views = new List<IBaseView>();

        protected _BaseComplexView()
        {
            Loaded += _BaseComplexView_Loaded;
            Unloaded += _BaseComplexView_Unloaded;
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            var index = GetFocusedViewIndexFromKey(e);
            if (index >= 0 && index < _views.Count)
                FocusView(_views[index]);
            else
            {
                base.OnPreviewKeyDown(e);
            }
        }

        private int GetFocusedViewIndexFromKey(KeyEventArgs e)
        {
            var result = -1;

            if ((Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.D1)
                || e.Key == Key.F6)
            {
                result = 0;
            }
            else if ((Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.D2)
                || e.Key == Key.F7)
            {
                result = 1;
            }
            else if ((Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.D3)
                || e.Key == Key.F8)
            {
                result = 2;
            }

            return result;
        }

        public void AddView(IBaseView view)
        {
            _views.Add(view);
        }

        void _BaseComplexView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            for (int i = _views.Count - 1; i >= 0; i--)
            {
                _views[i].dg.SelectionChanged += dg_SelectionChanged;
            }

            foreach (var view in _views)
            {
                view.MoveFocus += view_MoveFocus;
                view.AfterSave += view_AfterSave;
                view.AfterCancel += view_AfterCancel;
            }

            OnLoaded();
        }

        void _BaseComplexView_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            for (int i = _views.Count - 1; i >= 0; i--)
            {
                _views[i].dg.SelectionChanged -= dg_SelectionChanged;
            }

            foreach (var view in _views)
            {
                view.MoveFocus -= view_MoveFocus;
                view.AfterSave -= view_AfterSave;
                view.AfterCancel -= view_AfterCancel;
            }
        }

        void dg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender != e.OriginalSource)
            {
                return;
            }

            OnSelectionChanged(sender);
        }

        void view_AfterCancel(object sender, EventArgs e)
        {
            OnAfterCancel(sender as IBaseView);
        }

        void view_AfterSave(object sender, EventArgs e)
        {
            OnAfterSave(sender as IBaseView);
        }

        void view_MoveFocus(object sender, EventArgs e)
        {
            var index = _views.IndexOf(sender as IBaseView);
            FocusView(_views[(index + 1) % _views.Count]);
        }

        protected virtual void OnLoaded()
        {

        }

        protected virtual void OnSelectionChanged(object view)
        {

        }

        protected virtual void OnAfterSave(IBaseView view)
        {

        }

        protected virtual void OnAfterCancel(IBaseView view)
        {

        }

        protected virtual void FocusView(IBaseView view)
        {
            view.dg.FocusCell(view.dg.Items.Count - 1, view.dg.FindFirstEditableColumnIndex(0, view.dg));
        }
    }
}
