using StudentHub.Infrastructure.Data;
using StudentHub.Services.ResultModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudentHub.Services.Tag
{
    public interface ITagService
    {
        Task<ResultModel<string>> AddTag(TagDto tag);

        Task<ResultModel<TagResponseDto>> GetTag(Guid tagId);

        Task<ResultModel<TagResponseDto>> EditTag(TagDto tag, Guid tagId);

        Task<ResultModel<string>> DeleteTag(Guid tagId);

        PagedResultModel<TagResponseDto> GetTags(int index, int size);
    }
}
