using System;
using DataLayer.SQL;
using DataLayer.ERRORCHECK;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ErrorCheck_UnitTesting
{
    [TestClass]
    public class ErrorCheck_Tests
    {
        [TestMethod]
        public void initilized()
        {
            throw new NotImplementedException();
        }
        [TestMethod]
        public void BuildTable()
        {
            Sql_Functions.SetConn(ConfigurationManager.AppSettings.Get("Connection"));

            ErrorLog.SetOutput();
            ErrorLog.Output("BuildTable TestMethod", ErrorLog.ErrorLevel.Debug);

            Assert.IsTrue(ErrorLog.TableBuilt);
        }

    }
}
