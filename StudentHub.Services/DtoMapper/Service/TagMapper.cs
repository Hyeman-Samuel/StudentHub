using StudentHub.Services.DtoMapper.Interface;
using StudentHub.Services.Tag;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudentHub.Services.DtoMapper.Service
{
    public class TagMapper : ITagMapper
    {
        public async Task<Domain.Tag> ToModel(TagDto dto)
        {
            var tag = new Domain.Tag
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                Type = Domain.TagType.OFFICIAL
            };

            return tag;
        }

        public TagResponseDto ToResponseDto(Domain.Tag model)
        {
            var response = new TagResponseDto
            {
                Id = model.Id,
                Title = model.Title
            };

            return response;
        }
    }
}
