using System;
using LegacyApplication.Shared.Enums;

namespace LegacyApplication.Base
{
    public class EntityBase : IEntityBase
    {
        public EntityBase()
        {
            CreateTime = UpdateTime = DateTime.Now;
            LastAction = "添加";
            CreateUser = UpdateUser = "匿名";
            Status = Status.正常;
        }

        public int Id { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public string CreateUser { get; set; }
        public string UpdateUser { get; set; }
        public string LastAction { get; set; }
        public Status Status { get; set; }
        public string StatusDisplay => Status.ToString();
    }
}