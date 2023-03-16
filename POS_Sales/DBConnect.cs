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
            con = @"Data Source=DESKTOP-0NH2VE5\SQLEXPRESS;Initial Catalog=DBPOS_Sale;Integrated Security=True;Pooling=False";
            return con;
        }
    }
}
