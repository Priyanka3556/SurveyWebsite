using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.Enums;
using System.Data.Entity.Core;
using System.Security.Cryptography;
using System.Text;

namespace DataAccessLayer
{
    public class SurveyManager
    {
        private readonly SurveyRepository repo = new SurveyRepository();
        public Survey GetActiveSurvey()
        {
            return repo.GetActiveSurvey().Where(p=>p.ActiveFrom<=DateTime.Now && p.ActiveUntil >DateTime.Now).FirstOrDefault();
        }
        public List<SurveyQuestions> GetQuestions(int surveyId)
        {
            return repo.GetSurveyQuestions().Where(p => p.SurveyId == surveyId).ToList();
        }
        public List<SurveyAnswer> GetAnswers(List<int> answerId)
        {
            return repo.GetSurveyAnswers().Where(p => answerId.Contains(p.AnswerId)).ToList();
        }
        public List<SurveyResponse> GetSurveyData(string uniqueId)
        {
            return repo.GetSurveyResponse().Where(p => p.UniqueResponseId == uniqueId).ToList();
        }
        public bool AddData(List<SurveyResponse> data)
        {
            try
            {
                //foreach(var d in data)
                //{
                //    d.Survey = GetActiveSurvey();
                //    d.SurveyQuestions = GetQuestions(d.SurveyId).Where(r => r.QuestionId == d.QuestionId).SingleOrDefault();
                //}
                using (var repo = new SurveyRepository())
                {
                    repo.AddSurveyResponse(data);
                }
            }
            catch (Exception ex)
            {
                //Log Exception we can use log4net
                return false;
            }
            return true;
        }
    }
   
}
