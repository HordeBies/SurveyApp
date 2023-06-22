using SurveyApp.Domain.RepositoryContracts;
using SurveyApp.Infrastructure.DatabaseContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Infrastructure.Repositories
{
    public class UnitOfWork(ApplicationDbContext db) : IUnitOfWork
    {
        public ISurveyRepository Surveys { get; } = new SurveyRepository(db);
        public IQuestionRepository Questions { get; } = new QuestionRepository(db);
        public IChoiceRepository Choices { get; } = new ChoiceRepository(db);
         
        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }
    }
}
