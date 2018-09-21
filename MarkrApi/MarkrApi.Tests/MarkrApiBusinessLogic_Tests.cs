using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MarkrApi.Code;
using System.Xml.Serialization;
using MarkrApi.Models;
using System.IO;

namespace MarkrApi.Tests
{
    [TestClass]
    public class MarkrApiBusinessLogic_Tests
    {
        //The TestId 1 is reserved for test data. We should have appropriate error handling in the application to ensure that
        int testIdForUnitTests = 1;

        [TestMethod]
        public void InsertTestResults_TwoDistinctStudents_TwoRowsAffected()
        {
            MarkrApiBusinessLogic.DeleteTestResults(testIdForUnitTests);

            var testResults = Deserialize("TestInputs/InsertTestResults_TwoDistinctStudents_TwoRows.xml");

            var importResults = MarkrApiBusinessLogic.ImportTestResults(testResults);

            Assert.AreEqual(importResults.RowsParsed, 2);
            Assert.AreEqual(importResults.RowsAccepted, 2);
        }

        [TestMethod]
        public void InsertTestResults_TwoDuplicateStudents_CorrectOneInserted()
        {
            //The TestId 1 is reserved for test data. We should have appropriate error handling in the application to ensure that
            MarkrApiBusinessLogic.DeleteTestResults(testIdForUnitTests);

            var testResults = Deserialize("TestInputs/InsertTestResults_TwoDuplicateStudents_CorrectOneInserted.xml");

            var importResults = MarkrApiBusinessLogic.ImportTestResults(testResults);

            var testResultsInDB = MarkrApiBusinessLogic.GetTestResults(testIdForUnitTests);

            Assert.AreEqual(importResults.RowsParsed, 2);
            Assert.AreEqual(importResults.RowsAccepted, 2);
            Assert.AreEqual(testResultsInDB.Count, 1);
            Assert.IsTrue(testResultsInDB.Exists(r => r.MarksObtained == 13));
        }

        private MCQTestResults Deserialize(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(MCQTestResults));
            MCQTestResults testResults;

            using (var reader = new StreamReader(filePath))
            {
                testResults = (MCQTestResults)serializer.Deserialize(reader);
                reader.Close();
            }

            return testResults;
        }
    }
}
