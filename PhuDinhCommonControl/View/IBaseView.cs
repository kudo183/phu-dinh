using System;
using CustomControl;

namespace PhuDinhCommonControl
{
    public interface IBaseView
    {
        event EventHandler AfterSave;
        event EventHandler AfterCancel;

        event EventHandler MoveFocus;

        DataGridExt dg { get; set; }
    }
}
