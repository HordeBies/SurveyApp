using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveyApp.Core.DTO;
using SurveyApp.Domain.Entities;
using SurveyApp.Domain.RepositoryContracts;
using SurveyApp.Web.Api.Models;
using System.Net;

namespace SurveyApp.Web.Api.Controllers
{
    [Route("api/surveys")]
    public class SurveysController(IUnitOfWork unitOfWork, IMapper mapper) : ApiController
    {

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<SurveyResponse>>>> GetSurveys(bool populateQuestions = false)
        {
            var result = mapper.Map<IEnumerable<SurveyResponse>>(await unitOfWork.Surveys.GetAllAsync(includeProperties: populateQuestions));
            return new ApiResponse<IEnumerable<SurveyResponse>>()
            {
                Result = result,
                StatusCode = HttpStatusCode.OK
            };
        }
        [HttpGet("{survey_id:int}")]
        public async Task<ActionResult<ApiResponse<SurveyResponse>>> GetSurvey(int survey_id)
        {
            var response = new ApiResponse<SurveyResponse>();
            if (survey_id == 0)
            {
                response.ErrorMessages.Add("Survey Id is required");
                response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(response);
            }
            var entity = await unitOfWork.Surveys.GetAsync(r => r.Id == survey_id);
            if(entity == null)
            {
                response.ErrorMessages.Add("Survey not found");
                response.StatusCode = HttpStatusCode.NotFound;
                return NotFound(response);
            }
            
            response.StatusCode = HttpStatusCode.OK;
            response.Result = mapper.Map<SurveyResponse>(entity);
            return response;
        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ApiResponse<SurveyResponse>>> CreateSurvey(SurveyCreateRequest request)
        {
            var survey = mapper.Map<Survey>(request);
            await unitOfWork.Surveys.CreateAsync(survey);
            await unitOfWork.SaveAsync();
            var response = new ApiResponse<SurveyResponse>()
            {
                Result = mapper.Map<SurveyResponse>(survey),
                StatusCode = HttpStatusCode.Created
            };
            return CreatedAtAction(nameof(GetSurvey), new { survey_id = survey.Id }, response);
        }
        [HttpPut("{survey_id:int}")]
        [Authorize]
        public async Task<ActionResult<ApiResponse<SurveyResponse>>> ReplaceSurvey(int survey_id, SurveyReplaceRequest request)
        {
            var response = new ApiResponse<SurveyResponse>();
            var survey = mapper.Map<Survey>(request);
            if(survey_id != survey.Id)
            {
                response.ErrorMessages.Add("Survey Id in request body does not match the one in the url");
                response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(response);
            }
            var questions = await unitOfWork.Questions.GetAllAsync(q => q.SurveyId == survey_id);
            foreach (var question in questions) // TODO: Preserve old questions for answers and analytics, need an isActive flag for partial survey.
            {
                await unitOfWork.Questions.DeleteAsync(question);
            }
            await unitOfWork.Surveys.UpdateAsync(survey);
            await unitOfWork.SaveAsync();
            response.StatusCode = HttpStatusCode.OK;
            response.Result = mapper.Map<SurveyResponse>(survey);
            return response;
        }
        [HttpDelete("{survey_id:int}")]
        [Authorize]
        public async Task<IActionResult> DeleteSurvey(int survey_id)
        {
            if(survey_id == 0)
            {
                var response = new ApiResponse();
                response.ErrorMessages.Add("Not a valid survey_id");
                response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(response);
            }
            var survey = await unitOfWork.Surveys.GetAsync(r => r.Id == survey_id);
            if(survey == null)
            {
                var response = new ApiResponse();
                response.ErrorMessages.Add("Not found the survey with given id");
                response.StatusCode = HttpStatusCode.NotFound;
                return NotFound(response);
            }
            await unitOfWork.Surveys.DeleteAsync(survey);
            await unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}
