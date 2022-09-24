using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using GuXin.Core.Controllers.Basic;
using GuXin.Core.Extensions;
using GuXin.Core.Filters;
using GuXin.System.IServices;

namespace GuXin.System.Controllers
{
    [Route("api/Sys_Dictionary")]
    public partial class Sys_DictionaryController : ApiBaseController<ISys_DictionaryService>
    {
        public Sys_DictionaryController(ISys_DictionaryService service)
        : base("System", "System", "Sys_Dictionary", service)
        {
        }
    }
}
