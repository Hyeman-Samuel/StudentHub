using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudentHub.Services.DtoMapper.Interface
{
    public interface IMapper<Model,DTO,Response> where Model:class where DTO:class where Response:class
    {
        public Response ToResponseDto(Model model);
        public Task<Model> ToModel(DTO dto);
    }
}
