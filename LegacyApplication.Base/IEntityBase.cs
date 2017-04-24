using System;
using LegacyApplication.Shared.Enums;

namespace LegacyApplication.Base
{
    public interface IEntityBase
    {
        int Id { get; set; }
        DateTime CreateTime { get; set; }
        DateTime UpdateTime { get; set; }
        string CreateUser { get; set; }
        string UpdateUser { get; set; }
        string LastAction { get; set; }
        Status Status { get; set; }
        string StatusDisplay { get; }
    }
}