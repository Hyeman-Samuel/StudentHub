using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;
using StudentHub.Domain.Identity;
using StudentHub.Infrastructure.Network;
using StudentHub.Services.DtoMapper.Interface;
using StudentHub.Services.Image;
using StudentHub.Services.Question;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHub.Services.DtoMapper.Service
{
    public class QuestionMapper : IQuestionMapper
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IOcrScanner _ocrScanner;
        public QuestionMapper(UserManager<ApplicationUser> userManager,IOcrScanner ocrScanner)
        {
            _userManager = userManager;
            _ocrScanner = ocrScanner;
        }

        public async Task<Domain.Question> ToModel(QuestionDto dto)
        {
            var Question = new Domain.Question();
            Question.Id = Guid.NewGuid();
            Question.AuthorId = dto.AuthorId;
            Question.TimeAdded = dto.TimeAdded;
            Question.Message = dto.Message;
            Question.Title = dto.Title;
            var Images = new List<Domain.Image>();
            foreach (var image in dto.Images)
            {
                if (image.base64Format == null)
                {
                    return null;
                }
                ///Move To Mapper
                var _Image = new Domain.Image();
                _Image.Id = Guid.NewGuid();
                _Image.Latex = await _ocrScanner.RunImageOcrScan(image.base64Format);
                /////
                Images.Add(_Image);
            }

            Question.Images = Images;
            Question.CreatedAt = DateTime.UtcNow;
            return Question;
        }


        public QuestionResponseDto ToResponseDto(Domain.Question model)
        {
            var tags = model.Tags.Select(x => new { x.Tag });
            var images = new List<ImageResponseDto>();
            foreach (var Image in model.Images)
            {
                if (Image.ImageLink == null)
                {
                    return null;
                }

                var image = new ImageResponseDto
                {
                    Url = Image.ImageLink
                };
                images.Add(image);
            }

            var questionResponse = new QuestionResponseDto
            {
                Id = model.Id,
                AuthorId = model.AuthorId,
                Message = model.Message,
                TimeAdded = model.TimeAdded,
                Title = model.Title,
                Images = images
            };

            return questionResponse;
        }
    }
}
