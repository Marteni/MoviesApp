using System;
using System.Collections.Generic;
using System.Text;

namespace MoviesApp.APP.Services.MessageDialog
{
    public interface IMessageDialogService
    {
        MessageDialogResult Show(
            string title,
            string caption,
            MessageDialogButtonConfiguration buttonConfiguration,
            MessageDialogResult defaultResult);
    }
}
