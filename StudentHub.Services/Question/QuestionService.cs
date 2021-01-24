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

namespace StudentHub.Services.Question
{
    public class QuestionService : IQuestionService
    {
        private readonly ApplicationDBContext _applicationDBContext;
        private readonly IQuestionMapper _questionMapper;


        public QuestionService(ApplicationDBContext applicationDbContext,IQuestionMapper questionMapper)
        {
            _applicationDBContext = applicationDbContext;
            _questionMapper = questionMapper;           
        }

        public async Task<ResultModel<string>> AddQuestion(QuestionDto question)
        {
            var resultModel = new ResultModel<string>();
            var Question = await _questionMapper.ToModel(question);
            if (Question == null)
            {
                resultModel.AddError("Internal Server error");
            }
            try
            {
                await _applicationDBContext.Question.AddAsync(Question);
                await _applicationDBContext.SaveChangesAsync();
                resultModel.Data = Question.Id.ToString();
            }catch(Exception e)
            {
                resultModel.AddError("Internal Server error:"+e.Message+". "+e.InnerException.Message);
            }
            
            return resultModel;
        }

        public async Task<ResultModel<string>> DeleteQuestion(Guid questionId)
        {
            var resultModel = new ResultModel<string>();
            var question = await _applicationDBContext.Set<Domain.Question>().FindAsync(questionId);
            try
            {
                _applicationDBContext.Question.Remove(question);
                await _applicationDBContext.SaveChangesAsync();
            }
            catch(Exception e)
            {
                resultModel.AddError("Internal Server error:" + e.Message + ". " + e.InnerException.Message);
            }
            resultModel.Data = question.Id.ToString();
            return resultModel;
        }

        public async Task<ResultModel<QuestionResponseDto>> EditQuestion(QuestionDto question,Guid questionId)
        {
            var resultModel = new ResultModel<QuestionResponseDto>();
            var _question = await _applicationDBContext.Set<Domain.Question>().FindAsync(questionId);
            if (!string.IsNullOrEmpty(question.Message))
            {
                _question.Message = question.Message;
            }
            if (!string.IsNullOrEmpty(question.Title)){
                _question.Title = question.Title;
            }
            try
            {
                _applicationDBContext.Set<Domain.Question>().Update(_question);
                await _applicationDBContext.SaveChangesAsync();
                var questionResponse = _questionMapper.ToResponseDto(_question);
                resultModel.Data = questionResponse;
            }
            catch(Exception e)
            {
                resultModel.AddError("Internal Server error:" + e.Message + ". " + e.InnerException.Message);
            }
           
            return resultModel;
        }

        public async Task<ResultModel<QuestionResponseDto>> GetQuestion(Guid questionId)
        {
            var resultModel = new ResultModel<QuestionResponseDto>();
            try
            {
                var _question = await _applicationDBContext.Set<Domain.Question>().FindAsync(questionId);
                await _applicationDBContext.SaveChangesAsync();
                var questionResponse = _questionMapper.ToResponseDto(_question);
                resultModel.Data = questionResponse;               
            }
            catch(Exception e)
            {
                resultModel.AddError("Internal Server error:" + e.Message + ". " + e.InnerException.Message);
            }
          return resultModel;
        }

        public PagedResultModel<QuestionResponseDto> GetQuestions(int index,int size)
        {
            var questionsFromModel = _applicationDBContext.Set<Domain.Question>().ToList().AsQueryable<Domain.Question>().OrderBy(x => x.CreatedAt);
            var paginatedModel = questionsFromModel.Paginate(index, size);
            var questionDtos = new List<QuestionResponseDto>();
            foreach (var question in paginatedModel.Items)
            {
                questionDtos.Add(_questionMapper.ToResponseDto(question));
            };

            var pagedResultModel = new PagedResultModel<QuestionResponseDto>(size, index,questionDtos,questionDtos.Count);

            return pagedResultModel;
        }
    }
}
