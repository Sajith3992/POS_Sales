using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Sales
{
    class DBConnect
    {
        private string con;
        public string myConnection() 
        {
            con = @"Data Source=DESKTOP-33KS9JQ;Initial Catalog=POS_Sales;Integrated Security=True";
            return con;
        }
    }
}
