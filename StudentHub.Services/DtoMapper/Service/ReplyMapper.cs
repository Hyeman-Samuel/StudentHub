using StudentHub.Services.DtoMapper.Interface;
using StudentHub.Services.Reply;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudentHub.Services.DtoMapper.Service
{
    public class ReplyMapper : IReplyMapper
    {
        public async Task<Domain.Common.Reply> ToModel(ReplyDto dto)
        {
            var reply = new Domain.Common.Reply
            {
                CreatedAt = DateTime.Now,
                Id = Guid.NewGuid(),
                Message = dto.Message,
                TimeAdded = dto.TimeAdded,
                AuthorId = dto.AuthorId,
                CommentRepliedToId = dto.CommentRepliedToId,
            };
            return reply;
        }

        public ReplyResponseDto ToResponseDto(Domain.Common.Reply model)
        {
            var replyResponse = new ReplyResponseDto
            {
                Id = model.Id,
                AuthorId = model.AuthorId,
                Message = model.Message,
                CommentRepliedToId = model.CommentRepliedToId,
                TimeAdded = model.TimeAdded
            };

            return replyResponse;
        }
    }
}
