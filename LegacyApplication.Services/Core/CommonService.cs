using LegacyApplication.Repositories.Core;
using LegacyApplication.Repositories.HumanResources;
using System;
using System.Collections.Generic;
using System.Text;

namespace LegacyApplication.Services.Core
{
    public interface ICommonService
    {
        IUploadedFileRepository UploadedFileRepository { get; }
        IDepartmentRepository DepartmentRepository { get; }
    }

    public class CommonService : ICommonService
    {
        public IUploadedFileRepository UploadedFileRepository { get; }
        public IDepartmentRepository DepartmentRepository { get; }

        public CommonService(
            IUploadedFileRepository uploadedFileRepository,
            IDepartmentRepository departmentRepository)
        {
            UploadedFileRepository = uploadedFileRepository;
        }
    }
}
