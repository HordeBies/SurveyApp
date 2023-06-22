using SurveyApp.Domain.Entities;
using SurveyApp.Domain.RepositoryContracts;
using SurveyApp.Infrastructure.DatabaseContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Infrastructure.Repositories
{
    public class QuestionRepository(ApplicationDbContext db) : Repository<Question>(db), IQuestionRepository
    {
    }
}
