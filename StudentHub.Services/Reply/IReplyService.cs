using StudentHub.Infrastructure.Data;
using StudentHub.Services.ResultModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudentHub.Services.Reply
{
    public interface IReplyService
    {
        Task<ResultModel<string>> AddReply(ReplyDto reply);
        Task<PagedResultModel<ReplyResponseDto>> GetReplies(Guid CommentId,int index,int size);
        Task<ResultModel<string>> DeleteReply(Guid ReplyId);
    }
}
