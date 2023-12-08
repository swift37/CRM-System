using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CRM.Infrastructure.Commands
{
    public class DialogResultCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool? DialogResult { get; set; }

        public bool CanExecute(object? param) => App.CurrentWindow != null;

        public void Execute(object? param)
        {
            if (!CanExecute(param)) return;
            
            var window = App.CurrentWindow;
            if (window is null) return;

            var dialogResult = DialogResult;
            if (param != null)
                dialogResult = Convert.ToBoolean(param);

            window.DialogResult = dialogResult;
            window.Close();
        }
    }
}
