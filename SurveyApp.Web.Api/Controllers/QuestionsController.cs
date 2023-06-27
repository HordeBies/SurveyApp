using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveyApp.Core.DTO;
using SurveyApp.Domain.Entities;
using SurveyApp.Domain.RepositoryContracts;
using SurveyApp.Web.Api.Models;
using System.Net;

namespace SurveyApp.Web.Api.Controllers
{
    [Route("api/surveys/{survey_id:int}/questions")]
    public class QuestionsController(IUnitOfWork unitOfWork, IMapper mapper) : ApiController
    {

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<QuestionResponse>>>> GetQuestions(int survey_id)
        {
            var response = new ApiResponse<IEnumerable<QuestionResponse>>();
            if (survey_id == 0)
            {
                response.ErrorMessages.Add("Survey Id is required");
                response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest();
            }
            var survey = await unitOfWork.Surveys.GetAsync(r => r.Id == survey_id);
            if (survey == null)
            {
                response.ErrorMessages.Add("Survey not found");
                response.StatusCode = HttpStatusCode.NotFound;
                return NotFound(response);
            }
            response.Result = mapper.Map<IEnumerable<QuestionResponse>>(survey.Questions);
            response.StatusCode = HttpStatusCode.OK;
            return response;
        }

        [HttpGet("{question_id:int}")]
        public async Task<ActionResult<ApiResponse<QuestionResponse>>> GetQuestion(int survey_id,int question_id)
        {
            var response = new ApiResponse<QuestionResponse>();
            if(survey_id == 0 || question_id == 0)
            {
                response.ErrorMessages.Add("Survey Id and Question Id are required");
                response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(response);
            }
            var question = await unitOfWork.Questions.GetAsync(r => r.Id == question_id && r.SurveyId == survey_id);
            if(question == null)
            {
                response.ErrorMessages.Add("Question not found");
                response.StatusCode = HttpStatusCode.NotFound;
                return NotFound(response);
            }
            response.Result = mapper.Map<QuestionResponse>(question);
            response.StatusCode = HttpStatusCode.OK;
            return response;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateQuestion(int survey_id, QuestionCreateRequest request)
        {
            var response = new ApiResponse<QuestionResponse>();
            if(survey_id == 0)
            {
                response.ErrorMessages.Add("Survey Id is required");
                response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(response);
            }
            var survey = await unitOfWork.Surveys.GetAsync(r => r.Id == survey_id/*, tracked:true*/);
            if(survey == null)
            {
                response.ErrorMessages.Add("Survey not found");
                response.StatusCode = HttpStatusCode.NotFound;
                return NotFound(response);
            }
            var question = mapper.Map<Question>(request);
            question.SurveyId = survey.Id;
            await unitOfWork.Questions.CreateAsync(question);
            //survey.Questions.Add(question); // or track the survey and add the question to navigation property
            await unitOfWork.SaveAsync();

            response.StatusCode = HttpStatusCode.Created;
            response.Result = mapper.Map<QuestionResponse>(question);
            return CreatedAtAction(nameof(GetQuestion),new { survey_id = survey.Id, question_id = question.Id }, response);
        }

        [HttpPut("{question_id:int}")]
        [Authorize]
        public async Task<ActionResult<ApiResponse<QuestionResponse>>> ReplaceQuestion(int survey_id, int question_id, QuestionReplaceRequest request)
        {
            var response = new ApiResponse<QuestionResponse>();
            if(survey_id == 0 || question_id == 0)
            {
                response.ErrorMessages.Add("Survey Id and Question Id are required");
                response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(response);
            }
            if(question_id != request.Id || survey_id != request.SurveyId)
            {
                response.ErrorMessages.Add("Question Id or Survey Id in request body does not match the one in the url");
                response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(response);
            }

            var questionFromDb = await unitOfWork.Questions.GetAsync(r => r.Id == question_id && r.SurveyId == survey_id);
            if(questionFromDb == null)
            {
                response.ErrorMessages.Add("Question not found");
                response.StatusCode = HttpStatusCode.NotFound;
                return NotFound(response);
            }
            foreach (var choice in questionFromDb.Choices) // delete old question choices
            {
                await unitOfWork.Choices.DeleteAsync(choice);
            }

            var question = mapper.Map<Question>(request);
            await unitOfWork.Questions.UpdateAsync(question);
            await unitOfWork.SaveAsync();
            response.Result = mapper.Map<QuestionResponse>(question);
            response.StatusCode = HttpStatusCode.OK;
            return response;
        }

        [HttpDelete("{question_id:int}")]
        [Authorize]
        public async Task<IActionResult> DeleteQuestion(int survey_id, int question_id)
        {
            var response = new ApiResponse();
            if(survey_id == 0 || question_id == 0)
            {
                response.ErrorMessages.Add("Survey Id and Question Id are required");
                response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(response);
            }
            var question = await unitOfWork.Questions.GetAsync(r => r.Id == question_id && r.SurveyId == survey_id);
            if(question == null)
            {
                response.ErrorMessages.Add("Question not found");
                response.StatusCode = HttpStatusCode.NotFound;
                return NotFound(response);
            }
            await unitOfWork.Questions.DeleteAsync(question);
            await unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}
