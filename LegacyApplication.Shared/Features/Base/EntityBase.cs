using System;

namespace LegacyApplication.Shared.Features.Base
{
    public class EntityBase : IEntityBase
    {
        public EntityBase(string userName = "匿名")
        {
            CreateTime = UpdateTime = DateTime.Now;
            LastAction = "创建";
            CreateUser = UpdateUser = userName;
        }

        public int Id { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public string CreateUser { get; set; }
        public string UpdateUser { get; set; }
        public string LastAction { get; set; }

        public int Order { get; set; }
    }
}