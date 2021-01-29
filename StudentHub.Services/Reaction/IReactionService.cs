using StudentHub.Services.ResultModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudentHub.Services.Reaction
{
    public interface IReactionService
    {
        Task<ResultModel<string>> DownVoteQuestion(string authorId,Guid questionId);
        Task<ResultModel<string>> UpVoteQuestion(string authorId, Guid questionId);
        Task<ResultModel<string>> DownVoteSolution(string authorId, Guid solutionId);
        Task<ResultModel<string>> UpVoteSolution(string authorId, Guid solutionId);
    }
}
