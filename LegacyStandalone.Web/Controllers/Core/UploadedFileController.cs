using System.Threading.Tasks;
using System.Web.Http;
using LegacyApplication.Database.Infrastructure;
using LegacyStandalone.Web.Controllers.Bases;
using LegacyApplication.Services.Core;

namespace LegacyStandalone.Web.Controllers.Core
{
    public class UploadedFileController : ApiControllerBase
    {
        public UploadedFileController(ICommonService commonService,
            IUnitOfWork unitOfWork)
            : base(commonService, unitOfWork)
        {
        }

        [AllowAnonymous]
        [Route("api/UploadFile")]
        [HttpPost]
        public async Task<IHttpActionResult> UploadFile()
        {
            return await Upload();
        }

        [AllowAnonymous]
        [Route("api/Download/{fileId}")]
        [HttpGet]
        public async Task<IHttpActionResult> DownloadFileAsync(int fileId)
        {
            return await GetFileAsync(fileId);
        }

        [AllowAnonymous]
        [Route("api/Download/Path")]
        [HttpGet]
        public IHttpActionResult DownloadFileByPath(string path)
        {
            return GetFileByPath(path);
        }
    }
}