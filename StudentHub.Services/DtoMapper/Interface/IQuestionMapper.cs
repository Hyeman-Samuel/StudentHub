using StudentHub.Services.Question;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudentHub.Services.DtoMapper.Interface
{
    public interface IQuestionMapper : IMapper<Domain.Question, QuestionDto, QuestionResponseDto>
    {
       //public QuestionResponseDto ToQuestionResponseDto(Domain.Question question);
       //public Task<Domain.Question> ToQuestionModel(QuestionDto questionDto);
    }
}
