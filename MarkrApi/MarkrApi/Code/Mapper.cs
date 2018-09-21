using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MarkrApi.Models;
using MarkrApiDataAccess;

namespace MarkrApi.Code
{
    public abstract class Mapper
    {
        public static TestResult Map(MCQTestResult mcqTestResult)
        {
            return new TestResult
            {
                TestId = mcqTestResult.TestId,
                StudentNumber = mcqTestResult.StudentNumber,
                FirstName = mcqTestResult.FirstName,
                LastName = mcqTestResult.LastName,
                MarksAvailable = mcqTestResult.SummaryMarks.Available,
                MarksObtained = mcqTestResult.SummaryMarks.Obtained,
                ScannedOn = mcqTestResult.ScannedOn
            };
        }
    }
}