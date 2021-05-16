using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Core;
using DataAccessLayer;
using DataAccessLayer.Enums;
using SurveyWeb.Models;

namespace SurveyWeb.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private SurveyManager manager = new SurveyManager();
        // GET: /Home/Default
        [AllowAnonymous]
        public ActionResult Default(string returnUrl)
        {
            var model = (HomeViewModel)TempData["model"];
            //if (model != null && model.Survey.Any())
            //    return View(model);
            ViewBag.ReturnUrl = returnUrl;
            HomeViewModel viewModel = new HomeViewModel();
            viewModel.Survey = manager.GetActiveSurvey();
            viewModel.SurveyQuestions = manager.GetQuestions(viewModel.Survey.SurveyId);
            return View(viewModel);
        }
        [AllowAnonymous]
        [HttpPost]
        [ActionName("SurveyResponse")]
        public void SurveyResponse(FormCollection collection)
        {
            Helper helper = new Helper();
            Guid g = Guid.NewGuid();
            List<SurveyResponse> data = new List<SurveyResponse>();
            int surveyId = Convert.ToInt32(collection["FormSurveyId"]);
            foreach(var question in  manager.GetQuestions(surveyId))
            {
                SurveyResponse response = new SurveyResponse();
                response.SurveyId = surveyId;
                response.QuestionId = question.QuestionId;
                if(question.QuestionTypeId == (int)QuestionType.MultipleChoice || question.QuestionTypeId == (int)QuestionType.SingleAnswer)
                    response.AnswerId = collection[Convert.ToString(question.QuestionId)];
                else
                    response.Answer = collection[Convert.ToString(question.QuestionId)];
                response.UniqueResponseId =  g.ToString();
                data.Add(response);
            }
            if(manager.AddData(data))
                helper.AddDataToFile(g.ToString());
        }
    }
}
