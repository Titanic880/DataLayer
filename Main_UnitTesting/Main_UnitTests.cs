using System;
using DataLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;

namespace Main_UnitTesting
{
    /// <summary>
    /// Unit Tests for the Entry Functions/Methods
    /// </summary>
    [TestClass]
    public class Entry_UnitTests
    {
        [TestMethod]
        public void Test_Connection()
        {
            Entry ent = new Entry(ConfigurationManager.AppSettings.Get("Connection"));

            Assert.IsTrue(ent.GetConnection());
        }
    }
}
