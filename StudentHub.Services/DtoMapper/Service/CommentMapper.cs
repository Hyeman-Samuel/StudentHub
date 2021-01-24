using StudentHub.Services.Comment;
using StudentHub.Services.DtoMapper.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentHub.Services.DtoMapper.Service
{
    public class CommentMapper : ICommentMapper
    {
        public async Task<Domain.Comment> ToModel(CommentDto dto)
        {
            var comment = new Domain.Comment
            {
                CreatedAt = DateTime.Now,
                Id = Guid.NewGuid(),
                Message = dto.Message,
                TimeAdded = dto.TimeAdded,
                AuthorId = dto.AuthorId,
                QuestionId = dto.QuestionId
            };
            return comment;
        }

        public CommentResponseDto ToResponseDto(Domain.Comment model)
        {
            var commentResponse = new CommentResponseDto
            {
                Id = model.Id,
                AuthorId = model.AuthorId,
                Message = model.Message,
                QuestionId = model.QuestionId.Value,
                TimeAdded = model.TimeAdded
            };

            return commentResponse; 
        }
    }
}
