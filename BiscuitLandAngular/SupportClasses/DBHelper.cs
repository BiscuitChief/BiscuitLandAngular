using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiscuitLandAngular
{
    public class DBHelper
    {
        private string _connString;

        public DBHelper(string connString)
        {
            _connString = connString;
        }

        public string GetDBConn()
        {
            return _connString;
        }
    }
}
