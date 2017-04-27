using System;
using LegacyApplication.Shared.Enums;

namespace LegacyApplication.Shared.Features.Base
{
    public class EntityBase : IEntityBase
    {
        public EntityBase(string userName = "匿名")
        {
            CreateTime = UpdateTime = DateTime.Now;
            LastAction = "添加";
            CreateUser = UpdateUser = userName;
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