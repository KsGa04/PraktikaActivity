using Microsoft.VisualStudio.TestTools.UnitTesting;
using PraktikaActivity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace UnitTestProject
{
    /// <summary>
    /// Сводное описание для GenerationIdTest
    /// </summary>
    [TestClass]
    public class GenerationIdTest
    {
        [TestMethod]
        public void TestGenerateUniqueId()
        {
            int newid = RgistrationOfJuryModerator.GenerateUniqueId();
            using (ActivityEntities db = new ActivityEntities())
            {
                bool isExist = db.Users.Where(x => x.Id == newid).Any();
                Assert.IsFalse(isExist, "Данный isNumber отсутсвует в бд");
            }
        }
    }
}
