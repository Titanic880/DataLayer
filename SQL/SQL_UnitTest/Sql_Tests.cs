using System;
using DataLayer.SQL;
using System.Configuration;
using System.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;

/// <summary>
/// If the Connected Tests Fail, Everything else has a high Chance of failing
/// </summary>
namespace SQL_UnitTest
{
    /// <summary>
    /// Unit testing for the SQL Sub Folder
    /// </summary>
    [TestClass]
    public class Sql_Tests
    {
        #region Connection/Tbl check
        [TestMethod]
        public void Connected_STRING()
        {
            Assert.IsTrue(Sql_Functions.SetConn(ConfigurationManager.AppSettings.Get("Connection")));
        }

        [TestMethod]
        public void Connected_SQLCONN()
        {
            //This Breaks the Sql Connection from previous tests
            Sql_Functions.SetConn("");
            Assert.IsFalse(Sql_Functions.Connected);

            SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings.Get("Connection"));
            Assert.IsTrue(Sql_Functions.SetConn(conn));
        }
        [TestMethod]
        public void TableCheck()
        {
            Sql_Functions.SetConn(ConfigurationManager.AppSettings.Get("Connection"));

            string TableName = "TestCheck";
            Assert.IsTrue(Sql_Functions.Check_Table(TableName));
            Assert.IsTrue(Sql_Functions.Connected);
        }

        #endregion Connection/Tbl check

        #region Execution Methods
        [TestMethod]
        public void NonQuery_String()
        {
            Sql_Functions.SetConn(ConfigurationManager.AppSettings.Get("Connection"));

            string TableName = "NonQueryTest";

            string test = $"Create Table {TableName}([id] int);";
            if (Sql_Functions.Check_Table(TableName))
                test = $"Drop Table {TableName}";

            Assert.AreNotEqual("--", Sql_Functions.RunNonQuery(test).ToString().Substring(0, 2));
            Assert.IsTrue(Sql_Functions.Connected);
        }
        [TestMethod]
        public void NonQuery_SqlCommand()
        {
            Sql_Functions.SetConn(ConfigurationManager.AppSettings.Get("Connection"));

            string TableName = "NonQueryTest";

            string test = $"Create Table {TableName}([id] int);";
            if (Sql_Functions.Check_Table(TableName))
                test = $"Drop Table {TableName}";

            SqlCommand cmd = new SqlCommand(test);
            Assert.AreNotEqual("--", Sql_Functions.RunNonQuery(cmd).ToString().Substring(0, 2));
            Assert.IsTrue(Sql_Functions.Connected);
        }
        [TestMethod]
        public void NonQuery_Fail()
        {
            Sql_Functions.SetConn(ConfigurationManager.AppSettings.Get("Connection"));

            //Checks the command Version
            SqlCommand cmd = new SqlCommand("Fail");
            Assert.IsFalse(Sql_Functions.RunNonQuery(cmd));
            //Checks the String version
            Assert.IsFalse(Sql_Functions.RunNonQuery("Fail"));
        }

        [TestMethod]
        public void Scalar_String()
        {
            Sql_Functions.SetConn(ConfigurationManager.AppSettings.Get("Connection"));

            string TableName = "ScalarTest";

            string test = $"Create Table {TableName}([id] int);";
            if (Sql_Functions.Check_Table(TableName))
                test = $"Drop Table {TableName}";

            Assert.AreNotEqual("--",Sql_Functions.RunScalar(test));
            Assert.IsTrue(Sql_Functions.Connected);
        }
        [TestMethod]
        public void Scalar_SqlCommand()
        {
            Sql_Functions.SetConn(ConfigurationManager.AppSettings.Get("Connection"));
            string TableName = "ScalarTest";

            string test = $"Create Table {TableName}([id] int);";
            if (Sql_Functions.Check_Table(TableName))
                test = $"Drop Table {TableName}";

            SqlCommand cmd = new SqlCommand(test);
            Assert.AreNotEqual("--", Sql_Functions.RunScalar(cmd));
            Assert.IsTrue(Sql_Functions.Connected);
        }
        [TestMethod]
        public void Scalar_Fail()
        {
            Sql_Functions.SetConn(ConfigurationManager.AppSettings.Get("Connection"));

            //Checks the command Version
            SqlCommand cmd = new SqlCommand("Fail");
            Assert.AreEqual("--",Sql_Functions.RunScalar(cmd).ToString().Substring(0,2));
            //Checks the String version
            Assert.AreEqual("--", Sql_Functions.RunScalar("fail").ToString().Substring(0, 2));
        }

        [TestMethod]
        public void Query()
        {
            Sql_Functions.SetConn(ConfigurationManager.AppSettings.Get("Connection"));

            string TableName = "QueryTest";
            string[] Create_Query = {$"Create Table {TableName}(id int);" ,$"INSERT INTO {TableName} Values(1);"};
            foreach (string a in Create_Query)
                Sql_Functions.RunNonQuery(a);

            string Query = $"Select * from {TableName}";

            Assert.IsNotNull(Sql_Functions.Query(Query));
            Assert.IsTrue(Sql_Functions.Connected);
        }

        #endregion Execution Methods
    }
}
