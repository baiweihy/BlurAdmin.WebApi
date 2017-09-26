using LegacyApplication.Models.Work;
using LegacyApplication.Repositories.Work;
using LegacyApplication.Shared.ByModule.Work.Enums;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyApplication.Services.Work
{
    public interface IInternalMailService
    {
        void AddSystemMail(string title, string message, string toUserName, string fromUserName);
        void AchievementBack(int assessmentItemNo, string toUserName, string fromUserName);
        void AdditionBack(string description, string toUserName, string fromUserName);
        Task<int> GetUnreadCountAsync(string userName);
        Task<int> GetNormalUnreadCountAsync(string userName);
        Task<int> GetSystemUnreadCountAsync(string userName);
    }

    public class InternalMailService : IInternalMailService
    {
        private readonly IInternalMailRepository _internalMailRepository;
        private readonly IInternalMailToRepository _internalMailToRepository;

        public InternalMailService(
            IInternalMailRepository internalMailRepository,
            IInternalMailToRepository internalMailToRepository)
        {
            _internalMailRepository = internalMailRepository;
            _internalMailToRepository = internalMailToRepository;
        }

        public void AddSystemMail(string title, string message, string toUserName, string fromUserName)
        {
            const string systemUserName = "系统自动发送";
            var mail = new InternalMail
            {
                UserName = systemUserName,
                PersonName = systemUserName,
                Title = title,
                Body = $"<h3>{message}</h3>",
                SendTime = DateTime.Now,
                MailType = MailType.系统,
                CreateUser = fromUserName,
                UpdateUser = fromUserName,
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,
                LastAction = "发送",
                Tos = new List<InternalMailTo>
                {
                    new InternalMailTo
                    {
                        UserName = toUserName,
                        PersonName = toUserName,
                        CreateUser = fromUserName,
                        UpdateUser = fromUserName,
                        CreateTime = DateTime.Now,
                        UpdateTime = DateTime.Now,
                        LastAction = "发送"
                    }
                }
            };
            _internalMailRepository.Add(mail);
        }

        public void AchievementBack(int assessmentItemNo, string toUserName, string fromUserName)
        {
            var message = $@"<p>指标序号为“{assessmentItemNo}”的工作完成情况申请被退回。</p>
                            <p><a href='/#/assessed/assessed!achievement' target='_blank'>点此查看</a></p>";
            AddSystemMail("工作完成情况申请被退回", message, toUserName, fromUserName);
        }

        public void AdditionBack(string description, string toUserName, string fromUserName)
        {
            description = description.Length > 30 ? (description.Substring(0, 30) + "... ...") : description;
            var message = $@"<p>内容为“{description}”的加分申请申请被退回。</p>
                            <p><a href='/#/assessed/assessed!extraItemApplication' target='_blank'>点此查看</a></p>";
            AddSystemMail("加分申请被退回", message, toUserName, fromUserName);
        }

        public async Task<int> GetUnreadCountAsync(string userName)
        {
            var count = await _internalMailToRepository.All.Where(x => x.UserName == userName && !x.HasRead && !x.HasDeleted).CountAsync();
            return count;
        }

        public async Task<int> GetNormalUnreadCountAsync(string userName)
        {
            var count = await _internalMailToRepository.All.Where(x => x.UserName == userName && !x.HasRead && !x.HasDeleted && x.Mail.MailType == MailType.普通).CountAsync();
            return count;
        }

        public async Task<int> GetSystemUnreadCountAsync(string userName)
        {
            var count = await _internalMailToRepository.All.Where(x => x.UserName == userName && !x.HasRead && !x.HasDeleted && x.Mail.MailType == MailType.系统).CountAsync();
            return count;
        }
    }
}
