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

namespace StudentHub.Services.Comment
{
    public class CommentService : ICommentService
    {
        private readonly ApplicationDBContext _applicationDBContext;
        private readonly ICommentMapper _commentMapper;


        public CommentService(ApplicationDBContext applicationDbContext, ICommentMapper commentMapper)
        {
            _applicationDBContext = applicationDbContext;
            _commentMapper = commentMapper;
        }

        public async Task<ResultModel<string>> AddComment(CommentDto commentDto)
        {
            var result = new ResultModel<string>();
            var comment = await _commentMapper.ToModel(commentDto);            
            if (comment == null)
            {
                result.AddError("Error Mapping");
            }
            try
            {                
                _applicationDBContext.Set<Domain.Comment>().Add(comment);
                await _applicationDBContext.SaveChangesAsync();
                result.Data = comment.Id.ToString();
            }
            catch (Exception e)
            {
                result.AddError("Internal Server error:" + e.Message + ". " + e.InnerException.Message);
            }
            return result;
        }

        public async Task<ResultModel<string>> DeleteComment(Guid commentId)
        {
            var resultModel = new ResultModel<string>();           
            try
            {
                var comment = await _applicationDBContext.Set<Domain.Comment>().FindAsync(commentId);
                _applicationDBContext.Set<Domain.Comment>().Remove(comment);
                await _applicationDBContext.SaveChangesAsync();
                resultModel.Data = comment.Id.ToString();
            }
            catch (Exception e)
            {
                resultModel.AddError("Internal Server error:" + e.Message + ". " + e.InnerException.Message);
            }
            
            return resultModel;
        }

        public async Task<ResultModel<CommentResponseDto>> GetComment(Guid commentId)
        {
            var resultModel = new ResultModel<CommentResponseDto>();
            try
            {
                var comment = await _applicationDBContext.Set<Domain.Comment>().FindAsync(commentId);
                var commentResponse = _commentMapper.ToResponseDto(comment);
                resultModel.Data = commentResponse;
            }
            catch (Exception e)
            {
                resultModel.AddError("Internal Server error:" + e.Message + ". " + e.InnerException.Message);
            }
            return resultModel;
        }

        public PagedResultModel<CommentResponseDto> GetQuestionComments(int index, int size, Guid questionId)
        {
            var commentsFromModel = _applicationDBContext.Set<Domain.Comment>().Where(x=>x.QuestionId == questionId).ToList().AsQueryable<Domain.Comment>().OrderBy(x => x.CreatedAt);
            var paginatedModel = commentsFromModel.Paginate(index, size);
            var commentDtos = new List<CommentResponseDto>();
            foreach (var comment in paginatedModel.Items)
            {
                commentDtos.Add(_commentMapper.ToResponseDto(comment));
            };

            var pagedResultModel = new PagedResultModel<CommentResponseDto>(size, index, commentDtos, commentDtos.Count);
            return pagedResultModel;
        }

        public PagedResultModel<CommentResponseDto> GetSolutionComments(int index, int size, Guid solutionId)
        {
            var commentsFromModel = _applicationDBContext.Set<Domain.Comment>().Where(x=>x.SolutionId == solutionId).ToList().AsQueryable<Domain.Comment>().OrderBy(x => x.CreatedAt);
            var paginatedModel = commentsFromModel.Paginate(index, size);
            var commentDtos = new List<CommentResponseDto>();
            foreach (var comment in paginatedModel.Items)
            {
                commentDtos.Add(_commentMapper.ToResponseDto(comment));
            };

            var pagedResultModel = new PagedResultModel<CommentResponseDto>(size, index, commentDtos, commentDtos.Count);
            return pagedResultModel;
        }
    }
}
