using StudentHub.Services.Comment;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudentHub.Services.DtoMapper.Interface
{
    public interface ICommentMapper : IMapper<Domain.Comment, CommentDto, CommentResponseDto>
    {
        //public CommentResponseDto ToCommentResponseDto(Domain.Comment comment);
        //public Task<Domain.Comment> ToCommentModel(CommentDto commentDto);
    }
}
