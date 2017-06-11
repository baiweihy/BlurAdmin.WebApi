using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LegacyApplication.Shared.Features.Base;

namespace LegacyApplication.ViewModels.Work
{
    public class TodoViewModel: EntityBase
    {
        public string Title { get; set; }
        public bool Completed { get; set; }
        public string UserName { get; set; }
        public bool Deleted { get; set; }
    }
}
