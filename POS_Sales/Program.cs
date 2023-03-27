using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_Sales
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            /* Application.Run(new BrandModule());*/
            /* Application.Run(new MainForm1());*/
            /* Application.Run(new BrandModule());*/
            /*Application.Run(new Brand());*/
            /*Application.Run(new Cashier());*/
            Application.Run(new StockIn());
           
        }
    }
}
