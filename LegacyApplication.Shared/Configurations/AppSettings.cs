namespace LegacyApplication.Shared.Configurations
{
    public class AppSettings
    {
        public const string DefaultConnection = "DefaultConnection";
        public const string UploadDirectory = @"F://Upload";

        //Stateless
        public const int 开始节点 = 0;
        public const int 提交申请 = 0;
        public const int 审批通过 = 100000;
        public const int 审批未通过 = -100000;
    }
}