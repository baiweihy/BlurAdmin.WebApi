using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace LegacyStandalone.Web.MyConfigurations.Exceptions
{
    public class MyExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            var result = new TextPlainErrorResult
            {
                Request = context.ExceptionContext.Request,
                Content = "请求失败."
            };
            if (context.Exception is DbUpdateException)
            {
                string exceptionMessage = context.Exception.GetBaseException().Message;
                if (exceptionMessage.StartsWith("Cannot insert duplicate key row in object"))
                {
                    const string firstString = "The duplicate key value is (";
                    int firstIndex = exceptionMessage.IndexOf(firstString, StringComparison.Ordinal);
                    int lastIndex = exceptionMessage.IndexOf(")", StringComparison.Ordinal);
                    if (firstIndex != -1 && lastIndex != -1)
                    {
                        int length = lastIndex - firstIndex - firstString.Length;
                        if (length > 0)
                        {
                            string columnName = exceptionMessage.Substring(firstIndex + firstString.Length, length);
                            result.Content = "“" + columnName + "”已存在";
                        }
                    }
                }
                else if (exceptionMessage.StartsWith("Violation of PRIMARY KEY constraint"))
                {
                    const string firstString = "The duplicate key value is (";
                    int firstIndex = exceptionMessage.IndexOf(firstString, StringComparison.Ordinal);
                    int lastIndex = exceptionMessage.IndexOf(")", StringComparison.Ordinal);
                    if (firstIndex != -1 && lastIndex != -1)
                    {
                        int length = lastIndex - firstIndex - firstString.Length;
                        if (length > 0)
                        {
                            string pkName = exceptionMessage.Substring(firstIndex + firstString.Length, length);
                            result.Content = "“" + pkName + "”已存在";
                        }
                    }
                }
                else if (exceptionMessage.StartsWith("The DELETE statement conflicted with the REFERENCE constraint"))
                {
                    result.Content = "有关联数据已存在，无法删除";
                }
                else if (exceptionMessage.StartsWith("The INSERT statement conflicted with the FOREIGN KEY constraint"))
                {
                    result.Content = "缺少依赖的数据，无法新增";
                }
                else if (exceptionMessage.StartsWith("Parameter value ") && exceptionMessage.EndsWith(" is out of range."))
                {
                    const string firstString = "Parameter value '";
                    int firstIndex = exceptionMessage.IndexOf(firstString, StringComparison.Ordinal);
                    int lastIndex = exceptionMessage.LastIndexOf("'", StringComparison.Ordinal);
                    if (firstIndex != -1 && lastIndex != -1)
                    {
                        int length = lastIndex - firstIndex - firstString.Length;
                        if (length > 0)
                        {
                            string value = exceptionMessage.Substring(firstIndex + firstString.Length, length);
                            result.Content = "“" + value + "”超出了范围";
                        }
                    }
                }
                else if (exceptionMessage.StartsWith(
                        "The conversion of a datetime2 data type to a datetime data type resulted in an out-of-range value"))
                {
                    result.Content = "日期格式不正确";
                }
            }
            else if (context.Exception is DbEntityValidationException)
            {
                var exception = context.Exception.GetBaseException() as DbEntityValidationException;
                if (exception != null)
                {
                    IEnumerable<DbEntityValidationResult> errors = exception.EntityValidationErrors;
                    var msgTemp = new StringBuilder("");
                    const string lineBreak = "<br />";
                    foreach (DbEntityValidationResult err in errors)
                    {
                        foreach (DbValidationError vErr in err.ValidationErrors)
                        {
                            msgTemp.Append(vErr.ErrorMessage).Append(lineBreak);
                        }
                    }
                    if (msgTemp.Length > 0)
                    {
                        result.Content = msgTemp.ToString();
                    }
                    result.Content = "输入错误，实体验证失败";
                }
            }
            else
            {
                result.Content = context.Exception.Message;
            }
            context.Result = result;
        }

        private class TextPlainErrorResult : IHttpActionResult
        {
            public HttpRequestMessage Request { private get; set; }

            public string Content { private get; set; }

            public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
            {
                HttpResponseMessage response =
                    new HttpResponseMessage(HttpStatusCode.InternalServerError)
                    {
                        Content = new StringContent(Content),
                        RequestMessage = Request
                    };
                return Task.FromResult(response);
            }
        }
    }
}