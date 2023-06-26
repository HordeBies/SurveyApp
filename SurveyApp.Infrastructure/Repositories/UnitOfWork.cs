using SurveyApp.Domain.RepositoryContracts;
using SurveyApp.Infrastructure.DatabaseContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Infrastructure.Repositories
{
    public class UnitOfWork(ApplicationDbContext db, ISurveyRepository surveys, IQuestionRepository questions, IChoiceRepository choices, ISubmissionRepository submissions, IAnswerRepository answers) : IUnitOfWork
    {
        public ISurveyRepository Surveys { get; } = surveys;
        public IQuestionRepository Questions { get; } = questions;
        public IChoiceRepository Choices { get; } = choices;
        public ISubmissionRepository Submissions { get; } = submissions;
        public IAnswerRepository Answers { get; } = answers;
         
        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }
    }
}
