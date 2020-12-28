using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Entry
    {

        /// <summary>
        /// The Main Access point for the entire solution, MUST BE INIT'd FOR SQL TO WORK
        /// </summary>
        public Entry(string connString)
        {
            SQL.Sql_Functions.SetConn(connString);
        }

        /// <summary>
        /// Returns the Database connection status
        /// </summary>
        /// <returns></returns>
        public bool GetConnection()
        {
            return SQL.Sql_Functions.Connected;
        }
    }
}
