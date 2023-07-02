using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveyApp.Core.DTO;
using SurveyApp.Domain.Entities;
using SurveyApp.Domain.RepositoryContracts;
using SurveyApp.Web.UI.Models;
using System.Collections;

namespace SurveyApp.Web.UI.Controllers
{
    public class SurveyController(IUnitOfWork unitOfWork, IMapper mapper) : Controller
    {

        [HttpGet]
        [Route("/{survey_id:int}")]
        public async Task<IActionResult> TakeSurvey(int survey_id)
        {
            var survey = await unitOfWork.Surveys.GetAsync(r => r.Id == survey_id);
            if (survey == null)
                return NotFound();
            var model = new SurveyViewModel()
            {
                Request = new() { SurveyId = survey.Id },
                Survey = mapper.Map<SurveyResponse>(survey)
            };
            return View(model);
        }
        [HttpPost]
        [Route("/{survey_id:int}")]
        public async Task<IActionResult> TakeSurvey(int survey_id, SurveyViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var survey = await unitOfWork.Surveys.GetAsync(r => r.Id == survey_id);
                model.Survey = mapper.Map<SurveyResponse>(survey);
                return View(model);
            }

            var request = mapper.Map<Submission>(model.Request);
            request.CreatedOn = DateTime.Now;
            await unitOfWork.Submissions.CreateAsync(request);
            await unitOfWork.SaveAsync();
            TempData["success"] = "Survey submitted successfully";
            return RedirectToAction(nameof(TakeSurvey), new { survey_id });
        }
        [HttpGet]
        [Authorize]
        [Route("/statistics")]
        public async Task<IActionResult> ShowSurveys()
        {
            return View("Index");
        }
        [HttpGet]
        [Authorize]
        [Route("/statistics/{survey_id:int}")]
        public async Task<IActionResult> ShowSurveyStatistics(int survey_id)
        {
            var submissions = await unitOfWork.Submissions.GetAllAsync(r => r.SurveyId == survey_id, includeProperties: true);
            var survey = await unitOfWork.Surveys.GetAsync(r => r.Id == survey_id);
            var model = new StatisticsViewModel()
            {
                Survey = mapper.Map<SurveyResponse>(survey),
                Submissions = mapper.Map<IEnumerable<SubmissionResponse>>(submissions),
            };
            model.StatsDict = model.Survey.Questions.ToDictionary(r => r, r => model.Submissions.SelectMany(t => t.Answers).Where(s => s.QuestionId == r.Id));
            return View("SurveyStatistics", model);
        }
        [HttpGet]
        [Authorize]
        [Route("/statistics/getall")]
        public async Task<IActionResult> GetAll() // For Datatable ajax call
        {
            return Json(new { data = mapper.Map<IEnumerable<SurveyResponse>>(await unitOfWork.Surveys.GetAllAsync()) });
        }
    }
}
