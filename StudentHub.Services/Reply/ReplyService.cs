using StudentHub.Infrastructure;
using StudentHub.Infrastructure.Data;
using StudentHub.Infrastructure.Extensions;
using StudentHub.Services.DtoMapper.Interface;
using StudentHub.Services.ResultModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentHub.Services.Reply
{
    public class ReplyService : IReplyService
    {
        private readonly ApplicationDBContext _applicationDbContext;
        private readonly IReplyMapper _replyMapper;

        public ReplyService(IReplyMapper replyMapper,ApplicationDBContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _replyMapper = replyMapper;
        }
        public async Task<ResultModel<string>> AddReply(ReplyDto replyDto)
        {
            var resultModel = new ResultModel<string>();
            if (replyDto == null)
            {
                resultModel.AddError("reply is empty");
            }
            else
            {
                var reply = await _replyMapper.ToModel(replyDto);
                _applicationDbContext.Add(reply);
                await _applicationDbContext.SaveChangesAsync();
                resultModel.Data = reply.Id.ToString();
            }
            return resultModel;
        }

        public async Task<ResultModel<string>> DeleteReply(Guid replyId)
        {
            var resultModel = new ResultModel<string>();
            if (replyId == null)
            {
                resultModel.AddError("reply is empty");
            }
            else
            {
                var reply = await _applicationDbContext.Set<Domain.Common.Reply>().FindAsync(replyId);
                _applicationDbContext.Set<Domain.Common.Reply>().Remove(reply);
                await _applicationDbContext.SaveChangesAsync();

                resultModel.Data = reply.Id.ToString();
            }
            return resultModel;
        }

        public async Task<PagedResultModel<ReplyResponseDto>> GetReplies(Guid commentId,int index,int size)
        {
            var repliesFromModel = _applicationDbContext.Set<Domain.Common.Reply>().Where(x => x.CommentRepliedToId == commentId).ToList().AsQueryable<Domain.Common.Reply>().OrderBy(x => x.CreatedAt);
            var paginatedModel = repliesFromModel.Paginate(index, size);
            var replyDtos = new List<ReplyResponseDto>();
            foreach (var reply in paginatedModel.Items)
            {
                replyDtos.Add(_replyMapper.ToResponseDto(reply));
            };

            var pagedResultModel = new PagedResultModel<ReplyResponseDto>(size, index, replyDtos, replyDtos.Count);
            return pagedResultModel;
        }
    }
}
