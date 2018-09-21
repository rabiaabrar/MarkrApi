using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarkrApi.Models
{
    [XmlRoot(ElementName = "mcq-test-results")]
    public class MCQTestResults
    {
        [XmlElement(ElementName = "mcq-test-result")]
        public List<MCQTestResult> MCQTestResult { get; set; }
    }

    [XmlRoot(ElementName = "mcq-test-result")]
    public class MCQTestResult
    {
        [XmlElement(ElementName = "first-name")]
        public string FirstName { get; set; }
        [XmlElement(ElementName = "last-name")]
        public string LastName { get; set; }
        [XmlElement(ElementName = "student-number")]
        public int StudentNumber { get; set; }
        [XmlElement(ElementName = "test-id")]
        public int TestId { get; set; }
        [XmlElement(ElementName = "answer")]
        public List<Answer> Answer { get; set; }
        [XmlElement(ElementName = "summary-marks")]
        public SummaryMarks SummaryMarks { get; set; }
        [XmlAttribute(AttributeName = "scanned-on")]
        public DateTime ScannedOn { get; set; }
    }

    [XmlRoot(ElementName = "summary-marks")]
    public class SummaryMarks
    {
        [XmlAttribute(AttributeName = "available")]
        public int Available { get; set; }
        [XmlAttribute(AttributeName = "obtained")]
        public int Obtained { get; set; }
    }

    [XmlRoot(ElementName = "answer")]
    public class Answer
    {
        [XmlAttribute(AttributeName = "question")]
        public string Question { get; set; }
        [XmlAttribute(AttributeName = "marks-available")]
        public string MarksAvailable { get; set; }
        [XmlAttribute(AttributeName = "marks-awarded")]
        public string MarksAwarded { get; set; }
        [XmlText]
        public string Text { get; set; }
    }
}