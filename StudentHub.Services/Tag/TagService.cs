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

namespace StudentHub.Services.Tag
{
    public class TagService  :ITagService
    {
        private readonly ApplicationDBContext _applicationDBContext;
        private readonly ITagMapper _tagMapper;

        public TagService(ApplicationDBContext applicationDbContext,ITagMapper tagMapper)
        {
            _applicationDBContext = applicationDbContext;
            _tagMapper = tagMapper; 
        }

        public async Task<ResultModel<string>> AddTag(TagDto tagDto)
        {
            var result = new ResultModel<string>();
            var tag = await _tagMapper.ToModel(tagDto);
            if (tag == null)
            {
                result.AddError("Error Mapping");
            }
            try
            {
                _applicationDBContext.Set<Domain.Tag>().Add(tag);
                await _applicationDBContext.SaveChangesAsync();
                result.Data = tag.Id.ToString();
            }
            catch (Exception e)
            {
                result.AddError("Internal Server error:" + e.Message + ". " + e.InnerException.Message);
            }
            return result;
        }

        public async Task<ResultModel<string>> DeleteTag(Guid tagId)
        {
            var result = new ResultModel<string>();
            try
            {
                var tag = await _applicationDBContext.Set<Domain.Tag>().FindAsync(tagId);
                _applicationDBContext.Set<Domain.Tag>().Remove(tag);
                result.Data = tag.Id.ToString();
            }
            catch (Exception e)
            {
                result.AddError("Internal Server error:" + e.Message + ". " + e.InnerException.Message);
            }

            return result;
        }

        public async Task<ResultModel<TagResponseDto>> EditTag(TagDto tagDto, Guid tagId)
        {
            var resultModel = new ResultModel<TagResponseDto>();
            var tag = await _applicationDBContext.Set<Domain.Tag>().FindAsync(tagId);
            if (!string.IsNullOrEmpty(tagDto.Title))
            {
                tag.Title = tagDto.Title;
            }
            try
            {
                _applicationDBContext.Set<Domain.Tag>().Update(tag);
                await _applicationDBContext.SaveChangesAsync();
                var tagResponse = _tagMapper.ToResponseDto(tag);
                resultModel.Data = tagResponse;
            }
            catch (Exception e)
            {
                resultModel.AddError("Internal Server error:" + e.Message + ". " + e.InnerException.Message);
            }

            return resultModel;
        }

        public async Task<ResultModel<TagResponseDto>> GetTag(Guid tagId)
        {
            var resultModel = new ResultModel<TagResponseDto>();
            try
            {
                var tag = await _applicationDBContext.Set<Domain.Tag>().FindAsync(tagId);
                if (tag == null)
                {
                    resultModel.AddError("Not Found");
                }
                else
                {
                    var tagResponse = _tagMapper.ToResponseDto(tag);
                    resultModel.Data = tagResponse;
                }
            }
            catch (Exception e)
            {
                resultModel.AddError("Internal Server error:" + e.Message + ". " + e.InnerException.Message);
            }
            return resultModel;
        }

        public  PagedResultModel<TagResponseDto> GetTags(int index, int size)
        {
            var tagsFromModel = _applicationDBContext.Set<Domain.Tag>().ToList().AsQueryable<Domain.Tag>().OrderBy(x => x.CreatedAt);
            var paginatedModel = tagsFromModel.Paginate(index, size);
            var tagDtos = new List<TagResponseDto>();
            foreach (var tag in paginatedModel.Items)
            {
                tagDtos.Add(_tagMapper.ToResponseDto(tag));
            };

            var pagedResultModel = new PagedResultModel<TagResponseDto>(size, index, tagDtos, tagDtos.Count);

            return pagedResultModel;
        }
    }
}
