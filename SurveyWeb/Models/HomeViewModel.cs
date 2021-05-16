using System.Collections.Generic;
using DataAccessLayer;

namespace SurveyWeb.Models
{
    public class HomeViewModel
    {
        public Survey Survey { get; set; }
        public List<SurveyQuestions> SurveyQuestions { get; set; }
    }
}
