using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentHub.Core.Asp.NetCore;
using StudentHub.Infrastructure.Data;
using StudentHub.Services.Solution;

namespace StudentHub.API.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SolutionController : BaseController
    {
        private readonly ISolutionService _solutionService;

        public SolutionController(ISolutionService solutionService)
        {
            _solutionService = solutionService;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> PostSolution([FromBody] SolutionDto solutionDto)
        {
            if (!ModelState.IsValid)
            {
                return Response<string>(data: null, message: "", ApiResponseCode.INVALID_REQUEST, errors: ListModelStateErrors.ToArray());
            }
            var currentUser = HttpContext.User;
            solutionDto.AuthorId = currentUser.FindFirst("UserId").Value;
            try
            {
                var result = await _solutionService.AddSolution(solutionDto);
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
        /// Get a Solution
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        [Route("{solutionId}")]
        public async Task<IActionResult> GetSolution([FromRoute]Guid solutionId)
        {
            if (!ModelState.IsValid)
            {
                return Response<string>(data: null, message: "", ApiResponseCode.INVALID_REQUEST, errors: ListModelStateErrors.ToArray());
            }
            try
            {
                var result = await _solutionService.GetSolution(solutionId);
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
        /// Get Solutions
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        [Route("question/{questionId}")]
        public async Task<IActionResult> GetSolutions(Guid questionId, [FromQuery]string keyword = null, [FromQuery]int page = 1, [FromQuery]int size = 10)
        {
            var result = _solutionService.GetSolutions(page, size, questionId);
            if (result.TotalCount == 0)
            {

            }
            return ApiResponse<PagedResultModel<SolutionResponseDto>>(data: result);
        }

        /// <summary>
        /// Edit a Solution
        /// </summary>

        [HttpPut]
        [Route("{solutionId}")]
        public async Task<IActionResult> EditSolution(Guid solutionId, [FromBody] SolutionEditDto solutionEdit)
        {
            if (solutionId == null)
            {
                return Response<string>(data: null, message: "Error", ApiResponseCode.INVALID_REQUEST);
            }
            SolutionDto solutionDto = new SolutionDto { Message = solutionEdit.Message};
            try
            {
                var result = await _solutionService.EditSolution(solutionDto, solutionId);
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
        /// delete a Solution
        /// </summary>
        [HttpDelete]
        [Route("{solutionId}")]
        public async Task<IActionResult> DeleteSolution(Guid solutionId)
        {
            if (solutionId == null)
            {
                return Response<string>(data: null, message: "Error", ApiResponseCode.INVALID_REQUEST);
            }

            try
            {
                var result = await _solutionService.DeleteSolution(solutionId);
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