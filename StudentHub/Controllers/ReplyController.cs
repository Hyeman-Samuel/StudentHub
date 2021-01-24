using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentHub.Core.Asp.NetCore;
using StudentHub.Services.Reply;

namespace StudentHub.API.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ReplyController : BaseController
    {
        private readonly IReplyService _replyService;

        public ReplyController(IReplyService replyService)
        {
            _replyService = replyService;
        }

        /// <summary>
        /// Get Replies
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        [Route("{commentId}")]
        public async Task<IActionResult> GetReplies([FromRoute]Guid commentId,[FromQuery]int index,[FromQuery]int size)
        {
            if (!ModelState.IsValid)
            {
                return Response<string>(data: null, message: "", ApiResponseCode.INVALID_REQUEST, errors: ListModelStateErrors.ToArray());
            }
            try
            {
                var result = await _replyService.GetReplies(commentId,index,size);                
                return ApiResponse(data: result);
            }
            catch (Exception e)
            {
                return Response<string>(data: null, message: "Internal Server Error" + e.Message, codes: ApiResponseCode.ERROR);
            }
        }


        /// <summary>
        /// Delete Reply
        /// </summary>
        [HttpDelete]
        [Route("{replyId}")]
        public async Task<IActionResult> DeleteReply(Guid replyId)
        {
            if (replyId == null)
            {
                return Response<string>(data: null, message: "Error", ApiResponseCode.INVALID_REQUEST);
            }

            try
            {
                var result = await _replyService.DeleteReply(replyId);
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
        /// Post Reply
        /// </summary>
        [HttpPost]
        [Route("{replyId}")]
        public async Task<IActionResult> AddReply([FromBody]ReplyDto replyDto)
        {
            if (replyDto == null)
            {
                return Response<string>(data: null, message: "Error", ApiResponseCode.INVALID_REQUEST);
            }

            try
            {
                var result = await _replyService.AddReply(replyDto);
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