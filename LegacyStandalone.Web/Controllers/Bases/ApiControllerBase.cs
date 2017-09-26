using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using AutoMapper;
using LegacyApplication.Database.Infrastructure;
using LegacyApplication.Models.Core;
using LegacyApplication.Models.HumanResources;
using LegacyApplication.Repositories.Core;
using LegacyApplication.Repositories.HumanResources;
using LegacyApplication.Services.Core;
using LegacyApplication.Shared.Configurations;
using LegacyApplication.ViewModels.Core;
using LegacyStandalone.Web.Controllers.Core;
using LegacyStandalone.Web.Models;
using Microsoft.AspNet.Identity.Owin;

namespace LegacyStandalone.Web.Controllers.Bases
{
    public abstract class ApiControllerBase : ApiController
    {
        protected readonly ICommonService CommonService;
        protected readonly IUnitOfWork UnitOfWork;
        protected readonly IDepartmentRepository DepartmentRepository;
        protected readonly IUploadedFileRepository UploadedFileRepository;

        protected ApiControllerBase(
            ICommonService commonService,
            IUnitOfWork untOfWork)
        {
            CommonService = commonService;
            UnitOfWork = untOfWork;
            DepartmentRepository = commonService.DepartmentRepository;
            UploadedFileRepository = commonService.UploadedFileRepository;
        }

        #region Current Information

        protected DateTime Now => DateTime.Now;
        protected string UserName => User.Identity.Name;

        protected ApplicationUserManager UserManager => Request.GetOwinContext().GetUserManager<ApplicationUserManager>();

        [NonAction]
        protected async Task<ApplicationUser> GetMeAsync()
        {
            var me = await UserManager.FindByNameAsync(UserName);
            return me;
        }

        [NonAction]
        protected async Task<Department> GetMyDepartmentEvenNull()
        {
            var department = await DepartmentRepository.GetSingleAsync(x => x.Employees.Any(y => y.No == UserName));
            return department;
        }

        [NonAction]
        protected async Task<Department> GetMyDepartmentNotNull()
        {
            var department = await GetMyDepartmentEvenNull();
            if (department == null)
            {
                throw new Exception("您不属于任何单位/部门");
            }
            return department;
        }

        #endregion

        #region Upload

        [NonAction]
        public virtual async Task<IHttpActionResult> Upload()
        {
            var root = GetUploadDirectory(DateTime.Now.ToString("yyyyMM"));
            var result = await UploadFiles(root);
            return Ok(result);
        }

        [NonAction]
        public virtual async Task<IHttpActionResult> GetFileAsync(int fileId)
        {
            var model = await UploadedFileRepository.GetSingleAsync(x => x.Id == fileId);
            if (model != null)
            {
                return new FileActionResult(model);
            }
            return null;
        }

        [NonAction]
        public virtual IHttpActionResult GetFileByPath(string path)
        {
            return new FileActionResult(path);
        }

        [NonAction]
        protected string GetUploadDirectory(params string[] subDirectories)
        {
#if DEBUG
            var root = HttpContext.Current.Server.MapPath("~/App_Data/Upload");
#else
            var root = AppSettings.UploadDirectory;
#endif
            if (subDirectories != null && subDirectories.Length > 0)
            {
                foreach (var t in subDirectories)
                {
                    root = Path.Combine(root, t);
                }
            }
            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
            }
            return root;
        }

        [NonAction]
        protected async Task<List<UploadedFile>> UploadFiles(string root)
        {
            var list = await UploadFilesAsync(root);
            var models = Mapper.Map<List<UploadedFileViewModel>, List<UploadedFile>>(list).ToList();
            foreach (var model in models)
            {
                UploadedFileRepository.Add(model);
            }
            await UnitOfWork.SaveChangesAsync();
            return models;
        }

        [NonAction]
        private async Task<List<UploadedFileViewModel>> UploadFilesAsync(string root)
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            var provider = new MultipartFormDataStreamProvider(root);
            var count = HttpContext.Current.Request.Files.Count;
            var files = new List<HttpPostedFile>(count);
            for (var i = 0; i < count; i++)
            {
                files.Add(HttpContext.Current.Request.Files[i]);
            }
            await Request.Content.ReadAsMultipartAsync(provider);
            var list = new List<UploadedFileViewModel>();
            var now = DateTime.Now;
            foreach (var file in provider.FileData)
            {
                var temp = file.Headers.ContentDisposition.FileName;
                var length = temp.Length;
                var lastSlashIndex = temp.LastIndexOf(@"\", StringComparison.Ordinal);
                var fileName = temp.Substring(lastSlashIndex + 2, length - lastSlashIndex - 3);
                var fileInfo = files.SingleOrDefault(x => x.FileName == fileName);
                long size = 0;
                if (fileInfo != null)
                {
                    size = fileInfo.ContentLength;
                }
                var newFile = new UploadedFileViewModel
                {
                    FileName = fileName,
                    Path = file.LocalFileName,
                    Size = size,
                    Deleted = false
                };
                var userName = string.IsNullOrEmpty(User.Identity?.Name)
                    ? "anonymous"
                    : User.Identity.Name;
                newFile.CreateUser = newFile.UpdateUser = userName;
                newFile.CreateTime = newFile.UpdateTime = now;
                newFile.LastAction = "上传";
                list.Add(newFile);
            }
            return list;
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            UserManager?.Dispose();
            UnitOfWork?.Dispose();
        }
    }

    #region Upload Model

    internal class FileActionResult : IHttpActionResult
    {
        private readonly bool _isInline = false;
        private readonly string _contentType;
        public FileActionResult(UploadedFile fileModel, string contentType, bool isInline = false)
        {
            UploadedFile = fileModel;
            _contentType = contentType;
            _isInline = isInline;
        }

        public FileActionResult(UploadedFile fileModel)
        {
            UploadedFile = fileModel;
        }

        public FileActionResult(string path)
        {
            UploadedFile = new UploadedFile
            {
                Path = path
            };
        }

        private UploadedFile UploadedFile { get; set; }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            FileStream file;
            try
            {
                file = File.OpenRead(UploadedFile.Path);
            }
            catch (DirectoryNotFoundException)
            {
                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.NotFound));
            }
            catch (FileNotFoundException)
            {
                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.NotFound));
            }

            var response = new HttpResponseMessage
            {
                Content = new StreamContent(file)
            };
            var name = UploadedFile.FileName ?? file.Name;
            var last = name.LastIndexOf("\\", StringComparison.Ordinal);
            if (last > -1)
            {
                var length = name.Length - last - 1;
                name = name.Substring(last + 1, length);
            }
            if (!string.IsNullOrEmpty(_contentType))
            {
                response.Content.Headers.ContentType = MediaTypeHeaderValue.Parse(_contentType);
            }
            response.Content.Headers.ContentDisposition =
                new ContentDispositionHeaderValue(_isInline ? DispositionTypeNames.Inline : DispositionTypeNames.Attachment)
                {
                    FileName = HttpUtility.UrlEncode(name, Encoding.UTF8)
                };

            return Task.FromResult(response);
        }
    }
    #endregion
}