using LegacyApplication.Shared.Features.Base;

namespace LegacyApplication.ViewModels.Scrum
{
    public class ProjectTeamMemberViewModel : EntityBase
    {
        public int ProjectId { get; set; }

        public string UserName { get; set; }

        public ProjectViewModel Project { get; set; }
    }
}
