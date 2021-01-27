using StudentHub.Services.Tag;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentHub.Services.DtoMapper.Interface
{
    public interface ITagMapper : IMapper<Domain.Tag, TagDto, TagResponseDto>
    {
    }
}
