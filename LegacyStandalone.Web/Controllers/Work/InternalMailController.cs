using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using AutoMapper;
using LegacyApplication.Database.Infrastructure;
using LegacyApplication.Models.Core;
using LegacyApplication.Models.HumanResources;
using LegacyApplication.Models.Work;
using LegacyApplication.Repositories.HumanResources;
using LegacyApplication.Repositories.Work;
using LegacyApplication.Shared.ByModule.Work.Enums;
using LegacyApplication.Shared.Features.Pagination;
using LegacyApplication.ViewModels.Core;
using LegacyApplication.ViewModels.HumanResources;
using LegacyApplication.ViewModels.Work;
using LegacyStandalone.Web.Controllers.Bases;
using LegacyStandalone.Web.Models;
using Newtonsoft.Json.Linq;

namespace LegacyStandalone.Web.Controllers.Work
{
    [RoutePrefix("api/InternalMail")]
    public class InternalMailController : ApiControllerBase
    {
        private readonly IInternalMailRepository _internalMailRepository;
        public InternalMailController(
            IInternalMailRepository internalMailRepository,
            IUnitOfWork unitOfWork,
            IDepartmentRepository departmentRepository) : base(unitOfWork, departmentRepository)
        {
            _internalMailRepository = internalMailRepository;
        }

        [Route("Inbox/{pageIndex}/{pageSize}/{mailType?}")]
        public async Task<PaginatedItemsViewModel<InternalMailViewModel>> GetByPage(int pageIndex, int pageSize, MailType? mailType = null)
        {
            var exp = _internalMailRepository.AllIncluding(x => x.Tos, x => x.Attachments).Where(x => x.Tos.Any(y => y.UserName == UserName));
            if (mailType != null)
            {
                exp = exp.Where(x => x.MailType == mailType);
            }
            var items = await exp.OrderByDescending(x => x.Id)
                .Skip(pageIndex * pageSize).Take(pageSize).ToListAsync();
            var count = await exp.CountAsync();
            var vms = Mapper.Map<IEnumerable<InternalMail>, List<InternalMailViewModel>>(items);
            var result = new PaginatedItemsViewModel<InternalMailViewModel>(pageIndex, pageSize, count, vms);
            return result;
        }

        public async Task<IHttpActionResult> GetOne(int id)
        {
            var model = await _internalMailRepository.GetSingleAsync(x => x.Id == id, x => x.Tos, x => x.Attachments);
            if (model != null)
            {
                var viewModel = Mapper.Map<InternalMail, InternalMailViewModel>(model);
                return Ok(viewModel);
            }
            return NotFound();
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

    }
}