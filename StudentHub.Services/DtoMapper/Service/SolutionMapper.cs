using StudentHub.Infrastructure.Network.Ocr;
using StudentHub.Services.DtoMapper.Interface;
using StudentHub.Services.Image;
using StudentHub.Services.Solution;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudentHub.Services.DtoMapper.Service
{
    public class SolutionMapper : ISolutionMapper
    {
        private readonly IOcrScanner _ocrScanner;

        public SolutionMapper(IOcrScanner ocrScanner)
        {
            _ocrScanner = ocrScanner;  
        }

        public async Task<Domain.Solution> ToModel(SolutionDto dto)
        {
            var solution = new Domain.Solution
            {
                CreatedAt = DateTime.Now,
                Id = Guid.NewGuid(),
                Message = dto.Message,
                TimeAdded = dto.TimeAdded,
                AuthorId = dto.AuthorId,
                QuestionId = dto.QuestionId
            };
            var Images = new List<Domain.Image>();
            foreach (var image in dto.Images)
            {
                if (image.base64Format == null)
                {
                    return null;
                }               
                var _Image = new Domain.Image();
                _Image.Id = Guid.NewGuid();
                _Image.Latex = await _ocrScanner.RunImageOcrScan(image.base64Format);
                Images.Add(_Image);
            }

            solution.Images = Images;
            return solution;
        }

        public SolutionResponseDto ToResponseDto(Domain.Solution model)
        {
            var solutionResponse = new SolutionResponseDto
            {
                Id = model.Id,
                AuthorId = model.AuthorId,
                Message = model.Message,
                QuestionId = model.QuestionId,
                TimeAdded = model.TimeAdded
            };

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
            solutionResponse.Images = images;
            return solutionResponse;
        }
    }
}
