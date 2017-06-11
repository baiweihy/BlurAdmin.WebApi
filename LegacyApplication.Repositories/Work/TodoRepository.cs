using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LegacyApplication.Database.Infrastructure;
using LegacyApplication.Models.Work;

namespace LegacyApplication.Repositories.Work
{
    public interface ITodoRepository : IEntityBaseRepository<Todo>
    {
    }

    public class TodoRepository : EntityBaseRepository<Todo>, ITodoRepository
    {
        public TodoRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
