using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using LegacyApplication.Database.Infrastructure;
using LegacyApplication.Models.Work;
using LegacyApplication.Repositories.HumanResources;
using LegacyApplication.Repositories.Work;
using LegacyApplication.Shared.ByModule.Work.Enums;
using LegacyApplication.Shared.Features.Pagination;
using LegacyApplication.ViewModels.Core;
using LegacyApplication.ViewModels.Work;
using LegacyStandalone.Web.Controllers.Bases;
using Newtonsoft.Json.Linq;
using LegacyApplication.Services.Core;

namespace LegacyStandalone.Web.Controllers.Work
{
    [RoutePrefix("api/InternalMail")]
    public class InternalMailController : ApiControllerBase
    {
        private readonly IInternalMailRepository _internalMailRepository;
        private readonly IInternalMailToRepository _internalMailToRepository;

        public InternalMailController(
            IInternalMailRepository internalMailRepository,
            IInternalMailToRepository internalMailToRepository,
            ICommonService commonService,
            IUnitOfWork unitOfWork) : base(commonService, unitOfWork)
        {
            _internalMailRepository = internalMailRepository;
            _internalMailToRepository = internalMailToRepository;
        }

        [Route("Inbox/{pageIndex}/{pageSize}/{mailType?}")]
        public async Task<PaginatedItemsViewModel<InboxMailViewModel>> GetInbox(int pageIndex, int pageSize, MailType? mailType = null)
        {
            var exp = _internalMailToRepository.AllIncluding(x => x.Mail).Where(x => x.UserName == UserName && !x.HasDeleted);
            if (mailType != null)
            {
                exp = exp.Where(x => x.Mail.MailType == mailType);
            }
            var items = await exp.OrderByDescending(x => x.Id)
                .Skip(pageIndex * pageSize).Take(pageSize).ToListAsync();
            var count = await exp.CountAsync();
            var vms = items.Select(x => new InboxMailViewModel
            {
                Id = x.Mail.Id,
                FromUserName = x.Mail.UserName,
                FromPersonName = x.Mail.PersonName,
                Title = x.Mail.Title,
                Body = x.Mail.Body,
                SendTime = x.Mail.SendTime,
                FromHasDeleted = x.Mail.HasDeleted,
                MailType = x.Mail.MailType,
                ReceiveId = x.Id,
                ReceiveUserName = x.UserName,
                ReceivePersonName = x.PersonName,
                HasRead = x.HasRead,
                ReadTime = x.ReadTime,
                ReceiveHasDeleted = x.HasDeleted,
                HasAttachments = x.Mail.Attachments.Any()
            }).ToList();
            var result = new PaginatedItemsViewModel<InboxMailViewModel>(pageIndex, pageSize, count, vms);
            return result;
        }

        [HttpGet]
        [Route("Unread")]
        public async Task<IEnumerable<InboxMailViewModel>> GetUnread()
        {
            var items = await _internalMailToRepository.AllIncluding(x => x.Mail)
                .Where(x => x.UserName == UserName && !x.HasRead && !x.HasDeleted).ToListAsync();
            var vms = items.Select(x => new InboxMailViewModel
            {
                Id = x.Mail.Id,
                FromUserName = x.Mail.UserName,
                FromPersonName = x.Mail.PersonName,
                Title = x.Mail.Title,
                Body = x.Mail.Body,
                SendTime = x.Mail.SendTime,
                FromHasDeleted = x.Mail.HasDeleted,
                MailType = x.Mail.MailType,
                ReceiveId = x.Id,
                ReceiveUserName = x.UserName,
                ReceivePersonName = x.PersonName,
                HasRead = x.HasRead,
                ReadTime = x.ReadTime,
                ReceiveHasDeleted = x.HasDeleted,
                HasAttachments = x.Mail.Attachments.Any()
            }).ToList();
            return vms;
        }

        [Route("UnreadCount")]
        public async Task<IHttpActionResult> GetUnreadCount()
        {
            var count = await _internalMailToRepository.All.Where(x => x.UserName == UserName && !x.HasRead && !x.HasDeleted).CountAsync();
            return Ok(new { count });
        }

        public async Task<IHttpActionResult> GetOne(int id)
        {
            var mail = await _internalMailRepository.GetSingleAsync(x => x.Id == id && (x.UserName == UserName || x.Tos.Any(y => y.UserName == UserName)), x => x.Tos, x => x.Attachments);
            if (mail == null)
            {
                return NotFound();
            }
            if (mail.Tos.Any(x => x.UserName == UserName && !x.HasRead))
            {
                var to = mail.Tos.SingleOrDefault(x => x.UserName == UserName);
                if (to != null)
                {
                    to.HasRead = true;
                    to.ReadTime = Now;
                    to.UpdateUser = UserName;
                    to.UpdateTime = Now;
                    to.LastAction = "阅读";
                    _internalMailToRepository.Update(to);
                    await UnitOfWork.SaveChangesAsync();
                }
            }
            var vm = Mapper.Map<InternalMail, InternalMailViewModel>(mail);
            return Ok(vm);
        }

        [HttpPut]
        [Route("MarkAsRead")]
        public async Task<IHttpActionResult> MarkAsRead(JObject jObj)
        {
            var ids = jObj["ids"].ToObject<List<int>>();
            var tos = await _internalMailToRepository.All.Where(x => x.UserName == UserName && ids.Contains(x.Id)).ToListAsync();
            foreach (var to in tos)
            {
                to.HasRead = true;
                to.UpdateTime = Now;
                to.UpdateUser = UserName;
                to.LastAction = "设为已读";
                _internalMailToRepository.Update(to);
            }
            await UnitOfWork.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        [Route("MarkAsUnread")]
        public async Task<IHttpActionResult> MarkAsUnread(JObject jObj)
        {
            var ids = jObj["ids"].ToObject<List<int>>();
            var tos = await _internalMailToRepository.All.Where(x => x.UserName == UserName && ids.Contains(x.Id)).ToListAsync();
            foreach (var to in tos)
            {
                to.HasRead = false;
                to.UpdateTime = Now;
                to.UpdateUser = UserName;
                to.LastAction = "设为未读";
                _internalMailToRepository.Update(to);
            }
            await UnitOfWork.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        [Route("DeleteInbox")]
        public async Task<IHttpActionResult> DeleteInbox(JObject jObj)
        {
            var ids = jObj["ids"].ToObject<List<int>>();
            var tos = await _internalMailToRepository.All.Where(x => x.UserName == UserName && ids.Contains(x.Id)).ToListAsync();
            foreach (var to in tos)
            {
                to.HasDeleted = true;
                to.UpdateTime = Now;
                to.UpdateUser = UserName;
                to.LastAction = "删除";
                _internalMailToRepository.Update(to);
            }
            await UnitOfWork.SaveChangesAsync();
            return Ok();
        }

        [Route("Sent/{pageIndex}/{pageSize}")]
        public async Task<PaginatedItemsViewModel<SentMailViewModel>> GetSent(int pageIndex, int pageSize)
        {
            var exp = _internalMailRepository.AllIncluding(x => x.Tos, x => x.Attachments).Where(x => x.UserName == UserName && !x.HasDeleted);
            var items = await exp.OrderByDescending(x => x.Id)
                .Skip(pageIndex * pageSize).Take(pageSize).ToListAsync();
            var count = await exp.CountAsync();
            var vms = Mapper.Map<IEnumerable<InternalMail>, List<SentMailViewModel>>(items);
            var result = new PaginatedItemsViewModel<SentMailViewModel>(pageIndex, pageSize, count, vms);
            return result;
        }

        [HttpPut]
        [Route("DeleteSent")]
        public async Task<IHttpActionResult> DeleteSent(JObject jObj)
        {
            var ids = jObj["ids"].ToObject<List<int>>();
            var mails = await _internalMailRepository.All.Where(x => x.UserName == UserName && ids.Contains(x.Id)).ToListAsync();
            foreach (var mail in mails)
            {
                mail.HasDeleted = true;
                mail.UpdateTime = Now;
                mail.UpdateUser = UserName;
                mail.LastAction = "删除";
                _internalMailRepository.Update(mail);
            }
            await UnitOfWork.SaveChangesAsync();
            return Ok();
        }

        [Route("TrashBox/{pageIndex}/{pageSize}")]
        public async Task<PaginatedItemsViewModel<InternalMailViewModel>> GetTrashBox(int pageIndex, int pageSize)
        {
            var exp = _internalMailRepository.AllIncluding(x => x.Tos, x => x.Attachments).Where(x => (x.UserName == UserName && x.HasDeleted) || x.Tos.Any(y => y.UserName == UserName && y.HasDeleted));
            var items = await exp.OrderByDescending(x => x.Id)
                .Skip(pageIndex * pageSize).Take(pageSize).ToListAsync();
            var count = await exp.CountAsync();
            var vms = Mapper.Map<IEnumerable<InternalMail>, List<InternalMailViewModel>>(items);
            var result = new PaginatedItemsViewModel<InternalMailViewModel>(pageIndex, pageSize, count, vms);
            return result;
        }

        public async Task<IHttpActionResult> Post(JObject obj)
        {
            var mail = obj["mail"].ToObject<InternalMailViewModel>();
            var toUserNames = obj["tos"].ToObject<List<string>>();
            var attachmentVms = obj["attachments"].ToObject<List<UploadedFileViewModel>>();

            var newMailModel = Mapper.Map<InternalMailViewModel, InternalMail>(mail);
            newMailModel.UserName = UserName;
            newMailModel.SendTime = Now;
            newMailModel.CreateUser = newMailModel.UpdateUser = User.Identity.Name;
            newMailModel.LastAction = "发送消息";
            var tos = toUserNames.Select(x => new InternalMailTo
            {
                UserName = x,
                CreateTime = Now,
                CreateUser = UserName,
                UpdateTime = Now,
                UpdateUser = UserName,
                LastAction = "发送消息"
            }).ToList();
            var attachments = attachmentVms.Select(x => new InternalMailAttachment
            {
                FileId = x.FileId,
                FileName = x.FileName,
                Size = x.Size,
                Path = x.Path,
                CreateTime = Now,
                CreateUser = UserName,
                UpdateTime = Now,
                UpdateUser = UserName,
                LastAction = "发送消息"
            }).ToList();
            newMailModel.Tos = tos;
            newMailModel.Attachments = attachments;
            _internalMailRepository.Add(newMailModel);
            await UnitOfWork.SaveChangesAsync();

            return RedirectToRoute("", new { controller = "InternalMail", id = newMailModel.Id });
        }

        public async Task<IHttpActionResult> Delete(int id)
        {
            var mail = await _internalMailRepository.GetSingleAsync(x => x.Id == id && (x.UserName == UserName || x.Tos.Any(y => y.UserName == UserName)), x => x.Tos);
            if (mail == null)
            {
                return NotFound();
            }
            if (mail.UserName == UserName)
            {
                mail.HasDeleted = true;
                mail.UpdateUser = UserName;
                mail.UpdateTime = Now;
                mail.LastAction = "删除";
                _internalMailRepository.Update(mail);
            }
            var to = mail.Tos.SingleOrDefault(x => x.UserName == UserName);
            if (to != null)
            {
                to.HasDeleted = true;
                to.UpdateUser = UserName;
                to.UpdateTime = Now;
                to.LastAction = "删除";
                _internalMailToRepository.Update(to);
            }
            await UnitOfWork.SaveChangesAsync();
            return Ok();
        }

    }
}