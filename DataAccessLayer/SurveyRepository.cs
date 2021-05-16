using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccessLayer
{
    public class SurveyRepository : IDisposable
    {
        protected DbContext DbContext;
        public IQueryable<Survey> GetActiveSurvey( ){
            SurveyContext context = new SurveyContext();
            return context.Survey.AsQueryable();
        }
        public IQueryable<SurveyQuestions> GetSurveyQuestions()
        {
            SurveyContext context = new SurveyContext();
            return context.SurveyQuestions.Include(r=>r.SurveyAnswerOptions).AsQueryable();
        }
        public IQueryable<SurveyAnswer> GetSurveyAnswers()
        {
            SurveyContext context = new SurveyContext();
            return context.SurveyAnswer.AsQueryable();
        }
        public void AddSurveyResponse(List<SurveyResponse> data)
        {
            SurveyContext context = new SurveyContext();
            context.SurveyResponse.AddRange(data);
            context.SaveChanges();
        }
        public IQueryable<SurveyResponse> GetSurveyResponse()
        {
            SurveyContext context = new SurveyContext();
            return context.SurveyResponse.Include(r => r.Survey).Include(r=>r.SurveyQuestions).AsQueryable();
        }
        public void Dispose()
        {
            DbContext?.Dispose();
            DbContext = null;
        }
    }
}
