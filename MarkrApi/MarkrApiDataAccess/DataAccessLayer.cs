using System;
using System.Collections.Generic;
using System.Linq;

namespace MarkrApiDataAccess
{
    public abstract class DataAccessLayer
    {
        public static List<TestResult> SelectTestResultsByTestId(int testId)
        {
            using (var entities = new MarkrApiEntities())
            {
                return entities.TestResults.Where(r => r.TestId == testId).ToList();
            }
        }

        public static TestResult SelectTestResult(int testId, int studentNumber)
        {
            using (var entities = new MarkrApiEntities())
            {
                return entities.TestResults.Where(r => r.TestId == testId && r.StudentNumber == studentNumber).FirstOrDefault();
            }
        }

        public static int DeleteTestResult(int testId, int studentNumber)
        {
            using (var entities = new MarkrApiEntities())
            {
                var testResult = entities.TestResults.Where(r => r.TestId == testId && r.StudentNumber == studentNumber).First();
                entities.TestResults.Remove(testResult);
                return entities.SaveChanges();
            }
        }

        public static int InsertTestResult(TestResult testResult)
        {
            using (var entities = new MarkrApiEntities())
            {
                entities.TestResults.Add(testResult);
                return entities.SaveChanges();
            }
        }

        public static int InsertTestResults(List<TestResult> testResults)
        {
            using (var entities = new MarkrApiEntities())
            {
                entities.TestResults.AddRange(testResults);
                return entities.SaveChanges();
            }
        }

        public static int DeleteTestResults(int testId)
        {
            using (var entities = new MarkrApiEntities())
            {
                var testResult = entities.TestResults.Where(r => r.TestId == testId).ToList();
                entities.TestResults.RemoveRange(testResult);
                return entities.SaveChanges();
            }
        }
    }
}
