using StudentHub.Infrastructure;
using StudentHub.Infrastructure.Data;
using StudentHub.Infrastructure.Extensions;
using StudentHub.Services.DtoMapper.Interface;
using StudentHub.Services.ResultModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHub.Services.Solution
{
    public class SolutionService : ISolutionService
    {
        private readonly ApplicationDBContext _applicationDBContext;
        private readonly ISolutionMapper _solutionMapper;


        public SolutionService(ApplicationDBContext applicationDbContext, ISolutionMapper solutionMapper)
        {
            _applicationDBContext = applicationDbContext;
            _solutionMapper = solutionMapper;
        }

        public async Task<ResultModel<string>> AddSolution(SolutionDto solutionDto)
        {
            var result = new ResultModel<string>();
            var solution = await _solutionMapper.ToModel(solutionDto);
            if(solution == null)
            {
                result.AddError("Error Mapping");
            }
            try
            {
                _applicationDBContext.Set<Domain.Solution>().Add(solution);
                await _applicationDBContext.SaveChangesAsync();
                result.Data = solution.Id.ToString();
            }
            catch (Exception e)
            {
                result.AddError("Internal Server error:" + e.Message + ". " + e.InnerException.Message);
            }
            return result;
        }

        public async Task<ResultModel<string>> DeleteSolution(Guid solutionId)
        {
           var result = new ResultModel<string>();
            try
            {
                var solution = await _applicationDBContext.Set<Domain.Solution>().FindAsync(solutionId);
                _applicationDBContext.Set<Domain.Solution>().Remove(solution);
                result.Data = solution.Id.ToString();
            }
            catch (Exception e)
            {
                result.AddError("Internal Server error:" + e.Message + ". " + e.InnerException.Message);
            }

            return result;
        }

        public async Task<ResultModel<SolutionResponseDto>> EditSolution(SolutionDto solutionDto, Guid solutionId)
        {
            var resultModel = new ResultModel<SolutionResponseDto>();
            var solution = await _applicationDBContext.Set<Domain.Solution>().FindAsync(solutionId);
            if (!string.IsNullOrEmpty(solutionDto.Message))
            {
                solution.Message = solutionDto.Message;
            }
            try
            {
                _applicationDBContext.Set<Domain.Solution>().Update(solution);
                await _applicationDBContext.SaveChangesAsync();
                var solutionResponse = _solutionMapper.ToResponseDto(solution);
                resultModel.Data = solutionResponse;
            }
            catch (Exception e)
            {
                resultModel.AddError("Internal Server error:" + e.Message + ". " + e.InnerException.Message);
            }

            return resultModel;
        }

        public async Task<ResultModel<SolutionResponseDto>> GetSolution(Guid solutionId)
        {
            var resultModel = new ResultModel<SolutionResponseDto>();
            try
            {
                var solution = await _applicationDBContext.Set<Domain.Solution>().FindAsync(solutionId);
                if (solution == null)
                {
                    resultModel.AddError("Not Found");
                }
                else
                {
                    var solutionResponse = _solutionMapper.ToResponseDto(solution);
                    resultModel.Data = solutionResponse;
                }
            }
            catch (Exception e)
            {
                resultModel.AddError("Internal Server error:" + e.Message + ". " + e.InnerException.Message);
            }
            return resultModel;
        }

        public PagedResultModel<SolutionResponseDto> GetSolutions(int index, int size, Guid questionId)
        {
            var solutionsFromModel = _applicationDBContext.Set<Domain.Solution>().ToList().AsQueryable<Domain.Solution>().OrderBy(x => x.CreatedAt);
            var paginatedModel = solutionsFromModel.Paginate(index, size);
            var solutionDtos = new List<SolutionResponseDto>();
            foreach (var solution in paginatedModel.Items)
            {
                solutionDtos.Add(_solutionMapper.ToResponseDto(solution));
            };

            var pagedResultModel = new PagedResultModel<SolutionResponseDto>(size, index, solutionDtos, solutionDtos.Count);

            return pagedResultModel;
        }
    }
}
