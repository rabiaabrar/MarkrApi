using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MarkrApi.Models;
using MarkrApiDataAccess;

namespace MarkrApi.Code
{
    public abstract class MarkrApiBusinessLogic
    {
        public static Aggregates GetAggregatesForTest(int testId)
        {
            var dbTestResults = DataAccessLayer.SelectTestResultsByTestId(testId);

            var aggregates = new Aggregates
            {
                TestId = testId,
                Count = dbTestResults.Count,
                Mean = 0,
                P25 = 0,
                P50 = 0,
                P75 = 0
            };

            if (dbTestResults.Count > 0)
            {
                // I use a very simple strategy to get the elements at specific percentile markers
                // From 0 to total count, get numbers that are quarter, half and three quarters of the way. Take their Math.Floor and use that as an index
                // The test scores at those indexes can then be divided by marksAvailable to be expressed as percetages
                var marksObtained = dbTestResults.Select(r => r.MarksObtained).ToArray();
                Array.Sort(marksObtained);

                int i25 = (int)(dbTestResults.Count * (decimal)0.25),
                    i50 = (int)(dbTestResults.Count * (decimal)0.50),
                    i75 = (int)(dbTestResults.Count * (decimal)0.75);

                decimal marksAvailable = dbTestResults.First().MarksAvailable;

                aggregates.Mean = Convert.ToDecimal(dbTestResults.Average(r => r.MarksObtained));
                aggregates.P25 = marksObtained[i25] / marksAvailable * 100;
                aggregates.P50 = marksObtained[i50] / marksAvailable * 100;
                aggregates.P75 = marksObtained[i75] / marksAvailable * 100;
            }

            return aggregates;
        }

        public static ImportResults ImportTestResults(MCQTestResults testResults)
        {
            int rowsAccepted = 0;

            foreach (var testResult in testResults.MCQTestResult)
                rowsAccepted += InsertTestResult(testResult);

            return new ImportResults
            {
                RowsParsed = testResults.MCQTestResult.Count,
                RowsAccepted = rowsAccepted
            };
        }

        public static int InsertTestResult(MCQTestResult testResult)
        {
            var existingTestResult = DataAccessLayer.SelectTestResult(testResult.TestId, testResult.StudentNumber);

            // If this student's test results for this test don't already exist in db id then insert them
            if (existingTestResult == null)
                return DataAccessLayer.InsertTestResult(Mapper.Map(testResult));

            // If this student's test results for this test already exist in db but existing MarksObtained are same or higher, do nothing
            else if (existingTestResult.MarksObtained >= testResult.SummaryMarks.Obtained)
                return 0;

            // If this student's test results for this test already exist in db and existing MarksObtained are less, delete that record and insert new one
            else if (existingTestResult.MarksObtained < testResult.SummaryMarks.Obtained)
            {
                DataAccessLayer.DeleteTestResult(existingTestResult.TestId, existingTestResult.StudentNumber);
                return DataAccessLayer.InsertTestResult(Mapper.Map(testResult));
            }

            // This code should really never run
            return 0;
        }

        public static int _InsertTestResults(MCQTestResults testResults)
        {
            //Convert everything to a List of our Data Model TestResult because that guy is simpler to work with

            var dbTestResults = testResults.MCQTestResult.Select(r => Mapper.Map(r)).ToList();

            var duplicateStudentNumbers = dbTestResults.GroupBy(r => r.StudentNumber)
                        .Where(group => group.Count() > 1)
                        .Select(group => group.Key);

            foreach (int studentNumber in duplicateStudentNumbers)
            {
                // Get max MarksObtained for this student
                var maxMarks = dbTestResults.Where(r => r.StudentNumber == studentNumber).Max(r => r.MarksObtained);

                //Hold on to that one TestResult which has max MarksObtained
                var testResultWithMaxMarks = dbTestResults.First(r => r.StudentNumber == studentNumber && r.MarksObtained == maxMarks);

                //Delete all other TestResult for this student
                dbTestResults.RemoveAll(r => r.StudentNumber == studentNumber);

                //Add the max TestResult back to the list
                dbTestResults.Add(testResultWithMaxMarks);
            }

            return DataAccessLayer.InsertTestResults(dbTestResults);
        }

        public static List<TestResult> GetTestResults(int testId)
        {
            return DataAccessLayer.SelectTestResultsByTestId(testId);
        }

        public static int DeleteTestResults(int testId)
        {
            return DataAccessLayer.DeleteTestResults(testId);
        }
    }
}