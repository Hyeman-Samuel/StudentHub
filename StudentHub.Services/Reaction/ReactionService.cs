using StudentHub.Infrastructure;
using StudentHub.Services.ResultModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHub.Services.Reaction
{
    public class ReactionService : IReactionService
    {
        private readonly ApplicationDBContext _applicationDbContext;

        public ReactionService(ApplicationDBContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<ResultModel<string>> DownVoteQuestion(string authorId,Guid questionId)
        {
            var resultModel = new ResultModel<string>();
            if (questionId == null)
            {
                resultModel.AddError("reply is empty");
            }
            else
            {
                var existingReaction = _applicationDbContext.Reaction.Where(x => x.QuestionId == questionId).Where(x => x.AuthorId == authorId);
                if (existingReaction.Count() != 0)
                {
                    _applicationDbContext.Reaction.RemoveRange(existingReaction);
                }
                var reaction = new Domain.Reaction
                {
                    Id = Guid.NewGuid(),
                    AuthorId = authorId,
                    QuestionId = questionId,
                    Vote= Domain.Vote.Down
                };

                _applicationDbContext.Reaction.Add(reaction);
                await _applicationDbContext.SaveChangesAsync();
            }
            return resultModel;
        }

        public async Task<ResultModel<string>> DownVoteSolution(string authorId, Guid solutionId)
        {
            var resultModel = new ResultModel<string>();
            if (solutionId == null)
            {
                resultModel.AddError("reply is empty");
            }
            else
            {
                var existingReaction = _applicationDbContext.Reaction.Where(x => x.SolutionId == solutionId).Where(x => x.AuthorId == authorId);
                if (existingReaction.Count() != 0) 
                {
                    _applicationDbContext.Reaction.RemoveRange(existingReaction);
                }
                var reaction = new Domain.Reaction
                {
                    Id = Guid.NewGuid(),
                    AuthorId = authorId,
                    SolutionId = solutionId,
                    Vote = Domain.Vote.Down
                };

                _applicationDbContext.Reaction.Add(reaction);
                await _applicationDbContext.SaveChangesAsync();
            }
            return resultModel;
        }

        public async Task<ResultModel<string>> UpVoteQuestion(string authorId, Guid questionId)
        {
            var resultModel = new ResultModel<string>();
            if (questionId == null)
            {
                resultModel.AddError("reply is empty");
            }
            else
            {
                var existingReaction = _applicationDbContext.Reaction.Where(x => x.QuestionId == questionId).Where(x => x.AuthorId == authorId);
                if (existingReaction.Count() != 0)
                {
                    _applicationDbContext.Reaction.RemoveRange(existingReaction);
                }
                var reaction = new Domain.Reaction
                {
                    Id = Guid.NewGuid(),
                    AuthorId = authorId,
                    QuestionId = questionId,
                    Vote = Domain.Vote.UP
                };

                _applicationDbContext.Reaction.Add(reaction);
                await _applicationDbContext.SaveChangesAsync();
            }
            return resultModel;
        }

        public async Task<ResultModel<string>> UpVoteSolution(string authorId, Guid solutionId)
        {
            var resultModel = new ResultModel<string>();
            if (solutionId == null)
            {
                resultModel.AddError("reply is empty");
            }
            else
            {
                var existingReaction = _applicationDbContext.Reaction.Where(x => x.SolutionId == solutionId).Where(x => x.AuthorId == authorId);
                if (existingReaction.Count() != 0) 
                {
                    _applicationDbContext.Reaction.RemoveRange(existingReaction);
                }
                var reaction = new Domain.Reaction
                {
                    Id = Guid.NewGuid(),
                    AuthorId = authorId,
                    SolutionId = solutionId,
                    Vote = Domain.Vote.UP
                };

                _applicationDbContext.Reaction.Add(reaction);
                await _applicationDbContext.SaveChangesAsync();
            }
            return resultModel;
        }
    }
}
