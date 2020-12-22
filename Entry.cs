using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Entry
    {
        public bool SqlConnection { get { return SQL.Sql_Functions.Connected; } }

        /// <summary>
        /// The Main Access point for the entire solution, MUST BE INIT'd FOR SQL TO WORK
        /// </summary>
        public Entry(string connString)
        {
            SQL.Sql_Functions.SetConn(connString);
        }
    }
}
