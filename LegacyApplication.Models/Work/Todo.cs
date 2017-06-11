using LegacyApplication.Shared.Features.Base;

namespace LegacyApplication.Models.Work
{
    public class Todo: EntityBase
    {
        public string Title { get; set; }
        public bool Completed { get; set; }
        public string UserName { get; set; }
        public bool Deleted { get; set; }
    }

    public class TodoConfiguration : EntityBaseConfiguration<Todo>
    {
        public TodoConfiguration()
        {
            ToTable("work.Todo");

            Property(x => x.Title).IsRequired().HasMaxLength(50);
            Property(x => x.UserName).IsRequired().HasMaxLength(50);
        }
    }
}
