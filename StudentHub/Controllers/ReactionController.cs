using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentHub.Core.Asp.NetCore;
using StudentHub.Services.Reaction;

namespace StudentHub.API.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ReactionController : BaseController
    {
        private readonly IReactionService _reactionService;

        public ReactionController(IReactionService reactionService )
        {
            _reactionService = reactionService;
        }
        /// <summary>
        /// Post UpVote
        /// </summary>
        [HttpPost]
        [Route("upvote/{solutionId}")]
        public async Task<IActionResult> UpVote(Guid solutionId)
        {
            if (!ModelState.IsValid)
            {
                return Response<string>(data: null, message: "", ApiResponseCode.INVALID_REQUEST, errors: ListModelStateErrors.ToArray());
            }
            var authorId = HttpContext.User.FindFirst("UserId").Value;
            try
            {
                var result = await _reactionService.UpVoteSolution(authorId, solutionId);
                return ApiResponse(data: result);
            }
            catch (Exception e)
            {
                return Response<string>(data: null, message: "Internal Server Error" + e.Message, codes: ApiResponseCode.ERROR);
            }
        }

        /// <summary>
        /// Remove UpVote
        /// </summary>
        [HttpDelete]
        [Route("upvote/remove")]
        public async Task<IActionResult> RemoveUpVote()
        {
            return Ok();
        }

        /// <summary>
        /// Post DownVote
        /// </summary>
        [HttpPost]
        [Route("downvote/{solutionId}")]
        public async Task<IActionResult> DownVote(Guid solutionId)
        {
            if (!ModelState.IsValid)
            {
                return Response<string>(data: null, message: "", ApiResponseCode.INVALID_REQUEST, errors: ListModelStateErrors.ToArray());
            }
            var authorId = HttpContext.User.FindFirst("UserId").Value;
            try
            {
                var result = await _reactionService.DownVoteSolution(authorId, solutionId);
                return ApiResponse(data: result);
            }
            catch (Exception e)
            {
                return Response<string>(data: null, message: "Internal Server Error" + e.Message, codes: ApiResponseCode.ERROR);
            }
        }


        /// <summary>
        /// Post DownVote
        /// </summary>
        [HttpDelete]
        [Route("downvote/remove")]
        public async Task<IActionResult> RemoveDownVote()
        {
            return Ok();
        }
    }

}