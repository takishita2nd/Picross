using Picross.ui.dialog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Picross.ui
{
    class Controll
    {
        private static Dialog _dialog = null;

        public static Dialog GetDialog()
        {
            if(_dialog == null)
            {
                _dialog = new Dialog();
                _dialog.SetEngine();
            }
            return _dialog;
        }
    }
}
