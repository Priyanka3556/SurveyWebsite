using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer
{
    public class Survey
    {
        [Key]
        public int SurveyId { get; set; }
        public string Name { get; set; }
        public DateTime ActiveFrom { get; set; }
        public DateTime ActiveUntil { get; set; }
        public virtual List<SurveyQuestions> SurveyQuestions { get; set; }
    }
}
