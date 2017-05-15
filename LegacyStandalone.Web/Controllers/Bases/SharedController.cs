using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace LegacyStandalone.Web.Controllers.Bases
{
    [RoutePrefix("api/Shared")]
    public class SharedController : ApiController
    {
        [HttpGet]
        [Route("Enums/{moduleName?}")]
        public IHttpActionResult GetEnums(string moduleName = null)
        {
            var exp = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(t => t.GetTypes())
                .Where(t => t.IsEnum);
            if (!string.IsNullOrEmpty(moduleName))
            {
                exp = exp.Where(x => x.Namespace == $"LegacyApplication.Shared.ByModule.{moduleName}.Enums");
            }
            var enumTypes = exp;
            var result = new Dictionary<string, Dictionary<string, int>>();
            foreach (var enumType in enumTypes)
            {
                result[enumType.Name] = Enum.GetValues(enumType).Cast<int>().ToDictionary(e => Enum.GetName(enumType, e), e => e);
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("EnumsList/{moduleName?}")]
        public IHttpActionResult GetEnumsList(string moduleName = null)
        {
            var exp = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(t => t.GetTypes())
                .Where(t => t.IsEnum);
            if (!string.IsNullOrEmpty(moduleName))
            {
                exp = exp.Where(x => x.Namespace == $"LegacyApplication.Shared.ByModule.{moduleName}.Enums");
            }
            var enumTypes = exp;
            var result = new Dictionary<string, List<KeyValuePair<string, int>>>();
            foreach (var e in enumTypes)
            {
                var names = Enum.GetNames(e);
                var values = Enum.GetValues(e).Cast<int>().ToArray();
                var count = names.Count();
                var list = new List<KeyValuePair<string, int>>(count);
                for (var i = 0; i < count; i++)
                {
                    list.Add(new KeyValuePair<string, int> (names[i], values[i]));
                }
                result.Add(e.Name, list);
            }
            return Ok(result);
        }
    }
}
