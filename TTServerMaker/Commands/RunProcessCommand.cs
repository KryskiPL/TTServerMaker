using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TTServerMaker.Commands
{
    class RunProcessCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Process.Start(parameter.ToString());
        }

        public event EventHandler CanExecuteChanged { add { } remove { } }
    }
}
