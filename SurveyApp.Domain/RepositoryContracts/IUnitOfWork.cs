using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Domain.RepositoryContracts
{
    public interface IUnitOfWork
    {
        ISurveyRepository Surveys { get; }
        IQuestionRepository Questions { get; }
        IChoiceRepository Choices { get; }
        Task SaveAsync();
    }
}
