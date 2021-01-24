using StudentHub.Infrastructure.Data;
using StudentHub.Services.ResultModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudentHub.Services.Comment
{
    public interface ICommentService
    {
        Task<ResultModel<string>> AddComment(CommentDto Comment);

        Task<ResultModel<CommentResponseDto>> GetComment(Guid CommentId);

        //Task<ResultModel<CommentResponseDto>> EditComment(CommentDto comment, Guid commentId);

        Task<ResultModel<string>> DeleteComment(Guid commentId);

        PagedResultModel<CommentResponseDto> GetQuestionComments(int index, int size, Guid questionId);

        PagedResultModel<CommentResponseDto> GetSolutionComments(int index, int size, Guid solutionId);
    }
}
