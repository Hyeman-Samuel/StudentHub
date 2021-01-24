using StudentHub.Services.Solution;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudentHub.Services.DtoMapper.Interface
{
    public interface ISolutionMapper : IMapper<Domain.Solution, SolutionDto, SolutionResponseDto>
    {
        //public SolutionResponseDto ToSolutionResponseDto(Domain.Solution solution);
        //public Task<Domain.Solution> ToSolutionModel(SolutionDto questionDto);
    }
}
