using Picross.ui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Picross
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            PicrossUI picross = new PicrossUI();
            picross.Run();
        }
    }
}
