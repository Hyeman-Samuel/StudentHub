using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentHub.Core.Asp.NetCore;
using StudentHub.Core.Extensions;
using StudentHub.Infrastructure.Data;
using StudentHub.Services.Question;

namespace StudentHub.API.Controllers
{  
    [Authorize]
    [ApiController]    
    [Route("api/v1/[controller]/")]
    public class QuestionController : BaseController
    {
       private readonly IQuestionService _questionService;
        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        /// <summary>
        /// Add a Question
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> PostQuestion([FromBody] QuestionDto questionDto)                                                                                                                                       
        {
            if (!ModelState.IsValid)
            {
                return Response<string>(data: null, message: "", ApiResponseCode.INVALID_REQUEST, errors: ListModelStateErrors.ToArray());              
            }
            var currentUser = HttpContext.User;
            questionDto.AuthorId = currentUser.FindFirst("UserId").Value;  
            try
            {
                var result = await _questionService.AddQuestion(questionDto);
                if (result.HasError)
                {
                    return Response<string>(message: "Error",codes:ApiResponseCode.INVALID_REQUEST,errors: result.Errors);
                }
                return ApiResponse(data: result.Data);
            }
            catch (Exception e)
            {
                return Response<string>(message: "Internal Server Error"+e.Message,codes:ApiResponseCode.ERROR);
            }
        }

        /// <summary>
        /// Get Questions
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        [Route("")]
        public async Task<IActionResult> GetAllQuestions([FromQuery]string keyword = null,[FromQuery]int page = 1,[FromQuery]int size = 10)
        {
            var result = _questionService.GetQuestions(page, size);
            if(result.TotalCount == 0)
            {

            }
            return ApiResponse<PagedResultModel<QuestionResponseDto>>(data: result);
        }

        /// <summary>
        /// Get a Question
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        [Route("{questionId}")]
        public async Task<IActionResult> GetQuestion([FromRoute]Guid questionId)
        {
            if (!ModelState.IsValid)
            {
                return Response<string>(data: null, message: "", ApiResponseCode.INVALID_REQUEST, errors: ListModelStateErrors.ToArray());
            }
            try
            {
                var result = await _questionService.GetQuestion(questionId);
                if (result.HasError)
                {
                   return Response<string>(data: null, message: "Error", ApiResponseCode.INVALID_REQUEST, errors: result.Errors);
                }
                return ApiResponse(data: result.Data);
            }
            catch (Exception e)
            {
                return Response<string>(data: null, message: "Internal Server Error" + e.Message, codes: ApiResponseCode.ERROR);
            }

        }

        /// <summary>
        /// Edit a Question
        /// </summary>
        /// <remarks> e.g { Title : "Question Title", message : "Question Description"}</remarks>
        /// <param name="questionDto"> string username, password</param>
        /// <param name="questionId"> string username, password</param>
        /// <returns></returns>
        [HttpPut]
        [Route("{questionId}")]
        public async Task<IActionResult> EditQuestion(Guid questionId, [FromBody] QuestionEditDto questionEdit)
        {                                                                                                              
            if (questionId == null)
            {
                return Response<string>(data: null, message: "Error", ApiResponseCode.INVALID_REQUEST);
            }
            QuestionDto questionDto = new QuestionDto { Message =questionEdit.Message, Title =questionEdit.Title }; 
            try
            {
                var result = await _questionService.EditQuestion(questionDto, questionId);
                if (result.HasError)
                {
                    return Response<string>(data: null, message: "Error", ApiResponseCode.INVALID_REQUEST, errors: result.Errors);
                }
                return ApiResponse(data: result.Data);
            }
            catch (Exception e)
            {
                return Response<string>(data: null, message: "Internal Server Error" + e.Message, codes: ApiResponseCode.ERROR);
            }
        }

        /// <summary>
        /// delete Question
        /// </summary>
        [HttpDelete]
        [Route("{questionId}")]
        public async Task<IActionResult> DeleteQuestion(Guid questionId)
        {
            if (questionId == null)
            {
                return Response<string>(data: null, message: "Error", ApiResponseCode.INVALID_REQUEST);
            }

            try
            {
                var result = await _questionService.DeleteQuestion(questionId);
                if (result.HasError)
                {
                   return Response<string>(data: null, message: "Error", ApiResponseCode.INVALID_REQUEST, errors: result.Errors);
                }
                return ApiResponse(data: result.Data);
            }
            catch (Exception e)
            {
                return Response<string>(data: null, message: "Internal Server Error" + e.Message, codes: ApiResponseCode.ERROR);
            }
        }
    }
}