using System;
using System.IO;
using DataLayer.SQL;
using System.Data.SqlClient;

namespace DataLayer.ERRORCHECK
{
    public static class ErrorLog
    {
        #region Variables
        /// <summary>
        /// Determines the output type of the error
        /// </summary>
        public enum ErrorLevel
        {
            Startup = 69,
            Debug,
            None = 0,
            Minor,
            Intermediate,
            Major,
            Extreme
        }
        /// <summary>
        /// Changing this changes the table for the entire file
        /// </summary>
        private const string TableName = "[Logging]";
        /// <summary>
        /// Changing this changes the File that this points to
        /// </summary>
        private const string file = "ErrorLog.txt";
        /// <summary>
        /// Says if the table has been built
        /// </summary>
        public static bool TableBuilt { get; private set; } = false;
        /// <summary>
        /// True is Database, false is File
        /// </summary>
        public static bool OutputType { get; private set; } = false;
        #endregion Variables

        /// <summary>
        /// Builds the Database tables that are used
        /// </summary>
        /// <returns></returns>
        private static void Build_Table()
        {
            string[] Tables = {
                "Create Table "+TableName+" ("
                +"ID int not null Primary key Identity(0,1),"
                +"LogLevel varchar(12) not null,"
                +"Error_Desc varchar(50),"
                +"Time_Of_Error DateTime not null"
                +");",

                "Create Table [DataSave] ("
               +"ID INT NOT NULL PRIMARY KEY IDENTITY(0, 1),"
               +"FName varchar(40) NOT NULL,"
               +"FDirectory varchar(80) NOT NULL,"
               +");"
            };
            foreach (string tbl in Tables)
                Sql_Functions.RunNonQuery(tbl);

            TableBuilt = Sql_Functions.Check_Table(TableName);
        }
        /// <summary>
        /// Sets the Output type to either database or file
        /// </summary>
        /// <param name="ToDB"></param>
        public static void SetOutput(bool ToDB = true)
        {
            OutputType = ToDB;
        }

        #region Outputs
        /// <summary>
        /// Accepts the error values and passes them to the respective output type
        /// </summary>
        /// <param name="level"></param>
        /// <param name="input"></param>
        public static bool Output(string input, ErrorLevel level = ErrorLevel.None)
        {
            if (OutputType)
            {
                if (!TableBuilt)
                    Build_Table();
                return ToDB(level, input);
            }
            else
            {
                return ToFile(level, input);
            }
        }
        /// <summary>
        /// Outputs error to a File
        /// </summary>
        /// <param name="level"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        private static bool ToFile(ErrorLevel level, string input)
        {
            try
            {
                if (!File.Exists(file))
                {
                    File.Create(file).Close();
                    File.WriteAllText(file, ErrorLevel.Startup + " Error File created" + Environment.NewLine);
                }
                File.AppendAllText(file, level + " " + input + Environment.NewLine);
            }
            catch //(Exception ex)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// Outputs error to a Database table
        /// </summary>
        /// <param name="level"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        private static bool ToDB(ErrorLevel level, string input)
        {
            //Inserts Data to the table 
            SqlCommand cmd = new SqlCommand($"Insert into {TableName} Values " +
                $"(@level,@input,'{DateTime.Now}')");

            cmd.Parameters.AddWithValue("@level", level);
            cmd.Parameters.AddWithValue("@input", input);
            return Sql_Functions.RunNonQuery(cmd);
        }
        #endregion Outputs
    }
}
