using System;
using DataLayer.SQL;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;

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

        [TestMethod]
        public void NonQuery_SqlCommand()
        {
            Sql_Functions.SetConn(ConfigurationManager.AppSettings.Get("Connection"));

            string TableName = "NonQueryTest";

            string test = $"Create Table {TableName}([id] int);";
            if (Sql_Functions.Check_Table(TableName))
                test = $"Drop Table {TableName}";    

            SqlCommand comm = new SqlCommand(test);
            object result = Sql_Functions.RunNonQuery(comm);
            string res = result.ToString();
            Assert.AreNotEqual("--", res.Substring(0,2));
            Assert.IsTrue(Sql_Functions.Connected);
        }
    }
}
