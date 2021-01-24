using StudentHub.Infrastructure.Data;
using StudentHub.Services.ResultModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudentHub.Services.Question
{
    public interface IQuestionService
    {
        Task<ResultModel<string>> AddQuestion(QuestionDto question);

        Task<ResultModel<QuestionResponseDto>> GetQuestion(Guid questionId);

        Task<ResultModel<QuestionResponseDto>> EditQuestion(QuestionDto question,Guid questionId);

        Task<ResultModel<string>> DeleteQuestion(Guid questionId);

        PagedResultModel<QuestionResponseDto> GetQuestions(int index,int size);
    }
}
