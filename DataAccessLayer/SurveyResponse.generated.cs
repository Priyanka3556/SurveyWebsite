using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class SurveyResponse
    {
        [Key]
        public int DataId { get; set; }
        public int SurveyId { get; set; }
        public int QuestionId { get; set; }
        public string AnswerId { get; set; }
        public string Answer { get; set; }
        public string UniqueResponseId { get; set; }
        public virtual Survey Survey { get; set; }
        public virtual SurveyQuestions SurveyQuestions { get; set; }
    }
}
