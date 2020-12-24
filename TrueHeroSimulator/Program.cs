using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrueHeroSimulator
{
    class Program
    {
        [STAThread]
        [HandleProcessCorruptedStateExceptions]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            try
            {
                Application.Run(TrueHeroSimulatorUI.Instance);
            }
            catch { }
        }
    }
}
