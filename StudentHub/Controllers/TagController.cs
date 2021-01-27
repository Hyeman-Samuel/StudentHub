using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentHub.Core.Asp.NetCore;
using StudentHub.Infrastructure.Data;
using StudentHub.Services.Tag;

namespace StudentHub.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class TagController : BaseController
    {
        private readonly ITagService _tagService;
        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }



        /// <summary>
        /// Add a Tag
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> PostTag([FromBody] TagDto tagDto)
        {
            if (!ModelState.IsValid)
            {
                return Response<string>(data: null, message: "", ApiResponseCode.INVALID_REQUEST, errors: ListModelStateErrors.ToArray());
            }
            var currentUser = HttpContext.User;
            tagDto.AuthorId = currentUser.FindFirst("UserId").Value; ;
            try
            {
                var result = await _tagService.AddTag(tagDto);
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
        /// Get Tags
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        [Route("")]
        public async Task<IActionResult> GetAllTags([FromQuery]string keyword = null, [FromQuery]int page = 1, [FromQuery]int size = 10)
        {
            var result = _tagService.GetTags(page, size);
            if (result.TotalCount == 0)
            {

            }
            return ApiResponse<PagedResultModel<TagResponseDto>>(data: result);
        }

        /// <summary>
        /// Get a Tag
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        [Route("{tagId}")]
        public async Task<IActionResult> GetTag([FromRoute]Guid tagId)
        {
            if (!ModelState.IsValid)
            {
                return Response<string>(data: null, message: "", ApiResponseCode.INVALID_REQUEST, errors: ListModelStateErrors.ToArray());
            }
            try
            {
                var result = await _tagService.GetTag(tagId);
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
        /// Edit a Tag
        /// </summary>
        /// <remarks> e.g { Title : "Tag Title"}</remarks>
        /// <returns></returns>
        [HttpPut]
        [Route("{tagId}")]
        public async Task<IActionResult> EditTag(Guid tagId, [FromBody] TagDto tagDto)
        {
            if (tagId == null)
            {
                return Response<string>(data: null, message: "Error", ApiResponseCode.INVALID_REQUEST);
            }
            TagDto newTagDto = new TagDto {Title = tagDto.Title };
            try
            {
                var result = await _tagService.EditTag(newTagDto, tagId);
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
        /// delete Tag                                                                                                                                                          
        /// </summary>
        [HttpDelete]
        [Route("{tagId}")]
        public async Task<IActionResult> DeleteQuestion(Guid tagId)
        {
            if (tagId == null)
            {
                return Response<string>(data: null, message: "Error", ApiResponseCode.INVALID_REQUEST);
            }

            try
            {
                var result = await _tagService.DeleteTag(tagId);
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