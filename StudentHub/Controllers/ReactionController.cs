using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StudentHub.API.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ReactionController : ControllerBase
    {

        /// <summary>
        /// Post UpVote
        /// </summary>
        [HttpPost]
        [AllowAnonymous]
        [Route("upvote")]
        public async Task<IActionResult> GetReplies()
        {
            return Ok();
        }


        /// <summary>
        /// Post DownVote
        /// </summary>
        [HttpPost]
        [Route("downvote")]
        public async Task<IActionResult> DeleteReply()
        {
            return Ok();
        }
    }
}