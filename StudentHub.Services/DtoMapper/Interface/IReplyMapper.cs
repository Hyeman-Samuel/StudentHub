﻿using StudentHub.Services.Reply;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentHub.Services.DtoMapper.Interface
{
    public interface IReplyMapper : IMapper<Domain.Reply,ReplyDto,ReplyResponseDto>
    {
    }
}
