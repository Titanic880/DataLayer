using System;
using System.Data;
using System.Data.SqlClient;


namespace DataLayer.SQL
{
    public static class Sql_Functions
    {
        /// <summary>
        /// Primary Sql Connection - must be set prior to use
        /// </summary>
        internal static SqlConnection sql { get; private set; }
        public static bool Connected { get; private set; }

        #region Startup
        /// <summary>
        /// Used to set the Connection
        /// </summary>
        /// <param name="Conn"></param>
        /// <returns></returns>
        public static bool SetConn(string ConnString)
        {
            sql = new SqlConnection(ConnString);
            Connected = Test_Conn();
            return Connected;
        }
        public static bool SetConn(SqlConnection Conn)
        {
            sql = Conn;
            Connected = Test_Conn();
            return Connected;
        }

        /// <summary>
        /// Tests the Sql Connection
        /// </summary>
        public static bool Test_Conn()
        {
            string Table_Loggging = "Create Table Test_conn (" +
                 "ID int not null Primary key Identity(0,1)," +
                 "LogLevel int not null," +
                 "Error_Desc varchar(50)," +
                 "Time_Of_Error DateTime not null" +
                 ");";
            string check_tbl = "Select * from Test_conn";

            bool test = true;

            try
            {
                sql.Open();

                //Tests to see if the table exists, if it doesn't the runs the Table create
                try
                {
                    SqlCommand comm = new SqlCommand(check_tbl, sql);
                    comm.ExecuteScalar();
                }
                catch
                {
                    test = false;
                }
                if (!test)
                {
                    SqlCommand cmd = new SqlCommand(Table_Loggging, sql);
                    cmd.ExecuteScalar();
                }
                SqlCommand drop = new SqlCommand("Drop Table Test_conn;", sql);
                drop.ExecuteScalar();
                test = true;
            }
            catch
            {
                test = false;
            }
            finally
            {
                if (sql.State != ConnectionState.Closed)
                    sql.Close();
            }

            return test;
        }

        /// <summary>
        /// Checks to see if the table exists in the database
        /// </summary>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public static bool Check_Table(string TableName)
        {
            string Query = "Select * From "+TableName;

            try
            {
                sql.Open();
                SqlCommand cmd = new SqlCommand(Query, sql);
                cmd.ExecuteScalar();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
            finally
            {
                if (sql.State != ConnectionState.Closed)
                    sql.Close();
            }
        }
        #endregion Startup

        #region Execution
        /// <summary>
        /// Runs the Scalar method and returns the result, or the error log : check for error by substring(0,2)
        /// </summary>
        /// <param name="Query"></param>
        /// <returns></returns>
        public static object RunScalar(string Query)
        {
            try
            {
                sql.Open();
                SqlCommand cmd = new SqlCommand(Query, sql);
                object o = cmd.ExecuteScalar();
                return o;
            }
            catch (Exception ex)
            {
                return "--" + ex.Message;
            }
            finally
            {
                if (sql.State != ConnectionState.Closed)
                    sql.Close();
            }
        }
        /// <summary>
        /// Takes command and runs it
        /// </summary>
        /// <param name="Query"></param>
        /// <returns></returns>
        public static object RunScalar(SqlCommand Query)
        {
            try
            {
                sql.Open();
                Query.Connection = sql;
                return Query.ExecuteScalar();
            }
            catch (Exception ex)
            {
                return "--" + ex.Message;
            }
            finally
            {
                if (sql.State != ConnectionState.Closed)
                    sql.Close();
            }
        }

        /// <summary>
        /// Fills a DataTable and returns it
        /// </summary>
        /// <param name="Query"></param>
        /// <returns></returns>
        public static DataTable Query(string Query)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(Query, sql);
            adapter.Fill(dt);
            return dt;
        }

        /// <summary>
        /// Executes a Query without a return
        /// </summary>
        /// <param name="Query"></param>
        public static bool RunNonQuery(string Query)
        {
            try
            {
                sql.Open();
                SqlCommand cmd = new SqlCommand(Query, sql);
                cmd.ExecuteNonQuery().ToString();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                if (sql.State != ConnectionState.Closed)
                    sql.Close();
            }
        }
        /// <summary>
        /// Executes a Query without a return
        /// </summary>
        /// <param name="Query"></param>s
        public static bool RunNonQuery(SqlCommand Query)
        {
            try
            {
                sql.Open();
                Query.Connection = sql;
                Query.ExecuteNonQuery().ToString();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                if (sql.State != ConnectionState.Closed)
                    sql.Close();
            }
        }
        #endregion Execution
    }
}

