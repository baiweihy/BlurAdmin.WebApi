using LegacyApplication.Shared.Features.Base;

namespace LegacyApplication.ViewModels.Scrum
{
    public class ProjectViewModel : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ProjectManager { get; set; }
    }
}
