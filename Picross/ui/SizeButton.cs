using Picross.ui.dialog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Picross.ui
{
    class SizeButton : Button
    {
        public SizeButton() : base(10, 10, "サイズ変更")
        {

        }

        public override void onClick()
        {
            Dialog dialog = Controll.GetDialog();
            dialog.Show();
        }
    }
}
