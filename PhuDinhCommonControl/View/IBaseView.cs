using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomControl;
using PhuDinhData;

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
