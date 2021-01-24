using StudentHub.Infrastructure.Data;
using StudentHub.Services.ResultModel;
using System;
using System.Threading.Tasks;

namespace StudentHub.Services.Solution
{
    public interface ISolutionService
    {
        Task<ResultModel<string>> AddSolution(SolutionDto solution);

        Task<ResultModel<SolutionResponseDto>> GetSolution(Guid solutionId);

        Task<ResultModel<SolutionResponseDto>> EditSolution(SolutionDto solution, Guid solutionId);

        Task<ResultModel<string>> DeleteSolution(Guid solutionId);

        PagedResultModel<SolutionResponseDto> GetSolutions(int index, int size,Guid questionId);
    }
}
