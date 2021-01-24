using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentHub.Core.Asp.NetCore;
using StudentHub.Infrastructure.Data;
using StudentHub.Services.Comment;

namespace StudentHub.API.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CommentController : BaseController
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        /// <summary>
        /// Add a Comment
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> PostComment([FromBody] CommentDto commentDto)
        {
            if (!ModelState.IsValid)
            {
                return Response<string>(data: null, message: "", ApiResponseCode.INVALID_REQUEST, errors: ListModelStateErrors.ToArray());
            }
            var currentUser = HttpContext.User;
            commentDto.AuthorId = currentUser.FindFirst("UserId").Value;
            try
            {
                var result = await _commentService.AddComment(commentDto);
                if (result.HasError)
                {
                    return Response<string>(message: "Error", codes: ApiResponseCode.INVALID_REQUEST, errors: result.Errors);
                }
                return ApiResponse(data: result.Data);
            }
            catch (Exception e)
            {
                return Response<string>(message: "Internal Server Error" + e.Message, codes: ApiResponseCode.ERROR);
            }
        }

        /// <summary>
        /// Get Comments from Question
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        [Route("/question/{questionId}")]
        public async Task<IActionResult> GetAllQuestionComments(Guid questionId,[FromQuery]string keyword = null, [FromQuery]int page = 1, [FromQuery]int size = 10)
        {
            var result = _commentService.GetQuestionComments(page,size,questionId);
            if (result.TotalCount == 0)
            {

            }
            return ApiResponse<PagedResultModel<CommentResponseDto>>(data: result);
        }
        
        /// <summary>
        /// Get Comments from Solution
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        [Route("/solution/{solutionId}")]
        public async Task<IActionResult> GetAllSolutionComments(Guid solutionId,[FromQuery]string keyword = null, [FromQuery]int page = 1, [FromQuery]int size = 10)
        {
            var result = _commentService.GetSolutionComments(page,size,solutionId);
            if (result.TotalCount == 0)
            {

            }
            return ApiResponse<PagedResultModel<CommentResponseDto>>(data: result);
        }

        /// <summary>
        /// Get a Comment
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        [Route("{commentId}")]
        public async Task<IActionResult> GetComment([FromRoute]Guid commentId)
        {
            if (!ModelState.IsValid)
            {
                return Response<string>(data: null, message: "", ApiResponseCode.INVALID_REQUEST, errors: ListModelStateErrors.ToArray());
            }
            try
            {
                var result = await _commentService.GetComment(commentId);
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
        /// delete Comment
        /// </summary>
        [HttpDelete]
        [Route("{commentId}")]
        public async Task<IActionResult> DeleteComment(Guid commentId)
        {
            if (commentId == null)
            {
                return Response<string>(data: null, message: "Error", ApiResponseCode.INVALID_REQUEST);
            }

            try
            {
                var result = await _commentService.DeleteComment(commentId);
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