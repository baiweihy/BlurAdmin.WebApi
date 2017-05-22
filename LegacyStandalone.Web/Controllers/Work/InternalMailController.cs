using System;
using System.Collections.Generic;
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
using LegacyApplication.ViewModels.Core;
using LegacyApplication.ViewModels.HumanResources;
using LegacyApplication.ViewModels.Work;
using LegacyStandalone.Web.Controllers.Bases;
using Newtonsoft.Json.Linq;

namespace LegacyStandalone.Web.Controllers.Work
{
    [System.Web.Mvc.RoutePrefix("api/InternalMail")]
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