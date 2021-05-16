using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class SurveyQuestions
    {
        [Key]
        public int QuestionId { get; set; }
        public int SurveyId { get; set; }
        public string Question { get; set; }
        public int QuestionTypeId { get; set; }
        public virtual Survey Survey { get; set; }
        public virtual List<SurveyAnswer> SurveyAnswerOptions { get; set; }
    }
}
